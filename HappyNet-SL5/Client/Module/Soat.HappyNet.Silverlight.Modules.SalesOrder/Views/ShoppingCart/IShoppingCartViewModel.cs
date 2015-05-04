// -----------------------------------------------------------------------
// <copyright file="IShoppingCartViewModel.cs" company="So@t">
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
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Presentation.Commands;
using System.Collections.ObjectModel;
using Soat.HappyNet.Silverlight.Modules.SalesOrder.SalesOrderServiceReference;

namespace Soat.HappyNet.Silverlight.Modules.SalesOrder.Views
{
    /// <summary>
    /// Interface defining the ViewModel for the ShoppingCart View
    /// </summary>
    public interface IShoppingCartViewModel
    {
        /// <summary>
        /// View
        /// </summary>
        IShoppingCartView View { get; }

        /// <summary>
        /// Total price
        /// </summary>
        decimal TotalPrice { get; }

        /// <summary>
        /// Shopping cart items
        /// </summary>
        ObservableCollection<ShoppingCartItem> ShoppingCartItems { get; }

        /// <summary>
        /// Command to checkout
        /// </summary>
        ICommand CheckoutCommand { get; }

        /// <summary>
        /// Command to delete an item from the cart
        /// </summary>
        ICommand RemoveCartItemCommand { get; }
    }
}
