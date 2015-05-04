// -----------------------------------------------------------------------
// <copyright file="IShoppingCartView.cs" company="So@t">
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
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Soat.HappyNet.Silverlight.Modules.SalesOrder.Views
{
    /// <summary>
    /// Interface defining the ShoppingCart View
    /// </summary>
    public interface IShoppingCartView
    {
        /// <summary>
        /// ViewModel attached to the View
        /// </summary>
        IShoppingCartViewModel Model { get; set; }

        /// <summary>
        /// Displays the shopping cart
        /// </summary>
        void ShowShoppingCart();

        /// <summary>
        /// Hides the shopping cart
        /// </summary>
        void HideShoppingCart();
    }
}
