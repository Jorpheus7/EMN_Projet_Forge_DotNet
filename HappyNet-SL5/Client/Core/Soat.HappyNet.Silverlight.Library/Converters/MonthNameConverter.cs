// -----------------------------------------------------------------------
// <copyright file="MonthNameConverter.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Globalization;
using Soat.HappyNet.Silverlight.Library.Helpers;

namespace Soat.HappyNet.Silverlight.Library.Converters
{
    public class MonthNameConverter : ValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                DateTime date = (DateTime)value;
                string month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(date.Month);
                if (month.EndsWith("."))
                {
                    month = month.Remove(month.Length - 1);
                }

                return GeneralHelper.UcFirst(month);
            }
            else if (value is int)
            {
                string month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName((int)value);
                if (month.EndsWith("."))
                {
                    month = month.Remove(month.Length - 1);
                }
                return GeneralHelper.UcFirst(month);
            }

            return null;
        }
    }
}
