// -----------------------------------------------------------------------
// <copyright file="SalesOrderController.cs" company="So@t">
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
using Microsoft.Practices.Unity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Composite.Events;
using Soat.HappyNet.Silverlight.Infrastructure.RegionNames;
using Soat.HappyNet.Silverlight.Infrastructure.UriNames;
using Soat.HappyNet.Silverlight.Infrastructure.Localization;
using Soat.HappyNet.Silverlight.Modules.SalesOrder.Views;
using Soat.HappyNet.Silverlight.Library.Extensions;
using Soat.HappyNet.Silverlight.Infrastructure.Events;
using Soat.HappyNet.Silverlight.Modules.SalesOrder.SalesOrderServiceReference;
using System.Collections.ObjectModel;
using Soat.HappyNet.Silverlight.Modules.SalesOrder.Models;
using System.Linq;

namespace Soat.HappyNet.Silverlight.Modules.SalesOrder.Controllers
{
    /// <summary>
    /// Class defining a controller for sales
    /// </summary> 
    public class SalesOrderController : ISalesOrderController
    {
        #region Private Members

        /// <summary>
        /// Unity container
        /// </summary>
        private readonly IUnityContainer container;
        /// <summary>
        /// Event aggregator
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        /// <summary>
        /// Region Manager
        /// </summary>
        private readonly IRegionManager regionManager;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="regionManager">Region manager</param>
        /// <param name="container">Container Unity</param>
        /// <param name="eventAggregator">Event aggregator</param>
        public SalesOrderController(IUnityContainer container, 
            IEventAggregator eventAggregator,
            IRegionManager regionManager)
        {
            this.eventAggregator = eventAggregator;
            this.container = container;
            this.regionManager = regionManager;
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Executes the controller
        /// </summary>
        public void Run()
        {
            // We register the controller to the AddToCart event so it adds
            // new products to the cart wherever we are in the application
            // (the controller is a singleton instanciated at the application startup)
            eventAggregator.Subscribe<AddToCartEvent>(OnAddToCart);

            // Subscribes to authentification event to display (or not) the button menu to access orders
            //eventAggregator.Subscribe<LoginChangedEvent>(OnLoginChanged);
        }

        #endregion

        /// <summary>
        /// Adds the new product to the global instance of the shopping cart
        /// </summary>
        /// <param name="args"></param>
        public void OnAddToCart(AddToCartEvent args)
        {
            var cart = this.container.Resolve<ShoppingCart>();

            var product = cart.Items.Where(p => p.Product.ProductID == args.ProductID).FirstOrDefault();

            if (product != null)
            {
                // If the product is already in the cart, need to increment the quantity
                product.Quantity += args.Quantity;
            }
            else
            {
                cart.Items.Add(new ShoppingCartItem
                {
                    Product = new Product
                    {
                        ProductID = args.ProductID,
                        Name = args.ProductName,
                        Price = args.Price,
                        Currency = args.Currency
                    },
                    Quantity = args.Quantity
                });
            }
        }

        public void OnLoginChanged(LoginChangedEvent args)
        {
            if (args.IsLogged)
            {
                this.eventAggregator.Publish(new AddMenuButtonEvent()
                {
                    NameProvider = () => StringLibrary.MENU_Orders,
                    Url = UriNames.SalesOrderUri
                });
            }
            else
            {
                this.eventAggregator.Publish(new RemoveMenuButtonEvent()
                {
                    Url = UriNames.SalesOrderUri
                });
            }
        }
    }
}
