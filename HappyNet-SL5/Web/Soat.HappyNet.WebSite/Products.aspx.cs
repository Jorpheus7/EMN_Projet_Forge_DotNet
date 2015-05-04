// -----------------------------------------------------------------------
// <copyright file="Products.aspx.cs" company="So@t">
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

namespace Soat.HappyNet.WebSite
{
    public partial class Products : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadProducts();
            }
        }

        /// <summary>
        /// Loads all the products and bind it to the repeater
        /// </summary>
        void LoadProducts()
        {
            var lang = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            var region = new RegionInfo(Thread.CurrentThread.CurrentUICulture.LCID).TwoLetterISORegionName;

            ProductionService service = new ProductionService();
            rptProducts.DataSource = service.GetProducts(lang, region);
            rptProducts.DataBind();
        }
    }
}
