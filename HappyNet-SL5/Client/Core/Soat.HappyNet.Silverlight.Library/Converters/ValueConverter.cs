// -----------------------------------------------------------------------
// <copyright file="ValueConverter.cs" company="So@t">
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
using System.Windows.Data;

namespace Soat.HappyNet.Silverlight.Library.Converters
{
    /// <summary>
    /// Cette classe représente la classe de base pour convertir une valeur
    /// </summary>
    public abstract class ValueConverter : IValueConverter
    {
        #region Abstract Convert Method

        /// <summary>
        /// Cette Méthode abstraite permet d'effectuer la convertion
        /// </summary>
        /// <param name="value">Value a convertir</param>
        /// <param name="targetType">Type cible</param>
        /// <param name="parameter">parametre</param>
        /// <param name="culture">Culture pour la convertion</param>
        /// <returns>Objet Converti</returns>
        public abstract object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture);
        
        #endregion

        #region ConvertBack Method

        /// <summary>
        /// Cette Méthode abstraite permet d'effectuer la convertion inverse
        /// </summary>
        /// <param name="value">Value a convertir</param>
        /// <param name="targetType">Type cible</param>
        /// <param name="parameter">parametre</param>
        /// <param name="culture">Culture pour la convertion</param>
        /// <returns>Objet Converti</returns>
        public virtual object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        } 

        #endregion
    }
}
