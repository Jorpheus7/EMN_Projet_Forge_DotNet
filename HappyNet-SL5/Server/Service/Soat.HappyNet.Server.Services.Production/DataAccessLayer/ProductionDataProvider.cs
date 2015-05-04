// -----------------------------------------------------------------------
// <copyright file="ProductionDataProvider.cs" company="So@t">
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
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Soat.HappyNet.Server.Common.Extensions;

namespace Soat.HappyNet.Server.Services.Production.DataAccessLayer
{
    public class ProductionDataProvider : IProductionDataProvider
    {
        /// <summary>
        /// Gets the list of products to be sold
        /// </summary>
        /// <param name="lang">ISO code for language to be used</param>
        /// <param name="countryCode">Country code used for currency</param>
        /// <returns>Products list</returns>
        public IEnumerable<ProductModel> GetProducts(string lang, string countryCode)
        {
            try
            {
                using (AdventureWorksModel model = new AdventureWorksModel())
                {
                    var q = from pm in model.ProductModel
                                .Include("Product")
                                .Include("ProductModelProductDescriptionCulture.ProductDescription")
                            where pm.Product.Any(p => p.FinishedGoodsFlag
                                                   && p.SellStartDate != null 
                                                   && p.SellStartDate <= DateTime.Now) // products we sell only
                            orderby pm.Name
                            select pm;

                    var currencyRate = GetCurrencyRate(countryCode);

                    foreach (var productModel in q)
                    {
                        if (productModel.Product != null)
                        {
                            foreach (var product in productModel.Product)
                            {
                                if (productModel.LowerPrice > 0)
                                    productModel.LowerPrice = Math.Min(productModel.LowerPrice, product.ListPrice);
                                else
                                    productModel.LowerPrice = product.ListPrice;
                            }
                        }

                        productModel.Currency = currencyRate.ToCurrency.CurrencyCode;
                        productModel.LowerPrice = currencyRate.AverageRate * productModel.LowerPrice;
                        productModel.Product = null;
                    }

                    return q.ToList();
                }
            }
            catch (ApplicationException e)
            {
                bool rethrow = ExceptionPolicy.HandleException(e, "Web UI Exception Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            finally
            {
                Logger.Write("End GetProducts", "General");
            }

            return null;
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
            try
            {
                using (AdventureWorksModel model = new AdventureWorksModel())
                {
                    IQueryable<Product> q_products;

                    if (subCategoryIDs != null && subCategoryIDs.Count() > 0)
                    {
                        q_products = from p in model.Product
                                    .Include("ProductSubcategory")
                                    .Include("ProductModel")
                                    .WhereIn(p => p.ProductSubcategory.ProductSubcategoryID, subCategoryIDs)
                            where p.FinishedGoodsFlag
                                && p.SellStartDate != null
                                && p.SellStartDate <= DateTime.Now
                            select p;
                    }
                    else
                    {
                        q_products = from p in model.Product
                                .Include("ProductSubcategory")
                                .Include("ProductModel")
                            where p.FinishedGoodsFlag
                                && p.SellStartDate != null
                                && p.SellStartDate <= DateTime.Now
                            select p;
                    }

                    var q_prices = from p in q_products
                                 group p by p.ProductModel.ProductModelID into pm
                                 select new
                                 {
                                     ProductModelID = pm.Key,
                                     LowerPrice = pm.Min(p => p.ListPrice)
                                 };

                    var currencyRate = GetCurrencyRate(countryCode);

                    var productmodels = q_products.Select(p => p.ProductModel).ToList().Distinct();

                    foreach (var productModel in productmodels)
                    {
                        if (productModel != null)
                        {
                            var pm = q_prices.Where(p => p.ProductModelID == productModel.ProductModelID).FirstOrDefault();

                            if (pm != null)
                                productModel.LowerPrice = pm.LowerPrice;
                        }

                        productModel.Currency = currencyRate.ToCurrency.CurrencyCode;
                        productModel.LowerPrice = currencyRate.AverageRate * productModel.LowerPrice;
                        productModel.Product = null;
                    }

                    return productmodels.ToList();
                }
            }
            catch (ApplicationException e)
            {
                bool rethrow = ExceptionPolicy.HandleException(e, "Web UI Exception Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            finally
            {
                Logger.Write("End GetProducts", "General");
            }

            return null;
        }

        /// <summary>
        /// Gets the main photo of a product
        /// </summary>
        /// <param name="productID">Product ID</param>
        /// <returns>Product photo</returns>
        public ProductPhoto GetProductPrimaryPhoto(int productID)
        {
            try
            {
                using (AdventureWorksModel model = new AdventureWorksModel())
                {
                    var q = from ppp in model.ProductProductPhoto.Include("ProductPhoto")
                            where ppp.Primary && ppp.ProductID == productID // photo principale
                            select ppp.ProductPhoto;

                    return q.FirstOrDefault();
                }
            }
            catch (ApplicationException e)
            {
                bool rethrow = ExceptionPolicy.HandleException(e, "Web UI Exception Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            finally
            {
                Logger.Write("End GetProductPrimaryPhoto", "General");
            }

            return null;
        }

        /// <summary>
        /// Gets all the photos of a product
        /// </summary>
        /// <param name="productID">Product ID</param>
        /// <returns>Product photos</returns>
        public IEnumerable<ProductPhoto> GetProductPhotos(int productID)
        {
            try
            {
                using (AdventureWorksModel model = new AdventureWorksModel())
                {
                    var q = from ppp in model.ProductProductPhoto.Include("ProductPhoto")
                            where ppp.ProductID == productID // photo principale
                            select ppp.ProductPhoto;

                    return q.ToList();
                }
            }
            catch (ApplicationException e)
            {
                bool rethrow = ExceptionPolicy.HandleException(e, "Web UI Exception Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            finally
            {
                Logger.Write("End GetProductPhotos", "General");
            }

            return null;
        }

        /// <summary>
        /// Gets the main photo of a product model
        /// </summary>
        /// <param name="productModelID">Product model ID</param>
        /// <returns>Product photo</returns>
        public ProductPhoto GetProductModelPhoto(int productModelID)
        {
            try
            {
                using (AdventureWorksModel model = new AdventureWorksModel())
                {
                    var q = from ppp in model.ProductProductPhoto.Include("ProductPhoto").Include("Product.ProductModel")
                            where ppp.Primary && ppp.Product.ProductModel.ProductModelID == productModelID // photo principale
                            select ppp.ProductPhoto;

                    return q.FirstOrDefault();
                }
            }
            catch (ApplicationException e)
            {
                bool rethrow = ExceptionPolicy.HandleException(e, "Web UI Exception Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            finally
            {
                Logger.Write("End GetProducts", "General");
            }

            return null;
        }

        /// <summary>
        /// Gets a list of product categories and subcategories
        /// </summary>
        /// <returns>Categories list</returns>
        public IEnumerable<ProductCategory> GetProductCategories()
        {
            try
            {
                using (AdventureWorksModel model = new AdventureWorksModel())
                {
                    var q = from pc in model.ProductCategory.Include("ProductSubcategory")
                            select pc;

                    return q.ToList();
                }
            }
            catch (ApplicationException e)
            {
                bool rethrow = ExceptionPolicy.HandleException(e, "Web UI Exception Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            finally
            {
                Logger.Write("End GetProductCategories", "General");
            }

            return null;
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
            try
            {
                using (AdventureWorksModel model = new AdventureWorksModel())
                {
                    var productModel = (from pm in model.ProductModel.Include("Product").Include("ProductModelProductDescriptionCulture.ProductDescription")
                            where pm.ProductModelID == productModelID
                            select pm).FirstOrDefault();

                    if (productModel != null && productModel.ProductModelProductDescriptionCulture != null) 
                    {
                        // Loads the description for the given language
                        var culture = productModel.ProductModelProductDescriptionCulture.Where(pmpdc => pmpdc.CultureID.ToUpper().Contains(lang.ToUpper())).SingleOrDefault();
                        if (culture != null)
                        {
                            productModel.ProductDescription = culture.ProductDescription;
                        }

                        productModel.ProductModelProductDescriptionCulture = null;
                    }

                    var currencyRate = GetCurrencyRate(countryCode);

                    if (productModel.Product != null)
                    {
                        foreach (var product in productModel.Product)
                        {
                            if (productModel.LowerPrice > 0)
                                productModel.LowerPrice = Math.Min(productModel.LowerPrice, product.ListPrice);
                            else
                                productModel.LowerPrice = product.ListPrice;

                            product.Price = currencyRate.AverageRate * product.ListPrice;
                            product.Currency = currencyRate.ToCurrency.CurrencyCode;
                        }
                    }

                    productModel.Currency = currencyRate.ToCurrency.CurrencyCode;
                    productModel.LowerPrice = currencyRate.AverageRate * productModel.LowerPrice;

                    return productModel;
                }
            }
            catch (ApplicationException e)
            {
                bool rethrow = ExceptionPolicy.HandleException(e, "Web UI Exception Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            finally
            {
                Logger.Write("End GetProductModelDetails", "General");
            }

            return null;
        }

        /// <summary>
        /// Gets the currency rate for a given country
        /// </summary>
        /// <param name="regionCode">Region code</param>
        /// <returns>Currency rate</returns>
        private CurrencyRate GetCurrencyRate(string regionCode) 
        {
            try
            {
                using (AdventureWorksModel model = new AdventureWorksModel())
                {
                    var currency = (from c in model.CountryRegionCurrency.Include("Currency")
                                         where c.CountryRegionCode.ToUpper() == regionCode.ToUpper()
                                         select c.Currency).FirstOrDefault();

                    var currencyRate = from c in model.CurrencyRate.Include("ToCurrency")
                                       where c.ToCurrency.CurrencyCode == currency.CurrencyCode
                                       orderby c.CurrencyRateDate descending
                                       select c;

                    return currencyRate.FirstOrDefault();
                }
            }
            catch (ApplicationException e)
            {
                bool rethrow = ExceptionPolicy.HandleException(e, "Web UI Exception Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            finally
            {
                Logger.Write("End GetCurrencyRate", "General");
            }

            return null;
        }

    }
}
