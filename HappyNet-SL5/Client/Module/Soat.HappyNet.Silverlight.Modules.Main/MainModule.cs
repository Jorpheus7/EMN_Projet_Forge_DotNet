// -----------------------------------------------------------------------
// <copyright file="MainModule.cs" company="So@t">
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
using Soat.HappyNet.Silverlight.Modules.Main.Controllers;
using Soat.HappyNet.Silverlight.Modules.Main.Services;
using Soat.HappyNet.Silverlight.Modules.Main.Views;
using Soat.HappyNet.Server.DataContract;
using Soat.HappyNet.Silverlight.Infrastructure.Models;
using Soat.HappyNet.Silverlight.Infrastructure.RegionNames;
using Soat.HappyNet.Silverlight.Infrastructure.UriNames;
using Soat.HappyNet.Silverlight.Infrastructure.Localization;
using Microsoft.Practices.Composite.Events;
using Soat.HappyNet.Silverlight.Infrastructure.Events;
using Soat.HappyNet.Silverlight.Library.Extensions;

namespace Soat.HappyNet.Silverlight.Modules.Main
{
    /// <summary>
    /// Class representing the main module
    /// </summary>
    public class MainModule : IModule
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
        public MainModule(IUnityContainer container, 
            IRegionManager regionManager,
            IEventAggregator eventAggregator)
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
                NameProvider = () => StringLibrary.MENU_Home,
                Url = UriNames.HomeUri
            });
        }

        /// <summary>
        /// Initializes and runs the module controller
        /// </summary>
        private void InitializeController()
        {
            IMainController controller = container.Resolve<IMainController>();
            controller.Run();
        }

        /// <summary>
        /// Registers types and services
        /// </summary>
        private void RegisterTypesAndServices()
        {
            this.container.RegisterType<IMainController, MainController>(new ContainerControlledLifetimeManager());
            this.container.RegisterType<IMainService, MainService>(new ContainerControlledLifetimeManager());

            // Register Views / ViewModels
            this.container.RegisterType<IMenuItemView, MenuItemView>();
            this.container.RegisterType<IMenuItemViewModel, MenuItemViewModel>();

            this.container.RegisterType<ILoginStateView, LoginStateView>();
            this.container.RegisterType<ILoginStateViewModel, LoginStateViewModel>();

            this.container.RegisterType<ILanguageSwitchView, LanguageSwitchView>();
            this.container.RegisterType<ILanguageSwitchViewModel, LanguageSwitchViewModel>();

            this.container.RegisterType<INotificationsView, NotificationsView>();
            this.container.RegisterType<INotificationsViewModel, NotificationsViewModel>();

            this.container.RegisterType<IMenuView, MenuView>();
            this.container.RegisterType<IMenuViewModel, MenuViewModel>();

            // Register a default instance for the language
            this.container.RegisterInstance<Language>(new Language());
        }

        /// <summary>
        /// Registers regions
        /// </summary>
        void RegisterRegions()
        {
            // Regions
            this.regionManager.RegisterViewWithRegion(RegionNames.LoginStateRegion, () => this.container.Resolve<ILoginStateViewModel>().View);

            this.regionManager.RegisterViewWithRegion(RegionNames.LanguageSwitchRegion, () => this.container.Resolve<ILanguageSwitchViewModel>().View);

            this.regionManager.RegisterViewWithRegion(RegionNames.NotificationsRegion, () => this.container.Resolve<INotificationsViewModel>().View);

            this.regionManager.RegisterViewWithRegion(RegionNames.MenuRegion, () => this.container.Resolve<IMenuViewModel>().View);
        }

        #endregion
    }
}
