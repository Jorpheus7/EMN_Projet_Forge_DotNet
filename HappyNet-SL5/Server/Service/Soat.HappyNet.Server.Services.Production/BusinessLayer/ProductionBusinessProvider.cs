// -----------------------------------------------------------------------
// <copyright file="ProductionBusinessProvider.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soat.HappyNet.Server.Services.Production.DataAccessLayer;
using Soat.HappyNet.Server.Entities;
using System.Drawing;
using System.IO;
using Soat.HappyNet.Server.Common;

namespace Soat.HappyNet.Server.Services.Production.BusinessLayer
{
    /// <summary>
    /// Class defining the business layer for production
    /// </summary>
    public class ProductionBusinessProvider : IProductionBusinessProvider
    {
        #region Membres
        
        /// <summary>
        /// Data Provider for production
        /// </summary>
        public IProductionDataProvider ProductionDataProvider { get; private set; }

        #endregion

        #region Constructeur

        /// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="personDataProvider">Data provider for production</param>
        public ProductionBusinessProvider(IProductionDataProvider productionDataProvider)
		{
            this.ProductionDataProvider = productionDataProvider;
		}

        #endregion

        /// <summary>
        /// Gets the list of products to be sold
        /// </summary>
        /// <param name="lang">ISO code for language to be used</param>
        /// <param name="countryCode">Country code used for currency</param>
        /// <returns>Products list</returns>
        public IEnumerable<ProductModel> GetProducts(string lang, string countryCode)
        {
            return this.ProductionDataProvider.GetProducts(lang, countryCode);
        }

        /// <summary>
        /// Gets a list of product matching a list of subcategories
        /// </summary>
        /// <param name="lang">ISO code for language to be used</param>
        /// <param name="countryCode">Country code used for currency</param>
        /// <param name="subCategoryIDs">Subcategories IDs to filter from</param>
        /// <returns>Products list</returns>
        public IEnumerable<ProductModel> GetProductsByCategory(IEnumerable<int> subCategoryIDs, string lang, string countryCode)
        {
            return this.ProductionDataProvider.GetProductsByCategory(subCategoryIDs, lang, countryCode);
        }

        /// <summary>
        /// Gets the main photo of a product
        /// </summary>
        /// <param name="productID">Product ID</param>
        /// <returns>Product photo</returns>
        public ProductPhoto GetProductPrimaryPhoto(int productID)
        {
            return this.ProductionDataProvider.GetProductPrimaryPhoto(productID);
        }

        /// <summary>
        /// Gets all the photos of a product
        /// </summary>
        /// <param name="productID">Product ID</param>
        /// <returns>Product photos</returns>
        public IEnumerable<ProductPhoto> GetProductPhotos(int productID)
        {
            return this.ProductionDataProvider.GetProductPhotos(productID);
        }

        /// <summary>
        /// Gets the main photo of a product model
        /// </summary>
        /// <param name="productModelID">Product model ID</param>
        /// <returns>Product photo</returns>
        public ProductPhoto GetProductModelPhoto(int productModelID)
        {
            return this.ProductionDataProvider.GetProductModelPhoto(productModelID);
        }

        /// <summary>
        /// Gets a list of product categories and subcategories
        /// </summary>
        /// <returns>Categories list</returns>
        public IEnumerable<ProductCategory> GetProductCategories()
        {
            return this.ProductionDataProvider.GetProductCategories();
        }

        /// <summary>
        /// Gets the detail of a product model with products and description
        /// </summary>
        /// <param name="productModelID">Product model ID</param>
        /// <param name="lang">Language for the description</param>
        /// <param name="countryCode">Country code for currency</param>
        /// <returns>Product model</returns>
        public ProductModel GetProductModelDetails(int productModelID, string lang, string countryCode)
        {
            return this.ProductionDataProvider.GetProductModelDetails(productModelID, lang, countryCode);
        }
    }
}
