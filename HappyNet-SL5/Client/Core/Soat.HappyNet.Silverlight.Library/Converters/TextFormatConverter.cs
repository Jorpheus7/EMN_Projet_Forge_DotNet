// -----------------------------------------------------------------------
// <copyright file="TextFormatConverter.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Soat.HappyNet.Silverlight.Library.Converters
{
    public class TextFormatConverter : ValueConverter
    {

        #region IValueConverter Members

        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Format(value, parameter);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Format(value, parameter);
        }

        object Format(object value, object param)
        {
            if (value == null)
                return value;
            int ri;
            double rd;
            decimal rdd;

            if (int.TryParse(value.ToString(), out ri))
            {
                return string.Format(param.ToString(), ri);
            }
            else if (decimal.TryParse(value.ToString(), out rdd))
            {
                return string.Format(param.ToString(), rdd);
            }
            else if (double.TryParse(value.ToString(), out rd))
            {
                return string.Format(param.ToString(), rd);
            }
            else
            {
                return string.Format(param.ToString(), value);
            }
        }

        #endregion
    }
}