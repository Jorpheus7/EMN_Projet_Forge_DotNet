// -----------------------------------------------------------------------
// <copyright file="XElementExtensions.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

namespace Soat.HappyNet.Silverlight.Library.Extensions
{
    using System;
    using System.Globalization;
    using System.Xml.Linq;

    public static class XElementExtensions
    {
        #region Methods

        public static string GetAttribute(this XElement elem, XName attributeName)
        {
            XAttribute attribute = elem.Attribute(attributeName);
            if (attribute != null)
                return attribute.Value;
            return null;
        }

        public static string GetAttribute(this XElement elem, XName elementName, XName attributeName)
        {
            XElement childElement = elem.Element(elementName);
            if (childElement == null)
                return null;

            XAttribute attribute = childElement.Attribute(attributeName);
            if (attribute != null)
                return attribute.Value;
            return null;
        }

        public static bool? GetBoolAttribute(this XElement elem, XName attributeName)
        {
            string val = GetAttribute(elem, attributeName);
            bool result;
            if (bool.TryParse(val, out result))
                return result;
            return null;
        }

        public static bool? GetBoolAttribute(this XElement elem, XName elementName, XName attributeName)
        {
            string val = GetAttribute(elem, elementName, attributeName);
            bool result;
            if (bool.TryParse(val, out result))
                return result;
            return null;
        }

        public static double? GetDoubleAttribute(this XElement elem, XName attributeName)
        {
            string val = GetAttribute(elem, attributeName);
            double result;
            if (double.TryParse(val, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                return result;
            return null;
        }

        public static double? GetDoubleAttribute(this XElement elem, XName elementName, XName attributeName)
        {
            string val = GetAttribute(elem, elementName, attributeName);
            double result;
            if (double.TryParse(val, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                return result;
            return null;
        }

        public static string GetElementValue(this XElement elem, XName elementName)
        {
            XElement childElement = elem.Element(elementName);
            if (childElement != null)
                return childElement.Value;
            return null;
        }

        public static bool? GetElementBoolValue(this XElement elem, XName elementName)
        {
            string val = elem.GetElementValue(elementName);
            bool result;
            if (bool.TryParse(val, out result))
                return result;

            return null;
        }

        public static string GetElementValue<T>(this XElement elem, XName elementName)
        {
            XElement childElement = elem.Element(elementName);
            if (childElement != null)
                return childElement.Value;
            return null;
        }

        public static int? GetIntAttribute(this XElement elem, XName attributeName)
        {
            string val = GetAttribute(elem, attributeName);
            int result;
            if (int.TryParse(val, out result))
                return result;
            return null;
        }

        public static int? GetIntAttribute(this XElement elem, XName elementName, XName attributeName)
        {
            string val = GetAttribute(elem, elementName, attributeName);
            int result;
            if (int.TryParse(val, out result))
                return result;
            return null;
        }

        public static TimeSpan? GetTimeSpanAttribute(this XElement elem, XName attributeName)
        {
            string val = GetAttribute(elem, attributeName);
            TimeSpan result;
            if (TimeSpan.TryParse(val, out result))
                return result;
            return null;
        }

        public static TimeSpan? GetTimeSpanAttribute(this XElement elem, XName elementName, XName attributeName)
        {
            string val = GetAttribute(elem, elementName, attributeName);
            TimeSpan result;
            if (TimeSpan.TryParse(val, out result))
                return result;
            return null;
        }

        #endregion Methods
    }
}