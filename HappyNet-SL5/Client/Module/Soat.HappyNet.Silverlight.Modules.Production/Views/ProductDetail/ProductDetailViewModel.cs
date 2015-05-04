// -----------------------------------------------------------------------
// <copyright file="ProductDetailViewModel.cs" company="So@t">
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
using Soat.HappyNet.Silverlight.Library.Extensions;
using Soat.HappyNet.Silverlight.Infrastructure.Events;
using Soat.HappyNet.Silverlight.Modules.Production.Services;
using Soat.HappyNet.Silverlight.Modules.Production.ProductionServiceReference;
using Soat.HappyNet.Silverlight.Infrastructure.Localization;
using System.IO;
using System.Collections.ObjectModel;
using Soat.HappyNet.Silverlight.Library.Commands;

using IProductionService = Soat.HappyNet.Silverlight.Modules.Production.Services.IProductionService;
using Soat.HappyNet.Silverlight.Infrastructure;
using Soat.HappyNet.Silverlight.Infrastructure.UriNames;

namespace Soat.HappyNet.Silverlight.Modules.Production.Views
{
    /// <summary>
    /// Cette classe représente le Vue Model (ViewModel) pour le ProductDetail
    /// </summary>
    public class ProductDetailViewModel : ViewModel<IProductDetailView>, IProductDetailViewModel
    {
        #region Constantes

        /// <summary>
        /// Name of the parameter used in the query string to retrieve a product id
        /// </summary>
        private const string PARAM_PRODUCTID = "productid";

        #endregion

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

        private ProductModel productModel = new ProductModel();
        /// <summary>
        /// Current product model
        /// </summary>
        public ProductModel ProductModel
        {
            get 
            {
                if (productModel == null)
                    productModel = new ProductModel();

                return productModel; 
            }
            set
            {
                if (productModel != value)
                {
                    productModel = value;
                    Notify(() => this.ProductModel);
                    Notify(() => this.Name);
                    Notify(() => this.Price);
                    Notify(() => this.Description);
                    Notify(() => this.IsMultiProducts);
                    Notify(() => this.Products);
                }
            }
        }

        /// <summary>
        /// Products list
        /// </summary>
        public ObservableCollection<Product> Products 
        {
            get { return ProductModel.Product; }
        }

        /// <summary>
        /// Name of the selected product
        /// </summary>
        public string Name
        {
            get 
            {
                if (CurrentProduct != null)
                    return CurrentProduct.Name;

                return ProductModel.Name; 
            }
        }

        /// <summary>
        /// Price
        /// </summary>
        public string Price
        {
            get 
            {
                if (CurrentProduct == null)
                {
                    return string.Format("{0:0.00} {1}", ProductModel.LowerPrice, ProductModel.Currency);
                }

                return string.Format("{0:0.00} {1}", currentProduct.Price, currentProduct.Currency);
            }
        }

        /// <summary>
        /// Description
        /// </summary>
        public string Description
        {
            get 
            {
                if (ProductModel.ProductDescription != null)
                    return ProductModel.ProductDescription.Description;

                return null; 
            }
            set
            {
                if (ProductModel.ProductDescription != null) 
                {
                    ProductModel.ProductDescription.Description = value;
                    Notify(() => this.Description);
                }
            }
        }

        /// <summary>
        /// True if there are several products attached to the product model, false otherwise
        /// </summary>
        public bool IsMultiProducts
        {
            get 
            { 
                if (Products != null)
                    return Products.Count > 1;

                return false;
            }
        }

        private bool isProductLoaded = false;
        /// <summary>
        /// Indicator to know if the product detail has been loaded
        /// </summary>
        public bool IsProductLoaded
        {
            get { return isProductLoaded; }
            set
            {
                if (isProductLoaded != value)
                {
                    isProductLoaded = value;
                    Notify(() => this.IsProductLoaded);
                }
            }
        }

        private Product currentProduct;
        /// <summary>
        /// Selected product
        /// </summary>
        public Product CurrentProduct
        {
            get { return currentProduct; }
            set
            {
                if (currentProduct != value)
                {
                    currentProduct = value;
                    Notify(() => this.CurrentProduct);
                    Notify(() => this.Price);
                    Notify(() => this.Name);

                    UpdateProduct();
                }
            }
        }

        /// <summary>
        /// Photos collection, each one being attached to a product
        /// </summary>
        Dictionary<int, ObservableCollection<ImageTools.Image>> PhotosLibrary = new Dictionary<int, ObservableCollection<ImageTools.Image>>();

        #endregion

        #region Commandes

        /// <summary>
        /// Command to add the selected product in the shopping cart
        /// </summary>
        public ICommand AddToCardCommand { get; private set; }

        /// <summary>
        /// Command to display a photo gallery
        /// </summary>
        public ICommand ShowPhotoGalleryCommand { get; private set; }

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
        /// Service for production
        /// </summary>
        private readonly IProductionService productionService;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="view">View attached to this ViewModel</param>
        /// <param name="eventAggregator">Event aggregator</param>
        public ProductDetailViewModel(IProductDetailView view,
            IProductionService productionService,
            IEventAggregator eventAggregator,
            IUnityContainer container)
        {
            this.InitializeCommands();

            this.container = container;
            this.productionService = productionService;
            this.eventAggregator = eventAggregator;

            this.View = view;
            this.View.Model = this;

            eventAggregator.Subscribe<ProductSelectedEvent>(OnProductSelected);
            eventAggregator.Subscribe<LocalizationChangedEvent>(OnLocalizationChanged);

            TryNavigation();
        }

        #endregion

        #region InitializeCommands

        /// <summary>
        /// Initializes the commands
        /// </summary>
        void InitializeCommands()
        {
            AddToCardCommand = new DelegateCommand<Product>(AddToCard);
            ShowPhotoGalleryCommand = new DelegateCommand(ShowPhotoGallery);
        }

        /// <summary>
        /// Initializes data
        /// </summary>
        void InitData()
        {
            this.ProductModel = null;
            this.CurrentProduct = null;
            this.View.SetPhoto(null);
            IsProductLoaded = false;
        }

        #endregion

        #region Détail produit

        /// <summary>
        /// Triggered when a product is selected
        /// </summary>
        /// <param name="args"></param>
        public void OnProductSelected(ProductSelectedEvent args)
        {
            if (args.ProductModelID > 0)
            {
                Globals.ChangeFragmentWithoutNavigating(string.Format("{0}/{1}", UriNames.ShoppingUri, args.ProductModelID));

                LoadProductDetail(args.ProductModelID);
            }
        }

        /// <summary>
        /// Loads the product detail
        /// </summary>
        /// <param name="productModelID">Product model id</param>
        void LoadProductDetail(int productModelID)
        {
            InitData();

            if (productModelID > 0)
            {
                IsLoading = true;
                productionService.BeginGetProductModelDetails(productModelID, ProductModelDetailsCompleted);
            }
        }

        /// <summary>
        /// Callback when web service returns the detail of a product model
        /// </summary>
        /// <param name="args">Service argument</param>
        void ProductModelDetailsCompleted(ServiceArgs<ProductModel> args)
        {
            if (args.Error == null)
            {
                this.ProductModel = args.Result;
                if (ProductModel.Product != null && ProductModel.Product.Count > 0)
                {
                    CurrentProduct = ProductModel.Product[0];
                }

                this.View.SetPhoto(null);
                IsProductLoaded = true;
            }
            else
            {
                // Error notification
                eventAggregator.Publish(new NotificationEvent(NotificationType.Error, Messages.ERR_CouldNotRetrieveData));
            }

            IsLoading = false;
        }

        /// <summary>
        /// Updates the photos for the selected product
        /// </summary>
        void UpdateProduct()
        {
            if (CurrentProduct != null)
                LoadPhotos(CurrentProduct.ProductID);
        }

        #region Photos

        /// <summary>
        /// Loads all the photos for a given product
        /// </summary>
        /// <param name="productID">Product id</param>
        public void LoadPhotos(int productID)
        {
            this.View.SetPhoto(null);

            if (!PhotosLibrary.ContainsKey(productID))
            {
                // Loads photos
                IsLoading = true;
                productionService.BeginGetProductPhotos(productID, OnGetProductPhotosCompleted, productID);
            }
            else
            {
                // Displays already loaded photos
                DisplayCurrentProductPhoto(productID);
            }
        }

        /// <summary>
        /// Return from the web service retrieving photos
        /// </summary>
        /// <param name="args">Service argument</param>
        void OnGetProductPhotosCompleted(ServiceArgs<ObservableCollection<ProductPhoto>> args)
        {
            IsLoading = false;

            if (!args.IsResultNullOrError)
            {
                int productID = (int)args.UserState;

                foreach (var photo in args.Result)
                {
                    if (photo.LargePhoto != null)
                    {
                        ImageTools.Image img = new ImageTools.Image();

                        // GIF conversion (not supported by SL3)
                        MemoryStream ms = null;
                        try
                        {
                            ms = new MemoryStream(photo.LargePhoto);
                            img.IsLoadingSynchronously = true;
                            img.SetSource(ms);
                        }
                        catch
                        {
                            img = null;
                        }
                        finally
                        {
                            if (ms != null)
                            {
                                ms.Close();
                            }
                            ms = null;
                        }

                        // Adds to the photos collection linked to this product
                        if (!PhotosLibrary.ContainsKey(productID))
                        {
                            PhotosLibrary.Add(productID, new ObservableCollection<ImageTools.Image>());
                            
                        }
                        PhotosLibrary[productID].Add(img);
                    }
                }

                DisplayCurrentProductPhoto(productID);
            }
        }

        /// <summary>
        /// Displays the photo of the current product if already in cache
        /// </summary>
        /// <param name="productID"></param>
        void DisplayCurrentProductPhoto(int productID)
        {
            if (PhotosLibrary.ContainsKey(productID))
            {
                if (PhotosLibrary[productID].Count > 0)
                {
                    this.View.SetPhoto(PhotosLibrary[productID][0]);
                }
            }
            else
            {
                this.View.SetPhoto(null);
            }
        }

        #endregion

        #endregion

        #region Ajout panier

        /// <summary>
        /// Adds a product to the shopping cart
        /// </summary>
        /// <param name="product">Product to add</param>
        void AddToCard(Product product)
        {
            if (product != null)
            {
                eventAggregator.Publish(new AddToCartEvent
                {
                    ProductID = product.ProductID,
                    ProductName = product.Name,
                    Quantity = 1,
                    Price = product.Price,
                    Currency = product.Currency
                });
            }
        }

        #endregion

        #region Galerie Photos

        /// <summary>
        /// Displays the photos gallery of the current product
        /// </summary>
        void ShowPhotoGallery()
        {
            if (CurrentProduct != null)
            {
                if (PhotosLibrary.ContainsKey(CurrentProduct.ProductID))
                {
                    var galleryPopup = this.container.Resolve<IProductPhotoGalleryViewModel>();
                    foreach (var photo in PhotosLibrary[CurrentProduct.ProductID])
                    {
                        galleryPopup.Photos.Add(photo);
                    }
                    galleryPopup.Title = CurrentProduct.Name;
                    galleryPopup.Show();
                }
            }
        }

        #endregion

        #region Localization

        /// <summary>
        /// Handles the localization change
        /// </summary>
        /// <param name="args"></param>
        public void OnLocalizationChanged(LocalizationChangedEvent args)
        {
            if (this.ProductModel != null)
            {
                int productModelID = this.ProductModel.ProductModelID;
                LoadProductDetail(productModelID);
            }
        }

        #endregion

        #region Navigation

        /// <summary>
        /// Gets the last query string and if any product model id is found, loads its detail
        /// </summary>
        void TryNavigation()
        {
            // Checks if we're on the right page
            if (Globals.CurrentUri.OriginalString.StartsWith(UriNames.ShoppingUri + "/"))
            {
                // Looking for the product id param
                if (Globals.QueryString.ContainsKey(PARAM_PRODUCTID))
                {
                    string productid = Globals.QueryString[PARAM_PRODUCTID];
                    int productModelID = -1;
                    if (int.TryParse(productid, out productModelID))
                    {
                        // Found it ! Let's load the product detail ...
                        LoadProductDetail(productModelID);
                    }
                }
            }
        }

        #endregion

        #region ICleanable

        public override void Clean()
        {
            PhotosLibrary.Clear();

            base.Clean();
        }

        #endregion
    }
}
