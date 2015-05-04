// -----------------------------------------------------------------------
// <copyright file="ShoppingCartViewModel.cs" company="So@t">
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
using System.ComponentModel;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Presentation.Commands;
using Soat.HappyNet.Silverlight.Library;
using Soat.HappyNet.Silverlight.Infrastructure.Events;
using Soat.HappyNet.Silverlight.Library.Extensions;
using System.Collections.ObjectModel;
using Soat.HappyNet.Silverlight.Library.Commands;
using Soat.HappyNet.Silverlight.Modules.SalesOrder.SalesOrderServiceReference;
using Soat.HappyNet.Silverlight.Modules.SalesOrder.Models;
using Soat.HappyNet.Silverlight.Infrastructure.Localization;
using Soat.HappyNet.Silverlight.Infrastructure;

namespace Soat.HappyNet.Silverlight.Modules.SalesOrder.Views
{
    /// <summary>
    /// Class defining the ViewModel for the ShoppingCart View
    /// </summary>
    public class ShoppingCartViewModel : ViewModel<IShoppingCartView>, IShoppingCartViewModel
    {
        #region Private members

        /// <summary>
        /// Event aggregator
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        /// <summary>
        /// Unity container
        /// </summary>
        private readonly IUnityContainer container;

        #endregion

        #region Properties

        private decimal totalPrice;
        /// <summary>
        /// Total price
        /// </summary>
        public decimal TotalPrice
        {
            get { return totalPrice; }
            set
            {
                if (totalPrice != value)
                {
                    totalPrice = value;
                    Notify(() => this.TotalPrice);
                }
            }
        }

        /// <summary>
        /// Shopping cart items
        /// </summary>
        public ObservableCollection<ShoppingCartItem> ShoppingCartItems { get; private set; }

        #endregion

        #region Commands

        /// <summary>
        /// Command to checkout
        /// </summary>
        public ICommand CheckoutCommand { get; private set; }

        /// <summary>
        /// Command to delete an item from the cart
        /// </summary>
        public ICommand RemoveCartItemCommand { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="view">View attached to this ViewModel</param>
        /// <param name="eventAggregator">Event aggregator</param>
        public ShoppingCartViewModel(IShoppingCartView view,
              IEventAggregator eventAggregator,
              IUnityContainer container)
        {
            ShoppingCartItems = new ObservableCollection<ShoppingCartItem>();

            this.container = container;
            this.eventAggregator = eventAggregator;

            this.InitializeCommands();
            this.InitializeCart();

            this.View = view;
            this.View.Model = this;

            eventAggregator.Subscribe<AddToCartEvent>(OnAddToCart);
        }

        #endregion

        #region Initialize Methods

        /// <summary>
        /// Initializes the commands
        /// </summary>
        void InitializeCommands()
        {
            CheckoutCommand = new DelegateCommand(Checkout, 
                () => ShoppingCartItems.Count > 0);

            RemoveCartItemCommand = new DelegateCommand<ShoppingCartItem>(RemoveCartItem);
        }

        void InitializeCart()
        {
            // Gets the global instance of the shopping cart
            ShoppingCartItems = this.container.Resolve<ShoppingCart>().Items;
            ShoppingCartItems.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(OnShoppingCartItemsCollectionChanged);
        }

        void OnShoppingCartItemsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ((DelegateCommand)CheckoutCommand).RaiseCanExecuteChanged();
        }

        #endregion

        #region Gestion panier d'achat

        public void OnAddToCart(AddToCartEvent args)
        {
            // The new product for the shopping cart is added by the controller
            // As there's only 1 instance of the shoppingCart for the whole app,
            // no need to add it here, we just show the cart
            this.View.ShowShoppingCart();
        }

        /// <summary>
        /// Deletes a product from the cart
        /// </summary>
        /// <param name="cartItem">Cart item</param>
        void RemoveCartItem(ShoppingCartItem cartItem)
        {
            if (ShoppingCartItems.Contains(cartItem))
            {
                ShoppingCartItems.Remove(cartItem);

                this.eventAggregator.Publish(new NotificationEvent(NotificationType.Validation, Messages.OK_ProductDeletedFromShoppingCart));
            }
        }

        /// <summary>
        /// Proceeds to the checkout of the shopping cart
        /// </summary>
        void Checkout()
        {
            // Is the user logged in ?
            if (Globals.IsLoggedIn)
            {
                this.eventAggregator.Publish(new NotificationEvent(NotificationType.Validation, Messages.OK_CheckedOut));

                ShoppingCartItems.Clear();
            }
            else
            {
                this.eventAggregator.Publish(new MessageShowEvent
                {
                    Message = Messages.ERR_PleaseAuthenticate
                });
            }
        }

        #endregion

        #region ICleanable

        /// <summary>
        /// Cleans to avoid memory leaks ...
        /// </summary>
        public override void Clean()
        {
            base.Clean();

            ShoppingCartItems.CollectionChanged -= new System.Collections.Specialized.NotifyCollectionChangedEventHandler(OnShoppingCartItemsCollectionChanged);
            
            //!\ Memory leak if the binding is left on the global instance of the ShoppingCart
            ShoppingCartItems = null;
            Notify(() => this.ShoppingCartItems);
        }

        #endregion
    }
}
