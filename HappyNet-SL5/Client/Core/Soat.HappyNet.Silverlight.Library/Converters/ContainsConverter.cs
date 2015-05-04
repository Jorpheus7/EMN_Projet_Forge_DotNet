// -----------------------------------------------------------------------
// <copyright file="ContainsConverter.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Windows;

namespace Soat.HappyNet.Silverlight.Library.Converters
{
    public class ContainsConverter : ValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool result = false;

            if (value is IList)
            {
                result = ((IList)value).Contains(parameter);
            }

            if (targetType == typeof(Visibility))
                return result ? Visibility.Visible : Visibility.Collapsed;

            if (targetType == typeof(bool))
                return result;

            return Visibility.Collapsed;
        }
    }
}
