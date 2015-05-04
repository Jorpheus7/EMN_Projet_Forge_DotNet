// -----------------------------------------------------------------------
// <copyright file="CurrencyConverter.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Globalization;

namespace Soat.HappyNet.Silverlight.Library.Converters
{
    public class CurrencyConverter : ValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "0.00 €";

            decimal currency = (decimal)value;
            //return currency.ToString("0.00 €");
            return string.Format("{0:C}", currency);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string formattedValue = value.ToString().TrimEnd('€');
            decimal currency;

            if (decimal.TryParse(formattedValue, out currency))
            {
                return currency;
            }

            return value;
        }
    }
}
