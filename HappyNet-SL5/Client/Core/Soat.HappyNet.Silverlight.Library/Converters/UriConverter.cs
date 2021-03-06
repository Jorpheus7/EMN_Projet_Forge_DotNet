﻿// -----------------------------------------------------------------------
// <copyright file="UriConverter.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Soat.HappyNet.Silverlight.Library.Converters
{
    public class UriConverter : ValueConverter
    {
        #region Methods

        /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        /// <param name="value">The source data being passed to the target.</param>
        /// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the target dependency property.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>
        /// The Uri to be passed to the target dependency property.
        /// </returns>
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            Uri sourceUri;
            if (value is Uri)
            {
                sourceUri = (Uri)value;
            }
            else if (value is string)
            {
                // Create sourceUri
                string stringValue = value as string;
                sourceUri = new Uri(stringValue, UriKind.RelativeOrAbsolute);
            }
            else
            {
                return null;
            }

            // We got an abosulte Uri we do not need to modify it.
            if (sourceUri.IsAbsoluteUri)
            {
                return sourceUri;
            }

            // No parameter return the sourceUri
            if (parameter == null)
            {
                return sourceUri;
            }

            if (parameter is string)
            {
                // There a string as a parameter
                Uri uriParameter;

                // Check if it's an Uri
                if (Uri.TryCreate(parameter as string, UriKind.RelativeOrAbsolute, out uriParameter))
                {
                    // Apply our parameter Uri to the sourceUri
                    return new Uri(uriParameter, sourceUri);
                }
            }
            else if (parameter is Uri)
            {
                // Apply our parameter Uri to the sourceUri
                return new Uri((Uri)parameter, sourceUri);
            }

            return null;
        }

        /// <summary>
        /// Modifies the target data before passing it to the source object.
        /// </summary>
        /// <param name="value">The target data being passed to the source.</param>
        /// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the source object.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>
        /// The value to be passed to the source object.
        /// </returns>
        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Uri uri = value as Uri;
            if (uri == null)
            {
                return null;
            }

            if (targetType != typeof(string)
                && targetType != typeof(Uri))
            {
                return null;
            }

            // No parameter return the sourceUri
            if (parameter == null)
            {
                return ConvertBackType(targetType, uri);
            }

            if (parameter is string)
            {
                // There a string as a parameter
                Uri uriParameter;

                // Check if it's an Uri
                if (Uri.TryCreate(parameter as string, UriKind.RelativeOrAbsolute, out uriParameter))
                {
                    // Apply our parameter Uri
                    return ConvertBackType(targetType, uriParameter.MakeRelativeUri(uri));
                }
            }
            else if (parameter is Uri)
            {
                // Apply our parameter Uri to the sourceUri
                return ConvertBackType(targetType, ((Uri)parameter).MakeRelativeUri(uri));
            }

            return null;
        }

        /// <summary>
        /// Handle convert back type convertion
        /// </summary>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="uri">The URI to convert.</param>
        /// <returns>the converted value</returns>
        private static object ConvertBackType(Type targetType, Uri uri)
        {
            if (targetType == typeof(Uri))
            {
                return uri;
            }

            return uri.ToString();
        }

        #endregion Methods
    }

}
