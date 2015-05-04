// -----------------------------------------------------------------------
// <copyright file="SalesOrderModule.cs" company="So@t">
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
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Composite.Regions;
using Soat.HappyNet.Silverlight.Modules.SalesOrder.Controllers;
using Soat.HappyNet.Silverlight.Modules.SalesOrder.Services;
using Soat.HappyNet.Silverlight.Modules.SalesOrder.Views;
using Soat.HappyNet.Silverlight.Modules.SalesOrder.Models;
using Soat.HappyNet.Silverlight.Infrastructure.Localization;
using Soat.HappyNet.Silverlight.Infrastructure.UriNames;
using Soat.HappyNet.Silverlight.Infrastructure.RegionNames;
using Microsoft.Practices.Composite.Events;
using Soat.HappyNet.Silverlight.Library.Extensions;
using Soat.HappyNet.Silverlight.Infrastructure.Events;

namespace Soat.HappyNet.Silverlight.Modules.SalesOrder
{
    /// <summary>
    /// Class representing the sales module
    /// </summary>
    public class SalesOrderModule : IModule
    {
        #region Private Members

        /// <summary>
        /// Unity container
        /// </summary>
        private readonly IUnityContainer container;

        /// <summary>
        /// Region manager
        /// </summary>
        private readonly IRegionManager regionManager;

        /// <summary>
        /// Event aggregator
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="container">Unity container</param>
        /// <param name="regionManager">Region manager</param>
        public SalesOrderModule(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            this.container = container;
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Initiliazes the module
        /// </summary>
        public void Initialize()
        {
            this.RegisterTypesAndServices();

            this.RegisterRegions();

            this.InitializeController();

            this.InitializeMenuItem();
        }

        private void InitializeMenuItem()
        {
            this.eventAggregator.Publish(new AddMenuButtonEvent()
            {
                NameProvider = () => StringLibrary.MENU_HumanResources,
                Url = UriNames.EmployeesUri
            });
        }

        /// <summary>
        /// Initializes and runs the module controller
        /// </summary>
        private void InitializeController()
        {
            ISalesOrderController controller = container.Resolve<ISalesOrderController>();
            this.container.RegisterInstance<ISalesOrderController>(controller);

            controller.Run();
        }

        /// <summary>
        /// Registers types and services
        /// </summary>
        private void RegisterTypesAndServices()
        {
            // Register Services
            this.container.RegisterType<ISalesOrderController, SalesOrderController>(new ContainerControlledLifetimeManager());
            this.container.RegisterType<ISalesOrderService, SalesOrderService>(new ContainerControlledLifetimeManager());

            // Register Views / ViewModels
            this.container.RegisterType<IShoppingCartView, ShoppingCartView>();
            this.container.RegisterType<IShoppingCartViewModel, ShoppingCartViewModel>();

            // Register a default instance for the shopping cart
            this.container.RegisterInstance<ShoppingCart>(new ShoppingCart());
        }

        /// <summary>
        /// Register regions
        /// </summary>
        void RegisterRegions()
        {
            // Regions
            this.regionManager.RegisterViewWithRegion(RegionNames.ShoppingCartRegion, () => this.container.Resolve<IShoppingCartViewModel>().View);
        }

        #endregion
    }
}
