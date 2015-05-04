// -----------------------------------------------------------------------
// <copyright file="FormatConverter.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Soat.HappyNet.Silverlight.Library.Converters
{
    /// <summary>
    /// Formattage générique de texte en prenant une variable en argument
    /// </summary>
    /// <example>
    /// Text="{Binding Salaire, Converter={StaticResource FormatConverter}, ConverterParameter='{0} €' }"
    /// </example>
    public class FormatConverter : ValueConverter
    {
        /// <summary>
        /// Formattage des données
        /// </summary>
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter is string)
            {
                return String.Format((string)parameter, value);
            }
            return null;
        }
    }    
}
