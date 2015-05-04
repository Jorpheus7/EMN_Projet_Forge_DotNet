// -----------------------------------------------------------------------
// <copyright file="ProductionService.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soat.HappyNet.Server.Entities;
using Soat.HappyNet.Server.Services.Production.DataAccessLayer;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Soat.HappyNet.Server.Services.Production.BusinessLayer;
using System.Configuration;
using System.ServiceModel.Activation;
using System.Web;

namespace Soat.HappyNet.Server.Services.Production
{
    /// <summary>
    /// Class defining the service for production
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ProductionService : IProductionService
    {
        #region Const

        /// <summary>
        /// Section name for unity configuration
        /// </summary>
        const string UNITY_SECTION_NAME = "unity";

        /// <summary>
        /// Container name for Unity configuration
        /// </summary>
        const string UNITY_CONTAINER_NAME = "productionServiceContainer";

        #endregion

        #region Container Property

        private IUnityContainer container;
        /// <summary>
        /// Unity container
        /// </summary>
        public IUnityContainer Container
        {
            get
            {
                if (this.container == null)
                {
                    this.container = new UnityContainer();
                }
                return this.container;
            }
        }

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public ProductionService()
        {
            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection(ProductionService.UNITY_SECTION_NAME);
            section.Configure(this.Container, ProductionService.UNITY_CONTAINER_NAME);
        }

        /// <summary>
        /// Gets the list of products to be sold
        /// </summary>
        /// <param name="lang">ISO code for language to be used</param>
        /// <param name="countryCode">Country code used for currency</param>
        /// <returns>Products list</returns>
        public IEnumerable<ProductModel> GetProducts(string lang, string countryCode)
        {
            IProductionBusinessProvider businessProvider = this.Container.Resolve<IProductionBusinessProvider>();
            return businessProvider.GetProducts(lang, countryCode);
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
            IProductionBusinessProvider businessProvider = this.Container.Resolve<IProductionBusinessProvider>();
            return businessProvider.GetProductsByCategory(subCategoryIDs, lang, countryCode);
        }

        /// <summary>
        /// Gets the main photo of a product
        /// </summary>
        /// <param name="productID">Product ID</param>
        /// <returns>Product photo</returns>
        public ProductPhoto GetProductPrimaryPhoto(int productID)
        {
            IProductionBusinessProvider businessProvider = this.Container.Resolve<IProductionBusinessProvider>();
            return businessProvider.GetProductPrimaryPhoto(productID);
        }

        /// <summary>
        /// Gets all the photos of a product
        /// </summary>
        /// <param name="productID">Product ID</param>
        /// <returns>Product photos</returns>
        public IEnumerable<ProductPhoto> GetProductPhotos(int productID)
        {
            IProductionBusinessProvider businessProvider = this.Container.Resolve<IProductionBusinessProvider>();
            return businessProvider.GetProductPhotos(productID);
        }

        /// <summary>
        /// Gets the main photo of a product model
        /// </summary>
        /// <param name="productModelID">Product model ID</param>
        /// <returns>Product photo</returns>
        public ProductPhoto GetProductModelPhoto(int productModelID)
        {
            IProductionBusinessProvider businessProvider = this.Container.Resolve<IProductionBusinessProvider>();
            return businessProvider.GetProductModelPhoto(productModelID);
        }

        /// <summary>
        /// Gets a list of product categories and subcategories
        /// </summary>
        /// <returns>Categories list</returns>
        public IEnumerable<ProductCategory> GetProductCategories()
        {
            IProductionBusinessProvider businessProvider = this.Container.Resolve<IProductionBusinessProvider>();
            return businessProvider.GetProductCategories();
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
            IProductionBusinessProvider businessProvider = this.Container.Resolve<IProductionBusinessProvider>();

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
            }

            return businessProvider.GetProductModelDetails(productModelID, lang, countryCode);
        }
    }
}
