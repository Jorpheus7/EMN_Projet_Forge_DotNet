// -----------------------------------------------------------------------
// <copyright file="ProductsNavigationViewModel.cs" company="So@t">
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
using System.Collections.ObjectModel;
using Soat.HappyNet.Silverlight.Modules.Production.ProductionServiceReference;
using Soat.HappyNet.Silverlight.Library.Extensions;
using Soat.HappyNet.Silverlight.Infrastructure.Events;

using IProductionService = Soat.HappyNet.Silverlight.Modules.Production.Services.IProductionService;

namespace Soat.HappyNet.Silverlight.Modules.Production.Views
{
    /// <summary>
    /// Class defining the ViewModel for the ProductsNavigation View
    /// </summary>
    public class ProductsNavigationViewModel : ViewModel<IProductsNavigationView>, IProductsNavigationViewModel
    {
        #region Properties

        private bool isLoading;
        /// <summary>
        /// Loading indicator
        /// </summary>
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                if (isLoading != value)
                {
                    isLoading = value;
                    Notify(() => this.IsLoading);
                }
            }
        }

        private ProductCategory currentCategory;
        /// <summary>
        /// Current category
        /// </summary>
        public ProductCategory CurrentCategory
        {
            get { return currentCategory; }
            set
            {
                if (currentCategory != value)
                {
                    currentCategory = value;
                    Notify(() => this.CurrentCategory);
                }
            }
        }

        private ProductSubcategory currentSubCategory;
        /// <summary>
        /// Current sub-category
        /// </summary>
        public ProductSubcategory CurrentSubCategory
        {
            get { return currentSubCategory; }
            set
            {
                if (currentSubCategory != value)
                {
                    currentSubCategory = value;
                    Notify(() => this.CurrentSubCategory);
                }
            }
        }

        private bool isError = false;
        /// <summary>
        /// True if loading failed ... :(
        /// </summary>
        public bool IsError
        {
            get { return isError; }
            set
            {
                if (isError != value)
                {
                    isError = value;
                    Notify(() => this.IsError);
                }
            }
        }

        /// <summary>
        /// Categories
        /// </summary>
        public ObservableCollection<ProductCategory> Categories { get; private set; }

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
        /// Production service
        /// </summary>
        private readonly IProductionService productionService;

        #endregion

        #region Commands

        /// <summary>
        /// Command for category or sub-category selection
        /// </summary>
        public ICommand CategorySelectionCommand { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="view">View attached to this ViewModel</param>
        /// <param name="eventAggregator">Event aggregator</param>
        /// <param name="container">Unity container</param>
        /// <param name="productionService">Production service</param>
        public ProductsNavigationViewModel(IProductsNavigationView view,
            IProductionService productionService,
            IEventAggregator eventAggregator,
            IUnityContainer container)
        {
            InitializeCommands();

            this.productionService = productionService;
            this.container = container;
            this.eventAggregator = eventAggregator;

            Categories = new ObservableCollection<ProductCategory>();

            this.View = view;
            this.View.Model = this;

            InitData();
        }

        #endregion

        #region Commands

        /// <summary>
        /// Initializes commands
        /// </summary>
        void InitializeCommands()
        {
            CategorySelectionCommand = new DelegateCommand<object>(SelectCategory);
        }

        /// <summary>
        /// Selects a category or sub-category
        /// </summary>
        /// <param name="category"></param>
        void SelectCategory(object category)
        {
            if (category is ProductCategory)
            {
                var productCategory = category as ProductCategory;
                if (productCategory.ProductCategoryID > 0) // Category selected
                    eventAggregator.Publish(new CategorySelectedEvent(productCategory));
                else // Category "all products" selected
                    eventAggregator.Publish(new CategorySelectedEvent());
            }
            else if (category is ProductSubcategory)
            {
                eventAggregator.Publish(new CategorySelectedEvent((ProductSubcategory)category));
            }
        }

        #endregion

        /// <summary>
        /// Initializes and loads data
        /// </summary>
        void InitData()
        {
            IsError = false;
            IsLoading = true;
            productionService.BeginGetProductCategories(OnGetProductCategoriesCompleted);
        }

        void OnGetProductCategoriesCompleted(ServiceArgs<ObservableCollection<ProductCategory>> args)
        {
            Categories.Clear();

            if (!args.IsResultNullOrError)
            {
                Categories.Add(new ProductCategory
                {
                    Name = "All products"
                });

                foreach (var category in args.Result)
                {
                    Categories.Add(category);
                }
            }
            else
            {
                IsError = true;
            }

            IsLoading = false;
        }
    }
}
