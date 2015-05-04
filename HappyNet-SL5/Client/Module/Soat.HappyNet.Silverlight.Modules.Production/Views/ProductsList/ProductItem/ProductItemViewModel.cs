// -----------------------------------------------------------------------
// <copyright file="ProductItemViewModel.cs" company="So@t">
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
using Soat.HappyNet.Silverlight.Modules.Production.ProductionServiceReference;
using Soat.HappyNet.Silverlight.Library;
using Soat.HappyNet.Silverlight.Library.Extensions;
using System.Windows.Media.Imaging;
using Soat.HappyNet.Silverlight.Library.Helpers;
using System.Windows.Threading;
using ImageTools.IO.Gif;
using System.IO;
using ImageTools.IO;
using ImageTools.IO.Png;

using IProductionService = Soat.HappyNet.Silverlight.Modules.Production.Services.IProductionService;
using Soat.HappyNet.Silverlight.Library.Commands;
using Soat.HappyNet.Silverlight.Infrastructure.Events;

namespace Soat.HappyNet.Silverlight.Modules.Production.Views
{
    /// <summary>
    /// Class defining the ViewModel for the ProductItem View
    /// </summary>
    public class ProductItemViewModel : ViewModel<IProductItemView>, IProductItemViewModel
    {
        #region Properties

        private ImageTools.Image thumbNailPhoto = null;
        /// <summary>
        /// Photo of the product
        /// </summary>
        public ImageTools.Image ThumbNailPhoto
        {
            get { return thumbNailPhoto; }
            set
            {
                if (thumbNailPhoto != value)
                {
                    thumbNailPhoto = value;
                    Notify(() => this.ThumbNailPhoto);
                }
            }
        }

        ProductModel productModel = new ProductModel();
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
                productModel = value;
                Notify(() => this.Name);
                Notify(() => this.Price);
            }
        }

        /// <summary>
        /// Name of the product
        /// </summary>
        public string Name
        {
            get { return productModel.Name; }
            set
            {
                if (productModel.Name != value)
                {
                    productModel.Name = value;
                    Notify(() => this.Name);
                }
            }
        }

        /// <summary>
        /// Price
        /// </summary>
        public string Price
        {
            get { return string.Format("{0:0.00} {1}", productModel.LowerPrice, productModel.Currency); }
        }

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

        #endregion

        #region Commands

        /// <summary>
        /// Command to select a product
        /// </summary>
        public ICommand SelectProductCommand { get; private set; }

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
        /// Web services for production
        /// </summary>
        private readonly IProductionService productionService;

        /// <summary>
        /// Status for the photo being loaded
        /// </summary>
        bool loadingPhoto = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="view">View attached to this ViewModel</param>
        /// <param name="eventAggregator">Event aggregator</param>
        /// <param name="container">Unity container</param>
        /// <param name="productionService">Production service</param>
        public ProductItemViewModel(IProductItemView view,
            IEventAggregator eventAggregator,
            IUnityContainer container,
            IProductionService productionService)
        {
            InitializeCommands();

            this.productionService = productionService;
            this.container = container;
            this.eventAggregator = eventAggregator;

            this.View = view;
            this.View.Model = this;
        }

        #endregion

        #region Commands
        
        /// <summary>
        /// Initializes commands
        /// </summary>
        void InitializeCommands()
        {
            SelectProductCommand = new DelegateCommand(SelectProduct);
        }

        /// <summary>
        /// Selects a product
        /// </summary>
        void SelectProduct()
        {
            if (ProductModel != null)
            {
                eventAggregator.Publish(new ProductSelectedEvent(ProductModel.ProductModelID));
            }
        }

        #endregion

        #region Product methods

        /// <summary>
        /// Initialize the current product
        /// </summary>
        /// <param name="productModel">Product model</param>
        public void SetProduct(ProductModel productModel)
        {
            this.ProductModel = productModel;
        }

        /// <summary>
        /// Loads the product photo
        /// </summary>
        public void LoadPhoto()
        {
            if (ThumbNailPhoto == null && !loadingPhoto)
            {
                loadingPhoto = true;
                IsLoading = true;
                productionService.BeginGetProductModelPhoto(productModel.ProductModelID, OnGetProductModelPhotoCompleted);
            }
        }

        void OnGetProductModelPhotoCompleted(ServiceArgs<ProductPhoto> args)
        {
            loadingPhoto = false;
            IsLoading = false;

            if (!args.IsResultNullOrError)
            {
                if (args.Result.LargePhoto != null)
                {
                    ImageTools.Image img = new ImageTools.Image();

                    // GIF conversion (not supported by SL3)
                    MemoryStream ms = null;
                    try
                    {
                        ms = new MemoryStream(args.Result.LargePhoto);
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

                    ThumbNailPhoto = img;
                    View.SetPhoto(ThumbNailPhoto);
                }
            }
        }

        #endregion

        #region ICleanable

        /// <summary>
        /// Clean method
        /// </summary>
        public override void Clean()
        {
            SelectProductCommand = null;
            ThumbNailPhoto = null;

            base.Clean();
        }

        #endregion
    }
}
