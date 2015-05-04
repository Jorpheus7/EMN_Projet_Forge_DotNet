// -----------------------------------------------------------------------
// <copyright file="ProductsPage.xaml.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using Soat.HappyNet.Silverlight.Infrastructure.Localization;
using System.ComponentModel;
using Soat.HappyNet.Silverlight.Modules.Production.Views;
using Soat.HappyNet.Silverlight.Infrastructure;
using Soat.HappyNet.Silverlight.Library.Controls;
using Soat.HappyNet.Silverlight.Infrastructure.RegionNames;
using Soat.HappyNet.Silverlight.Infrastructure.Components;

namespace Soat.HappyNet.Silverlight.Modules.Production.ViewPages
{
    /// <summary>
    /// Products Page
    /// </summary>
    public partial class ProductsPage : LocalizedPage
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ProductsPage()
            : base()
        {
            InitializeComponent();
        }

        #endregion

        /// <summary>
        /// Sets the current title of the page
        /// </summary>
        protected override void SetTitle()
        {
            this.Title = StringLibrary.PAGE_Products;
        }

        /// <summary>
        /// Handles query strings when navigated to
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Saves the queryString to treat it afterwards
            Globals.QueryString = NavigationContext.QueryString;
        }
    }
}
