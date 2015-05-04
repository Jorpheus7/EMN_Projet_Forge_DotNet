// -----------------------------------------------------------------------
// <copyright file="ProductionService.cs" company="So@t">
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
using System.Collections.ObjectModel;
using Microsoft.Practices.Composite.Events;
using Soat.HappyNet.Silverlight.Modules.Production.ProductionServiceReference;
using Soat.HappyNet.Silverlight.Library;
using System.Threading;
using System.Collections.Generic;
using Soat.HappyNet.Server.DataContract;
using Microsoft.Practices.Unity;
using Soat.HappyNet.Silverlight.Infrastructure.Helpers;
using Soat.HappyNet.Silverlight.Infrastructure.Models;

namespace Soat.HappyNet.Silverlight.Modules.Production.Services
{
    /// <summary>
    /// Class defining the service of production
    /// </summary>
    public class ProductionService : IProductionService
    {
        #region Const

        /// <summary>
        /// Configuration name for the client proxy (cf. ServiceReferences.ClientConfig in the Shell Project)
        /// </summary>
        const string ServiceConfigurationName = "ProductionServiceConfiguration";

        #endregion
        
        #region Private members

        /// <summary>
        /// Web services proxy
        /// </summary>
        private readonly ProductionServiceClient client;

        /// <summary>
        /// Unity container
        /// </summary>
        private readonly IUnityContainer container;

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public ProductionService(IUnityContainer container)
        {
            this.container = container;

            this.client = new ProductionServiceClient(ProductionService.ServiceConfigurationName);

            client.GetProductsCompleted += new EventHandler<GetProductsCompletedEventArgs>(OnGetProductsCompleted);
            client.GetProductsByCategoryCompleted += new EventHandler<GetProductsByCategoryCompletedEventArgs>(OnGetProductsByCategoryCompleted);
            client.GetProductPrimaryPhotoCompleted += new EventHandler<GetProductPrimaryPhotoCompletedEventArgs>(OnGetProductPhotoCompleted);
            client.GetProductPhotosCompleted += new EventHandler<GetProductPhotosCompletedEventArgs>(OnGetProductPhotosCompleted);
            client.GetProductModelPhotoCompleted += new EventHandler<GetProductModelPhotoCompletedEventArgs>(OnGetProductModelPhotoCompleted);
            client.GetProductCategoriesCompleted += new EventHandler<GetProductCategoriesCompletedEventArgs>(OnGetProductCategoriesCompleted);
            client.GetProductModelDetailsCompleted += new EventHandler<GetProductModelDetailsCompletedEventArgs>(OnGetProductModelDetailsCompleted);
        }

        #region GetProducts

        /// <summary>
        /// Retrieves a list of products to be sold
        /// </summary>
        /// <param name="GetProductsCompleted">Callback method</param>
        public void BeginGetProducts(ServiceCompleted<ObservableCollection<ProductModel>> GetProductsCompleted)
        {
            // Gets current language
            var lang = container.Resolve<Language>();
            
            client.GetProductsAsync(lang.CultureID, lang.CountryCode,
                new ServiceArgs<ObservableCollection<ProductModel>>(GetProductsCompleted));
        }

        void OnGetProductsCompleted(object sender, GetProductsCompletedEventArgs e)
        {
            ObservableCollection<ProductModel> result = new ObservableCollection<ProductModel>();
            if (e.Error == null)
            {
                result = e.Result;
            }

            var arg = e.UserState as ServiceArgs<ObservableCollection<ProductModel>>;
            arg.Complete(result, e);
        }

        #endregion

        #region GetProductsByCategory

        /// <summary>
        /// Retrieves a list of products to be sold with the current language and from the given subcategories
        /// </summary>
        /// <param name="subCategoryIDs">Subcategories filtering the products list</param>
        /// <param name="GetProductsByCategoryCompleted">Callback method</param>
        public void BeginGetProductsByCategory(ObservableCollection<int> subCategoryIDs, ServiceCompleted<ObservableCollection<ProductModel>> GetProductsByCategoryCompleted)
        {
            BeginGetProductsByCategory(subCategoryIDs, GetProductsByCategoryCompleted, null);
        }

        /// <summary>
        /// Retrieves a list of products to be sold with the current language and from the given subcategories
        /// </summary>
        /// <param name="subCategoryIDs">Subcategories filtering the products list</param>
        /// <param name="GetProductsByCategoryCompleted">Callback method</param>
        /// <param name="userState">Object passed in when the method is called and retrieved in the callback</param>
        public void BeginGetProductsByCategory(ObservableCollection<int> subCategoryIDs, ServiceCompleted<ObservableCollection<ProductModel>> GetProductsByCategoryCompleted, object userState)
        {
            // Gets current language
            var lang = container.Resolve<Language>();

            client.GetProductsByCategoryAsync(subCategoryIDs, lang.CultureID, lang.CountryCode,
                new ServiceArgs<ObservableCollection<ProductModel>>(GetProductsByCategoryCompleted, userState));
        }

        void OnGetProductsByCategoryCompleted(object sender, GetProductsByCategoryCompletedEventArgs e)
        {
            ObservableCollection<ProductModel> result = new ObservableCollection<ProductModel>();
            if (e.Error == null)
            {
                result = e.Result;
            }

            var arg = e.UserState as ServiceArgs<ObservableCollection<ProductModel>>;
            arg.Complete(result, e);
        }

        #endregion

        #region GetProductPhoto

        /// <summary>
        /// Retrieves the photo of a product
        /// </summary>
        /// <param name="productID">Product ID</param>
        /// <param name="GetProductPhotoCompleted">Callback method</param>
        public void BeginGetProductPhoto(int productID, ServiceCompleted<ProductPhoto> GetProductPhotoCompleted)
        {
            client.GetProductPrimaryPhotoAsync(productID, 
                new ServiceArgs<ProductPhoto>(GetProductPhotoCompleted));
        }

        void OnGetProductPhotoCompleted(object sender, GetProductPrimaryPhotoCompletedEventArgs e)
        {
            ProductPhoto result = null;
            if (e.Error == null)
                result = e.Result;

            var arg = e.UserState as ServiceArgs<ProductPhoto>;
            arg.Complete(result, e);
        }

        #endregion

        #region GetProductModelPhoto

        /// <summary>
        /// Retrieves the main photo of a product model
        /// </summary>
        /// <param name="productModelID">Product model ID</param>
        /// <param name="GetProductModelPhotoCompleted">Callback method</param>
        public void BeginGetProductModelPhoto(int productModelID, ServiceCompleted<ProductPhoto> GetProductModelPhotoCompleted)
        {
            client.GetProductModelPhotoAsync(productModelID,
                new ServiceArgs<ProductPhoto>(GetProductModelPhotoCompleted));
        }

        void OnGetProductModelPhotoCompleted(object sender, GetProductModelPhotoCompletedEventArgs e)
        {
            ProductPhoto result = null;
            if (e.Error == null)
                result = e.Result;

            var arg = e.UserState as ServiceArgs<ProductPhoto>;
            arg.Complete(result, e);
        }

        #endregion

        #region GetProductCategories

        /// <summary>
        /// Retrieves the list of product categories
        /// </summary>
        /// <param name="GetProductCategoriesCompleted">Callback method</param>
        public void BeginGetProductCategories(ServiceCompleted<ObservableCollection<ProductCategory>> GetProductCategoriesCompleted)
        {
            client.GetProductCategoriesAsync(
                new ServiceArgs<ObservableCollection<ProductCategory>>(GetProductCategoriesCompleted));
        }

        void OnGetProductCategoriesCompleted(object sender, GetProductCategoriesCompletedEventArgs e)
        {
            ObservableCollection<ProductCategory> result = new ObservableCollection<ProductCategory>();
            if (e.Error == null)
            {
                result = e.Result;
            }

            var arg = e.UserState as ServiceArgs<ObservableCollection<ProductCategory>>;
            arg.Complete(result, e);
        }

        #endregion

        #region GetProductModelDetail

        /// <summary>
        /// Retrieves the detail of a product model with associated products and description
        /// </summary>
        /// <param name="productModelID">Product model ID</param>
        /// <param name="GetProductModelDetailsCompleted">Callback method</param>
        public void BeginGetProductModelDetails(int productModelID, ServiceCompleted<ProductModel> GetProductModelDetailsCompleted)
        {
            // Gets current language
            var lang = container.Resolve<Language>();

            client.GetProductModelDetailsAsync(productModelID, lang.CultureID, lang.CountryCode,
                new ServiceArgs<ProductModel>(GetProductModelDetailsCompleted));
        }

        void OnGetProductModelDetailsCompleted(object sender, GetProductModelDetailsCompletedEventArgs e)
        {
            var arg = e.UserState as ServiceArgs<ProductModel>;
            arg.Complete(e.Error == null ? e.Result : null, e);
        }   

        #endregion

        #region GetProductPhotos

        /// <summary>
        /// Retrieves the list of photos for a product
        /// </summary>
        /// <param name="productID">Product ID</param>
        /// <param name="GetProductPhotoCompleted">Callback method</param>
        public void BeginGetProductPhotos(int productID, ServiceCompleted<ObservableCollection<ProductPhoto>> GetProductPhotosCompleted) 
        {
            BeginGetProductPhotos(productID, GetProductPhotosCompleted, null);
        }

        /// <summary>
        /// Retrieves the list of photos for a product
        /// </summary>
        /// <param name="productID">Product ID</param>
        /// <param name="GetProductPhotoCompleted">Callback method</param>
        /// <param name="userState">Object passed in when the method is called and retrieved in the callback</param>
        public void BeginGetProductPhotos(int productID, ServiceCompleted<ObservableCollection<ProductPhoto>> GetProductPhotosCompleted, object userState)
        {
            client.GetProductPhotosAsync(productID,
                new ServiceArgs<ObservableCollection<ProductPhoto>>(GetProductPhotosCompleted, userState));
        }

        void OnGetProductPhotosCompleted(object sender, GetProductPhotosCompletedEventArgs e)
        {
            ObservableCollection<ProductPhoto> result = null;
            if (e.Error == null)
                result = e.Result;

            var arg = e.UserState as ServiceArgs<ObservableCollection<ProductPhoto>>;
            arg.Complete(result, e);
        }

        #endregion
    }
}
