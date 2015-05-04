// -----------------------------------------------------------------------
// <copyright file="DateTimeConverter.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Globalization;

namespace Soat.HappyNet.Silverlight.Library.Converters
{
    /// <summary>
    /// Formattage de date au format "ShortDate"
    /// </summary>
    public class DateTimeConverter : ValueConverter
    {
        /// <summary>
        /// Conversion au format string
        /// </summary>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            DateTime date;
            if (value is string)
            {
                if (!DateTime.TryParse(value.ToString(), out date))
                {
                    return value.ToString();
                }
            }
            else if (value is DateTime)
            {
                date = (DateTime)value;
            }
            else
            {
                return string.Empty;
            }

            string mode = string.Empty;
            if (parameter is string 
                && !string.IsNullOrEmpty(parameter.ToString()))
            {
                mode = parameter.ToString();
            }
            else
            {
                mode = "d";
            }

            if (date.Date == DateTime.MinValue.Date)
            {
                return null;
            }
            else
            {
                return date.ToString(mode, CultureInfo.CurrentUICulture);
            }
        }

        /// <summary>
        /// Reconversion au format datetime
        /// </summary>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value.ToString();
            DateTime resultDateTime;
            if (DateTime.TryParse(strValue, out resultDateTime))
            {
                return resultDateTime;
            }
            return value;
        }
    }
}
