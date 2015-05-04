// -----------------------------------------------------------------------
// <copyright file="IProductionService.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Soat.HappyNet.Server.Entities;
using System.ServiceModel;

namespace Soat.HappyNet.Server.Services.Production
{
    [ServiceContract]
    public interface IProductionService
    {
        /// <summary>
        /// Gets the list of products to be sold
        /// </summary>
        /// <param name="lang">ISO code for language to be used</param>
        /// <param name="countryCode">Country code used for currency</param>
        /// <returns>Products list</returns>
        [OperationContract]
        IEnumerable<ProductModel> GetProducts(string lang, string countryCode);

        /// <summary>
        /// Gets a list of product matching a list of subcategories
        /// </summary>
        /// <param name="lang">ISO code for language to be used</param>
        /// <param name="countryCode">Country code used for currency</param>
        /// <param name="subCategoryIDs">Subcategories IDs to filter from</param>
        /// <returns>Products list</returns>
        [OperationContract]
        IEnumerable<ProductModel> GetProductsByCategory(IEnumerable<int> subCategoryIDs, string lang, string countryCode);

        /// <summary>
        /// Gets the main photo of a product
        /// </summary>
        /// <param name="productID">Product ID</param>
        /// <returns>Product photo</returns>
        [OperationContract]
        ProductPhoto GetProductPrimaryPhoto(int productID);

        /// <summary>
        /// Gets all the photos of a product
        /// </summary>
        /// <param name="productID">Product ID</param>
        /// <returns>Product photos</returns>
        [OperationContract]
        IEnumerable<ProductPhoto> GetProductPhotos(int productID);

        /// <summary>
        /// Gets the main photo of a product model
        /// </summary>
        /// <param name="productModelID">Product model ID</param>
        /// <returns>Product photo</returns>
        [OperationContract]
        ProductPhoto GetProductModelPhoto(int productModelID);

        /// <summary>
        /// Gets a list of product categories and subcategories
        /// </summary>
        /// <returns>Categories list</returns>
        [OperationContract]
        IEnumerable<ProductCategory> GetProductCategories();

        /// <summary>
        /// Gets the detail of a product model with products and description
        /// </summary>
        /// <param name="productModelID">Product model ID</param>
        /// <param name="lang">Language for the description</param>
        /// <param name="countryCode">Country code for currency</param>
        /// <returns>Product model</returns>
        [OperationContract]
        ProductModel GetProductModelDetails(int productModelID, string lang, string countryCode);
    }
}
