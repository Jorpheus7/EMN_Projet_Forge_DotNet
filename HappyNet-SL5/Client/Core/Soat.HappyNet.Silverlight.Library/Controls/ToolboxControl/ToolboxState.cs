// -----------------------------------------------------------------------
// <copyright file="ToolboxState.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Soat.HappyNet.Silverlight.Library.Controls
{
    /// <summary>
    /// Liste des états possible de la toolbox
    /// </summary>
    public enum ToolboxState
    {
        /// <summary>
        /// Affichage normal avec Drag And Drop
        /// </summary>
        Normal,

        /// <summary>
        /// Affichage sur toute la hauteur de l'écran
        /// </summary>
        Auto,

        /// <summary>
        /// Cache le menu sur la gauche ou a droite
        /// </summary>
        Hidden
        
    }
}
