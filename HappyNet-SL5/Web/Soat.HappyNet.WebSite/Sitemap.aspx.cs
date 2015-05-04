// -----------------------------------------------------------------------
// <copyright file="Sitemap.aspx.cs" company="So@t">
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

namespace Soat.HappyNet.WebSite
{
    public partial class Sitemap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            rptUrl.DataSource = XmlPageMappings.PageMappings;
            rptUrl.DataBind();

            this.Response.ContentType = "text/xml";
        }
    }
}
