// -----------------------------------------------------------------------
// <copyright file="BackOfficeModule.cs" company="So@t">
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
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using Soat.HappyNet.Silverlight.Modules.BackOffice.Services;
using Soat.HappyNet.Silverlight.Modules.BackOffice.Views;
using Soat.HappyNet.Silverlight.Infrastructure.Localization;
using Soat.HappyNet.Silverlight.Infrastructure.RegionNames;
using Soat.HappyNet.Silverlight.Infrastructure.UriNames;
using Soat.HappyNet.Silverlight.Modules.BackOffice.Controllers;
using Microsoft.Practices.Composite.Events;
using Soat.HappyNet.Silverlight.Infrastructure.Events;
using Soat.HappyNet.Silverlight.Library.Extensions;

namespace Soat.HappyNet.Silverlight.Modules.BackOffice
{
    public class BackOfficeModule : IModule
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
        public BackOfficeModule(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator)
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
        }

        private void InitializeController()
        {
            var controller = container.Resolve<IOrdersController>();
            controller.Run();
        }

        /// <summary>
        /// Registers types and services
        /// </summary>
        private void RegisterTypesAndServices()
        {
            // Services
            this.container.RegisterType<ISalesManagementService, SalesManagementService>(new ContainerControlledLifetimeManager());

            // Register Views / ViewModels
            this.container.RegisterType<IOrdersListView, OrdersListView>();
            this.container.RegisterType<IOrdersListViewModel, OrdersListViewModel>();

            this.container.RegisterType<IOrderDetailsView, OrderDetailsView>();
            this.container.RegisterType<IOrderDetailsViewModel, OrderDetailsViewModel>();

            this.container.RegisterType<IOrdersController, OrdersController>(new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// Register regions
        /// </summary>
        void RegisterRegions()
        {
            // Regions
            this.regionManager.RegisterViewWithRegion(RegionNames.OrdersListRegion, () => this.container.Resolve<IOrdersListViewModel>().View);
            this.regionManager.RegisterViewWithRegion(RegionNames.OrderDetailsRegion, () => this.container.Resolve<IOrderDetailsViewModel>().View);
        }

        #endregion
    }
}
