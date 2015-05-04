// -----------------------------------------------------------------------
// <copyright file="Site.Master.cs" company="So@t">
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
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Formats the page name + query before mapping
                string htmlPage = this.Request.Url.PathAndQuery;
                htmlPage = htmlPage.Replace(string.Format("{0}/", this.Request.ApplicationPath), string.Empty);

                // Looking for the page in the mapping file
                string deepLink = XmlPageMappings.GetSilverlightUrl(htmlPage);

                // Redirect to the anchor so it can be received by the Silverlight client through NavigationFramework
                Response.Write("<script type=text/javascript>window.location.hash='#" + deepLink + "';</script>");
            }
        }
    }
}
