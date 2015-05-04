// -----------------------------------------------------------------------
// <copyright file="MailToConverter.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Globalization;

namespace Soat.HappyNet.Silverlight.Library.Converters
{
    /// <summary>
    /// Formattage de texte au format mailto
    /// </summary>
    public class MailToConverter : ValueConverter
    {
        /// <summary>
        /// Conversion au format mailto Uri
        /// </summary>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is String)
            {
                string mailto = String.Format("mailto:{0}", value);
                return new Uri(mailto);
            }

            return null;
        }
    }
}
