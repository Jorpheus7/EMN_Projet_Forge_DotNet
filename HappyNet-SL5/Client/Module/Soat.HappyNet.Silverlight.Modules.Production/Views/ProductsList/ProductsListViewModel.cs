// -----------------------------------------------------------------------
// <copyright file="ProductsListViewModel.cs" company="So@t">
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
using Soat.HappyNet.Silverlight.Modules.Production.Services;
using System.Collections.ObjectModel;
using Soat.HappyNet.Silverlight.Library.Extensions;
using Soat.HappyNet.Silverlight.Infrastructure.Events;
using Soat.HappyNet.Silverlight.Modules.Production.ProductionServiceReference;
using Soat.HappyNet.Silverlight.Infrastructure.Localization;

using IProductionService = Soat.HappyNet.Silverlight.Modules.Production.Services.IProductionService;

namespace Soat.HappyNet.Silverlight.Modules.Production.Views
{
    /// <summary>
    /// Class defining the ViewModel for the ProductsList View
    /// </summary>
    public class ProductsListViewModel : ViewModel<IProductsListView>, IProductsListViewModel
    {
        #region Properties

        private bool isBusy = false;
        /// <summary>
        /// Busy indicator
        /// </summary>
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;
                    Notify(() => this.IsBusy);
                }
            }
        }

        private string categoryName;
        /// <summary>
        /// Category name
        /// </summary>
        public string CategoryName
        {
            get { return categoryName; }
            set
            {
                if (categoryName != value)
                {
                    categoryName = value;
                    Notify(() => this.CategoryName);
                }
            }
        }

        /// <summary>
        /// List of products
        /// </summary>
        ObservableCollection<IProductItemViewModel> Products { get; set; }

        /// <summary>
        /// Products filtered and displayed
        /// </summary>
        public ObservableCollection<IProductItemViewModel> FilteredProducts { get; private set; }

        #endregion

        #region Private members

        /// <summary>
        /// Event aggregator
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        /// <summary>
        /// Unity container
        /// </summary>
        private readonly IUnityContainer container;

        /// <summary>
        /// Web service of production
        /// </summary>
        private readonly IProductionService productionService;

        /// <summary>
        /// Search string
        /// </summary>
        private string searchString = string.Empty;

        /// <summary>
        /// Selected category
        /// </summary>
        private ProductCategory selectedCategory = null;

        /// <summary>
        /// Selected sub category
        /// </summary>
        private ProductSubcategory selectedSubCategory = null;

        /// <summary>
        /// Id for the last call to the method GetProductsByCategory
        /// </summary>
        private Guid lastGetProductsByCategory;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="view">View attached to this ViewModel</param>
        /// <param name="eventAggregator">Event aggregator</param>
        /// <param name="container">Unity container</param>
        /// <param name="productionService">Production service</param>
        public ProductsListViewModel(IProductsListView view,
            IProductionService productionService,
            IEventAggregator eventAggregator,
            IUnityContainer container)
        {
            this.productionService = productionService;
            this.container = container;
            this.eventAggregator = eventAggregator;

            Products = new ObservableCollection<IProductItemViewModel>();
            FilteredProducts = new ObservableCollection<IProductItemViewModel>();

            this.View = view;
            this.View.Model = this;

            eventAggregator.Subscribe<LocalizationChangedEvent>(OnLocalizationChanged);
            eventAggregator.Subscribe<SearchProductsEvent>(OnSearchProduct);
            eventAggregator.Subscribe<CategorySelectedEvent>(OnCategorySelected);
        }

        #endregion

        #region Data Methods

        /// <summary>
        /// Loads the products list
        /// </summary>
        void LoadProductsList()
        {
            // Clean up
            Products.Clear();
            FilteredProducts.Clear();

            // Gets the ids of subcategories corresponding to the current selection
            ObservableCollection<int> subCategories = null;
            if (selectedCategory != null)
            {
                subCategories = selectedCategory.ProductSubcategory.Select(ps => ps.ProductSubcategoryID).ToObservableCollection();
            }
            else if (selectedSubCategory != null)
            {
                subCategories = new ObservableCollection<int> { selectedSubCategory.ProductSubcategoryID };
            }

            // Notify that we are loading data
            IsBusy = true;
            // Generates a new unique id for the web service call
            lastGetProductsByCategory = Guid.NewGuid();
            // Calls the web service
            productionService.BeginGetProductsByCategory(subCategories, OnGetProductsByCategoryCompleted, lastGetProductsByCategory);
        }

        void OnGetProductsByCategoryCompleted(ServiceArgs<ObservableCollection<ProductModel>> args)
        {
            // Clean up
            Products.Clear();
            FilteredProducts.Clear();

            // Checks if it's the last call passed to the web service
            if ((Guid)args.UserState != lastGetProductsByCategory)
                return;

            if (!args.IsResultNullOrError)
            {
                foreach (var product in args.Result)
                {
                    var productViewModel = container.Resolve<IProductItemViewModel>();
                    productViewModel.SetProduct(product);
                    Products.Add(productViewModel);
                }
            }
            else if (args.Error != null)
            {
                // Error notification
                eventAggregator.Publish(new NotificationEvent(NotificationType.Error, Messages.ERR_CouldNotRetrieveData));
            }

            Filter();
            // Notify we have finished loading data
            IsBusy = false;
        }

        /// <summary>
        /// Filter the current product list based on the search string
        /// </summary>
        void Filter()
        {
            FilteredProducts.Clear();

            IEnumerable<IProductItemViewModel> filteredList;

            if (!string.IsNullOrEmpty(searchString))
            {
                filteredList = Products
                    .Where(p => (p.ProductModel.Name != null && p.ProductModel.Name.ToUpper().Contains(searchString))
                        || (p.ProductModel.ProductDescription != null && p.ProductModel.ProductDescription.Description != null && p.ProductModel.ProductDescription.Description.ToUpper().Contains(searchString)));
            }
            else
            {
                filteredList = Products;
            }

            foreach (var productViewModel in filteredList)
            {
                (productViewModel.View as DependencyObject).RemoveFromParentPanel();
                productViewModel.LoadPhoto();
                FilteredProducts.Add(productViewModel);
            }
        }

        #endregion

        #region EventAggregator

        /// <summary>
        /// Handles when a category is selected
        /// </summary>
        /// <param name="args"></param>
        public void OnCategorySelected(CategorySelectedEvent args)
        {
            this.selectedCategory = args.SelectedCategory;
            this.selectedSubCategory = args.SelectedSubCategory;

            if (selectedCategory != null)
                CategoryName = selectedCategory.Name;
            else if (selectedSubCategory != null)
                CategoryName = selectedSubCategory.Name;
            else
                CategoryName = null;

            LoadProductsList();
        }

        /// <summary>
        /// Handles when we want to search for a product
        /// </summary>
        /// <param name="args"></param>
        public void OnSearchProduct(SearchProductsEvent args)
        {
            this.searchString = string.Empty;
            if (!string.IsNullOrEmpty(args.SearchString))
            {
                this.searchString = args.SearchString.Trim().ToUpper();
            }

            // If no product has been loaded, we just load them all
            if (Products.Count == 0)
                LoadProductsList();
            // Otherwise, we filter on the current list
            else
                Filter();
        }

        #endregion

        #region Localization

        /// <summary>
        /// Handles when localization changes
        /// </summary>
        /// <param name="args"></param>
        public void OnLocalizationChanged(LocalizationChangedEvent args)
        {
            // If nothing has been loaded, no use to retrieve the product list
            if (Products.Count > 0) 
            {
                LoadProductsList();
            }
        }

        #endregion

        #region ICleanable

        /// <summary>
        /// Cleans
        /// </summary>
        public override void Clean()
        {
            base.Clean();

            foreach (var pVM in FilteredProducts)
            {
                if (pVM is IDisposable)
                    ((IDisposable)pVM).Dispose();
            }
            FilteredProducts.Clear();
            Products.Clear();
        }

        #endregion
    }
}
