// -----------------------------------------------------------------------
// <copyright file="Product.aspx.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Soat.HappyNet.Server.Services.Production;
using System.Threading;
using System.Globalization;
using Soat.HappyNet.Server.Entities;

namespace Soat.HappyNet.WebSite
{
    public partial class Product : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string product = Request.QueryString.Get("product");
                if (!string.IsNullOrEmpty(product))
                {
                    int productID = -1;
                    if (int.TryParse(product, out productID))
                    {
                        LoadProduct(productID);
                    }
                }
            }
        }

        /// <summary>
        /// Loads a product model
        /// </summary>
        /// <param name="productID"></param>
        void LoadProduct(int productID)
        {
            var lang = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            var region = new RegionInfo(Thread.CurrentThread.CurrentUICulture.LCID).TwoLetterISORegionName;

            ProductionService service = new ProductionService();
            var productModel = service.GetProductModelDetails(productID, lang, region);

            BindProduct(productModel);
        }

        /// <summary>
        /// Binds a product model to the view
        /// </summary>
        /// <param name="productModel"></param>
        void BindProduct(ProductModel productModel)
        {
            lblName.Text = productModel.Name;
            lblPrice.Text = string.Format("{0:0.00} {1}", productModel.LowerPrice, productModel.Currency);
            lblDescription.Text = productModel.ProductDescription.Description;

            // Products
            rptProducts.DataSource = productModel.Product;
            rptProducts.DataBind();
        }
    }
}
