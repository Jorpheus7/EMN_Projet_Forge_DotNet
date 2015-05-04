// -----------------------------------------------------------------------
// <copyright file="ImageBrushConverter.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Windows.Media;

namespace Soat.HappyNet.Silverlight.Library.Converters
{
    public class ImageBrushConverter : ValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is ImageSource)
            {
                ImageBrush img = new ImageBrush();
                img.ImageSource = value as ImageSource;

                Stretch stretch = Stretch.Uniform;
                if (parameter != null)
                {
                    stretch = (Stretch)Enum.Parse(typeof(Stretch), parameter.ToString(), true);
                }

                img.Stretch = stretch;

                return img;
            }

            return null;
        }
    }
}
