// -----------------------------------------------------------------------
// <copyright file="DebugConverter.cs" company="So@t">
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

namespace Soat.HappyNet.Silverlight.Library.Converters
{
    public class DebugConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value; // Add the breakpoint here!!
        }


        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException("This method should never be called");
        }
    }


}
