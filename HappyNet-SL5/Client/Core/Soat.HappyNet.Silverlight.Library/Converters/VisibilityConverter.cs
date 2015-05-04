// -----------------------------------------------------------------------
// <copyright file="VisibilityConverter.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Soat.HappyNet.Silverlight.Library.Converters
{
    /// <summary>
    /// Cette classe représent un convertisseur Visibility
    /// </summary>
    public class VisibilityConverter : ValueConverter
    {
        /// <summary>
        /// Convert a boolean value to a Visibility value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter">ConverterParameter is of type Visibility</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool isVisible = (bool)value;

            // If visibility is inverted by the converter parameter, then invert our value
            if (IsVisibilityInverted(parameter))
                isVisible = !isVisible;

            return (isVisible ? Visibility.Visible : Visibility.Collapsed);
        }

        /// <summary>
        /// Support 2-way databinding of the VisibilityConverter, converting Visibility to a boolean
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool isVisible = ((Visibility)value == Visibility.Visible);

            // If visibility is inverted by the converter parameter, then invert our value
            if (IsVisibilityInverted(parameter))
                isVisible = !isVisible;

            return isVisible;
        }

        /// <summary>
        /// Determine the visibility mode based on a converter parameter.  This parameter is of type Visibility,
        /// and specifies what visibility value to return when the boolean value is true.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private static Visibility GetVisibilityMode(object parameter)
        {
            // Default to Visible
            Visibility mode = Visibility.Visible;

            // If a parameter is specified, then we'll try to understand it as a Visibility value
            if (parameter != null)
            {
                // If it's already a Visibility value, then just use it
                if (parameter is Visibility)
                {
                    mode = (Visibility)parameter;
                }
                else
                {
                    // Let's try to parse the parameter as a Visibility value, throwing an exception when the parsing fails
                    try
                    {
                        mode = (Visibility)Enum.Parse(typeof(Visibility), parameter.ToString(), true);
                    }
                    catch (FormatException e)
                    {
                        throw new FormatException("Invalid Visibility specified as the ConverterParameter.  Use Visible or Collapsed.", e);
                    }
                }
            }

            // Return the detected mode
            return mode;
        }

        /// <summary>
        /// Determine whether or not visibility is inverted based on a converter parameter.
        /// When the parameter is specified as Collapsed, that means that when the boolean value
        /// is true, we should return Collapsed, which is inverted.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private static bool IsVisibilityInverted(object parameter)
        {
            return (GetVisibilityMode(parameter) == Visibility.Collapsed);
        }
    }
}
