// -----------------------------------------------------------------------
// <copyright file="PageMappingInfo.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace Soat.HappyNet.WebSite
{
    /// <summary>
    /// Class describing page mapping information
    /// </summary>
    public class PageMappingInfo
    {
        private string _htmlUrl = "";
        private string _silverlightUrl = "";
        private Regex htmlUriRegex;
        private Regex silverlightUriRegex;
        private int _order = 0;
        private string _priority = "0.0";
        private string _changeFrequency = "";
        private System.DateTime _lastModified = System.DateTime.MinValue;
        private Dictionary<string, string> _query = new Dictionary<string,string>();

        /// <summary>
        /// Change frequency
        /// </summary>
        public string ChangeFrequency
        {
            get { return _changeFrequency; }
            set { _changeFrequency = value; }
        }

        /// <summary>
        /// Html url
        /// </summary>
        public string HtmlUrl
        {
            get { return _htmlUrl; }
            set { _htmlUrl = value; }
        }

        /// <summary>
        /// Silverlight url
        /// </summary>
        public string SilverlightUrl
        {
            get { return _silverlightUrl; }
            set { _silverlightUrl = value; }
        }

        /// <summary>
        /// Regex used to find queries in htmlUrl
        /// </summary>
        public Regex HtmlUriRegex
        {
            get { return htmlUriRegex; }
            set { htmlUriRegex = value; }
        }

        /// <summary>
        /// Regex used to find queries in silverlightUrl
        /// </summary>
        public Regex SilverlightUriRegex
        {
            get { return silverlightUriRegex; }
            set { silverlightUriRegex = value; }
        }

        /// <summary>
        /// Date time for last modification
        /// </summary>
        public System.DateTime LastModified
        {
            get { return _lastModified; }
            set { _lastModified = value; }
        }

        /// <summary>
        /// Order
        /// </summary>
        public int Order
        {
            get { return _order; }
            set { _order = value; }
        }

        /// <summary>
        /// Priority
        /// </summary>
        public string Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        /// <summary>
        /// Queries
        /// </summary>
        public Dictionary<string, string> Query
        {
            get { return _query; }
            set { _query = value; }
        }
    }
}
