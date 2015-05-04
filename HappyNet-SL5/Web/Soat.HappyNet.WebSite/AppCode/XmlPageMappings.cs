// -----------------------------------------------------------------------
// <copyright file="XmlPageMappings.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Xml.Linq;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Soat.HappyNet.WebSite
{
    /// <summary>
    /// Static class mapping urls based on a xml file
    /// </summary>
    public static class XmlPageMappings
    {
        #region Static fields and constants

        private const string QueryStringDelimiter = "?";
        private const string ValueDelimiter = "=";
        private const string StatePairDelimiter = "&";
        private const string FragmentDelimiter = "#";
        private static readonly char[] externalQueryStringDelimiter = new char[] { ';' };
        private static readonly char[] externalFragmentDelimiter = new char[] { '$' };
        private const string ExternalQueryStringDelimiterPercentEncoded = "%3B";
        private const string ExternalFragmentDelimiterPercentEncoded = "%24";

        #endregion

        private static readonly Regex _conversionRegex = new Regex("(?<ConversionCapture>{.*?})", RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);

        private static List<PageMappingInfo> pageMappings = new List<PageMappingInfo>();
        /// <summary>
        /// List of page mappings
        /// </summary>
        public static List<PageMappingInfo> PageMappings
        {
            get { return XmlPageMappings.pageMappings; }
            set { XmlPageMappings.pageMappings = value; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        static XmlPageMappings()
        {
            PageMappings = XmlPageMappings.GetHtmlSiteMapEntries();
        }

        /// <summary>
        /// Format an url replacing each fragment key with its value
        /// </summary>
        /// <param name="path">Path</param>
        /// <param name="fragments">Fragments</param>
        /// <returns>Formatted url</returns>
        private static string FormatUrlWithFragments(string path, Dictionary<string, string> fragments)
        {
            if (fragments != null)
            {
                foreach (var fragment in fragments)
                {
                    path = path.Replace(string.Format("{{{0}}}", fragment.Key), fragment.Value);
                }
            }

            return path;
        }

        /// <summary>
        /// Extract string queries from the path
        /// </summary>
        /// <param name="mappingPath">Mapping path given by configuration</param>
        /// <param name="requestedPath">Requested path</param>
        /// <returns>Queries as a dictionary</returns>
        public static Dictionary<string, string> ExtractQuery(PageMappingInfo mapping, string requestedPath)
        {
            Dictionary<string, string> queries = new Dictionary<string, string>();

            if (mapping != null && mapping.HtmlUriRegex != null)
            {
                requestedPath = requestedPath.Trim();

                // Avoiding any "/" unwanted at the end
                if (requestedPath.EndsWith("/"))
                    requestedPath = requestedPath.Substring(0, requestedPath.Length - 1);

                var requestMatch = mapping.HtmlUriRegex.Match(requestedPath);
                if (requestMatch.Success)
                {
                    // Starts at 1 because at index 0 is the default origin string
                    for (int i = 1; i < requestMatch.Groups.Count; i++)
                    {
                        queries.Add(mapping.HtmlUriRegex.GroupNameFromNumber(i), requestMatch.Groups[i].Value);
                    }
                }
            }

            return queries;
        }

        /// <summary>
        /// Looks for a mapping corresponding to the provided url
        /// </summary>
        /// <param name="url">Url</param>
        /// <param name="isSilverlightUrl">True if it's a silverlight url, false if it's a html one</param>
        /// <returns></returns>
        private static PageMappingInfo GetMapping(string url, bool isSilverlightUrl)
        {
            foreach (var mapping in XmlPageMappings.PageMappings) 
            {
                var path = isSilverlightUrl ? mapping.SilverlightUrl : mapping.HtmlUrl;

                // Identical path
                if (path == url)
                {
                    return mapping;
                }

                // Checks if paths are identical taking account of the fragments
                var fragments = ExtractQuery(mapping, url);

                if (fragments.Count > 0)
                {
                    mapping.Query = fragments;
                    return mapping;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the Silverlight url from the html one
        /// </summary>
        /// <param name="htmlUrl">Html url</param>
        /// <returns>Silverlight url matching the html url</returns>
        public static string GetSilverlightUrl(string htmlUrl)
        {
            var mapping = GetMapping(htmlUrl, false);
            if (mapping != null)
                return FormatUrlWithFragments(mapping.SilverlightUrl, mapping.Query);

            return null;
        }

        /// <summary>
        /// File path to the XML file with page mappings
        /// </summary>
        /// <returns></returns>
        private static string GetFilePath()
        {
            return HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["PageMappingsXmlPath"]);
        }

        /// <summary>
        /// Retrieves all of the page mappings and store them
        /// </summary>
        /// <returns></returns>
        public static List<PageMappingInfo> GetHtmlSiteMapEntries()
        {
            var xdoc = XDocument.Load(GetFilePath());

            var mappings = (from mapping in xdoc.Descendants("PageMappings").Descendants("PageMapping")
                           select new PageMappingInfo()
                           {
                               ChangeFrequency = mapping.Attribute("changeFrequency").Value,
                               HtmlUrl = mapping.Attribute("htmlUrl").Value,
                               LastModified = DateTime.Parse(mapping.Attribute("lastModified").Value),
                               Priority = mapping.Attribute("priority").Value,
                               SilverlightUrl = mapping.Attribute("silverlightUrl").Value
                           }).ToList();

            foreach (var mapping in mappings)
            {
                if (!string.IsNullOrEmpty(mapping.HtmlUrl))
                {
                    string mappingHtmlPath = mapping.HtmlUrl.Trim();

                    // Avoiding any "/" unwanted at the end
                    if (mappingHtmlPath.EndsWith("/"))
                        mappingHtmlPath = mappingHtmlPath.Substring(0, mappingHtmlPath.Length - 1);

                    var matches = _conversionRegex.Matches(mappingHtmlPath);
                    List<string> uriIdentifiers = new List<string>();
                    bool identifierIsPresentTwice = false;

                    if (matches.Count > 0)
                    {
                        foreach (Match match in matches)
                        {
                            string valWithoutBraces = match.Value.Replace("{", String.Empty).Replace("}", String.Empty);

                            // We've hit the same identifier being used twice, this isn't valid
                            if (uriIdentifiers.Contains(valWithoutBraces))
                            {
                                uriIdentifiers.Clear();
                                identifierIsPresentTwice = true;
                                break;
                            }

                            uriIdentifiers.Add(valWithoutBraces);
                        }

                        if (!identifierIsPresentTwice)
                        {
                            // Let's check if the SilverlightUrl contains all the identifiers
                            string mappingSilverlightPath = mapping.SilverlightUrl.Trim();

                            // Avoiding any "/" unwanted at the end
                            if (mappingSilverlightPath.EndsWith("/"))
                                mappingSilverlightPath = mappingSilverlightPath.Substring(0, mappingSilverlightPath.Length - 1);

                            var SLmatches = _conversionRegex.Matches(mappingSilverlightPath);
                            if (SLmatches.Count > 0)
                            {
                                foreach (Match match in SLmatches)
                                {
                                    string valWithoutBraces = match.Value.Replace("{", String.Empty).Replace("}", String.Empty);
                                    if (uriIdentifiers.Contains(valWithoutBraces))
                                    {
                                        uriIdentifiers.Remove(valWithoutBraces);
                                    }
                                }

                                // Identifiers from htmlUrl matches the silverlightUrl ones
                                if (uriIdentifiers.Count == 0)
                                {
                                    mappingHtmlPath = Regex.Escape(mappingHtmlPath).Replace(@"\{", "{");
                                    string convertedValue = _conversionRegex.Replace(mappingHtmlPath, "(?<$1>.*?)").Replace("{", String.Empty).Replace("}", String.Empty);
                                    mapping.HtmlUriRegex = new Regex("^" + convertedValue + "$", RegexOptions.IgnoreCase);
                                }
                            }
                        }
                    }
                }
            }

            return mappings;
        }
    }
}
