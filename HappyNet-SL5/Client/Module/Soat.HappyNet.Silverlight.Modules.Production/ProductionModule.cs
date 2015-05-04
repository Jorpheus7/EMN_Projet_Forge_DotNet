// -----------------------------------------------------------------------
// <copyright file="ProductionModule.cs" company="So@t">
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
using Soat.HappyNet.Silverlight.Modules.Production.Services;
using Soat.HappyNet.Silverlight.Modules.Production.Views;
using Microsoft.Practices.Composite.Events;
using Soat.HappyNet.Silverlight.Infrastructure.Localization;
using Soat.HappyNet.Silverlight.Infrastructure.UriNames;
using Soat.HappyNet.Silverlight.Infrastructure.RegionNames;
using Soat.HappyNet.Silverlight.Infrastructure.Events;
using Soat.HappyNet.Silverlight.Library.Extensions;

namespace Soat.HappyNet.Silverlight.Modules.Production
{
    /// <summary>
    /// Class representing the production module
    /// </summary>
    public class ProductionModule : IModule
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
        public ProductionModule(IUnityContainer container,
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

            this.InitializeMenuItem();
        }

        private void InitializeMenuItem()
        {
            this.eventAggregator.Publish(new AddMenuButtonEvent()
            {
                NameProvider = () => StringLibrary.MENU_Production,
                Url = UriNames.ShoppingUri
            });
        }

        /// <summary>
        /// Registers types and services
        /// </summary>
        private void RegisterTypesAndServices()
        {
            // Services
            this.container.RegisterType<IProductionService, ProductionService>(new ContainerControlledLifetimeManager());

            // ProductsList
            this.container.RegisterType<IProductsListView, ProductsListView>();
            this.container.RegisterType<IProductsListViewModel, ProductsListViewModel>();

            // ProductsSearch
            this.container.RegisterType<IProductsSearchView, ProductsSearchView>();
            this.container.RegisterType<IProductsSearchViewModel, ProductsSearchViewModel>();

            // ProductItem
            this.container.RegisterType<IProductItemView, ProductItemView>();
            this.container.RegisterType<IProductItemViewModel, ProductItemViewModel>();

            // ProductDetail
            this.container.RegisterType<IProductDetailView, ProductDetailView>();
            this.container.RegisterType<IProductDetailViewModel, ProductDetailViewModel>();

            // ProductDetail PopupMode
            this.container.RegisterType<IProductDetailView, ProductDetailView>("PopupProductDetailView");
            this.container.RegisterType<IProductDetailViewModel, ProductDetailViewModel>("PopupProductDetailViewModel",
                new InjectionConstructor(
                    new ResolvedParameter<IProductDetailView>("PopupProductDetailView"),
                    typeof(Services.IProductionService),
                    typeof(IEventAggregator),
                    typeof(IUnityContainer))
                    );

            // PhotoGallery
            this.container.RegisterType<IProductPhotoGalleryView, ProductPhotoGalleryView>();
            this.container.RegisterType<IProductPhotoGalleryViewModel, ProductPhotoGalleryViewModel>();

            // ProductsNavigation
            this.container.RegisterType<IProductsNavigationView, ProductsNavigationView>();
            this.container.RegisterType<IProductsNavigationViewModel, ProductsNavigationViewModel>();
        }

        /// <summary>
        /// Registers regions
        /// </summary>
        void RegisterRegions()
        {
            // Regions
            this.regionManager.RegisterViewWithRegion(RegionNames.ProductsRegion, () => this.container.Resolve<IProductsListViewModel>().View);

            this.regionManager.RegisterViewWithRegion(RegionNames.ProductsSearchRegion, () => this.container.Resolve<IProductsSearchViewModel>().View);

            this.regionManager.RegisterViewWithRegion(RegionNames.ProductDetailRegion, () => this.container.Resolve<IProductDetailViewModel>().View);

            this.regionManager.RegisterViewWithRegion(RegionNames.ProductsNavigationRegion, () => this.container.Resolve<IProductsNavigationViewModel>().View);
        }

        #endregion
    }
}
