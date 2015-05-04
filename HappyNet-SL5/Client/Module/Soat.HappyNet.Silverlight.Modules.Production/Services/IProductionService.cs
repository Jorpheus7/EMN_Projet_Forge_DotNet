// -----------------------------------------------------------------------
// <copyright file="IProductionService.cs" company="So@t">
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
using Soat.HappyNet.Silverlight.Library;
using Soat.HappyNet.Silverlight.Modules.Production.ProductionServiceReference;

namespace Soat.HappyNet.Silverlight.Modules.Production.Services
{
    /// <summary>
    /// Interface defining the service of production
    /// </summary>
    public interface IProductionService
    {
        /// <summary>
        /// Retrieves a list of products to be sold
        /// </summary>
        /// <param name="GetProductsCompleted">Callback method</param>
        void BeginGetProducts(ServiceCompleted<ObservableCollection<ProductModel>> GetProductsCompleted);

        /// <summary>
        /// Retrieves a list of products to be sold with the current language and from the given subcategories
        /// </summary>
        /// <param name="subCategoryIDs">Subcategories filtering the products list</param>
        /// <param name="GetProductsByCategoryCompleted">Callback method</param>
        void BeginGetProductsByCategory(ObservableCollection<int> subCategoryIDs, ServiceCompleted<ObservableCollection<ProductModel>> GetProductsByCategoryCompleted);

        /// <summary>
        /// Retrieves a list of products to be sold with the current language and from the given subcategories
        /// </summary>
        /// <param name="subCategoryIDs">Subcategories filtering the products list</param>
        /// <param name="GetProductsByCategoryCompleted">Callback method</param>
        /// <param name="userState">Object passed in when the method is called and retrieved in the callback</param>
        void BeginGetProductsByCategory(ObservableCollection<int> subCategoryIDs, ServiceCompleted<ObservableCollection<ProductModel>> GetProductsByCategoryCompleted, object userState);

        /// <summary>
        /// Retrieves the photo of a product
        /// </summary>
        /// <param name="productID">Product ID</param>
        /// <param name="GetProductPhotoCompleted">Callback method</param>
        void BeginGetProductPhoto(int productID, ServiceCompleted<ProductPhoto> GetProductPhotoCompleted);

        /// <summary>
        /// Retrieves the list of photos for a product
        /// </summary>
        /// <param name="productID">Product ID</param>
        /// <param name="GetProductPhotoCompleted">Callback method</param>
        void BeginGetProductPhotos(int productID, ServiceCompleted<ObservableCollection<ProductPhoto>> GetProductPhotosCompleted);

        /// <summary>
        /// Retrieves the list of photos for a product
        /// </summary>
        /// <param name="productID">Product ID</param>
        /// <param name="GetProductPhotoCompleted">Callback method</param>
        /// <param name="userState">Object passed in when the method is called and retrieved in the callback</param>
        void BeginGetProductPhotos(int productID, ServiceCompleted<ObservableCollection<ProductPhoto>> GetProductPhotosCompleted, object userState);

        /// <summary>
        /// Retrieves the main photo of a product model
        /// </summary>
        /// <param name="productModelID">Product model ID</param>
        /// <param name="GetProductModelPhotoCompleted">Callback method</param>
        void BeginGetProductModelPhoto(int productModelID, ServiceCompleted<ProductPhoto> GetProductModelPhotoCompleted);

        /// <summary>
        /// Retrieves the list of product categories
        /// </summary>
        /// <param name="GetProductCategoriesCompleted">Callback method</param>
        void BeginGetProductCategories(ServiceCompleted<ObservableCollection<ProductCategory>> GetProductCategoriesCompleted);

        /// <summary>
        /// Retrieves the detail of a product model with associated products and description
        /// </summary>
        /// <param name="productModelID">Product model ID</param>
        /// <param name="GetProductModelDetailsCompleted">Callback method</param>
        void BeginGetProductModelDetails(int productModelID, ServiceCompleted<ProductModel> GetProductModelDetailsCompleted);
    }
}
