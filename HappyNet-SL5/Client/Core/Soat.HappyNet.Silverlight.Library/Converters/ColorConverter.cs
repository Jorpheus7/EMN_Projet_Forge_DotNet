// -----------------------------------------------------------------------
// <copyright file="ColorConverter.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Windows.Media;
using Soat.HappyNet.Silverlight.Library.Helpers;

namespace Soat.HappyNet.Silverlight.Library.Converters
{
    public class ColorConverter : ValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is String)
            {
                if (value.ToString().StartsWith("#"))
                {
                    Color c = GeneralHelper.GetColorFromHex(value.ToString());
                    return new SolidColorBrush(c);
                }

                try
                {
                    Color c = GeneralHelper.GetColorFromString(value as string);
                    return new SolidColorBrush(c);
                }
                catch
                {
                    return null;
                }
            }
            else if (value is Color)
            {
                return new SolidColorBrush((Color)value);
            }

            return null;
        }
    }
}
