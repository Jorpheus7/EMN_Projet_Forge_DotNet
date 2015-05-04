// -----------------------------------------------------------------------
// <copyright file="EnumToBoolConverter.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace Soat.HappyNet.Silverlight.Library.Converters
{
    public class EnumToBoolConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value,
            Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            bool result = false;
            if (parameter != null)
            {
                if (parameter.Equals(value))
                    result = true;
                else
                    result = false;
            }

            if (targetType == typeof(Visibility))
            {
                return result ? Visibility.Visible : Visibility.Collapsed;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return parameter;

        }
        #endregion

    }
}
