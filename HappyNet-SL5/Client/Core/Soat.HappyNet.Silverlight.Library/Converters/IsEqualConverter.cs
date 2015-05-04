// -----------------------------------------------------------------------
// <copyright file="IsEqualConverter.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soat.HappyNet.Silverlight.Library.Converters
{
    public class IsEqualConverter : ValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && parameter != null)
            {
                return string.Compare(value.ToString(), parameter.ToString()) == 0;
            }

            if (targetType == typeof(bool))
            {
                return false;
            }

            return null;
        }
    }
}
