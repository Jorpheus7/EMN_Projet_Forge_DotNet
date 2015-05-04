// -----------------------------------------------------------------------
// <copyright file="DecimalConverter.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Soat.HappyNet.Silverlight.Library.Converters
{
    public class DecimalConverter : ValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is decimal)
            {
                string.Format("{0:0.00}", value);
            }
            else if (value is string)
            {
                decimal result = 0;
                Decimal.TryParse((string)value, out result);
                return result;
            }

            return null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is decimal)
            {
                string.Format("{0:0.00}", value);
            }
            else if (value is string)
            {
                decimal result = 0;
                Decimal.TryParse((string)value, out result);
                return result;
            }

            return null;
        }
    }
}
