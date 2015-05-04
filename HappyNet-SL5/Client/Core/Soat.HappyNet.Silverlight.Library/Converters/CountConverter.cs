// -----------------------------------------------------------------------
// <copyright file="CountConverter.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Globalization;
using System.Windows;

namespace Soat.HappyNet.Silverlight.Library.Converters
{
    public class CountConverter : ValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = false;

            if (value != null)
            {
                if (value is ICollection)
                {
                    result = ((ICollection)value).Count > 0;
                }
                else if (value is IList)
                {
                    result = ((IList)value).Count > 0;
                }
            }

            if (targetType == typeof(Visibility))
                return result ? Visibility.Visible : Visibility.Collapsed;

            if (targetType == typeof(bool))
                return result;

            return Visibility.Collapsed;
        }
    }
}
