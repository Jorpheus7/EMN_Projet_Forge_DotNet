// -----------------------------------------------------------------------
// <copyright file="HomePage.xaml.cs" company="So@t">
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
using Soat.HappyNet.Silverlight.Library.Controls;
using Soat.HappyNet.Silverlight.Infrastructure;
using Soat.HappyNet.Silverlight.Infrastructure.Components;
using Soat.HappyNet.Silverlight.Modules.Main.Models;

namespace Soat.HappyNet.Silverlight.Modules.Main.ViewPages
{
    /// <summary>
    /// Home Page
    /// </summary>
    public partial class HomePage : LocalizedPage
    {
        LocalizedStrings localization;

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public HomePage() 
            : base()
        {
            InitializeComponent();

            InitDataGrid();
        }

        #endregion

        /// <summary>
        /// Sets the current title of the page
        /// </summary>
        protected override void SetTitle()
        {
            this.Title = StringLibrary.PAGE_Home;
        }

        /// <summary>
        /// Initializes the static data grid
        /// </summary>
        void InitDataGrid()
        {
            // DataGrid source ne supporte pas les types anonymes ?
            LastProductsDataGrid.ItemsSource = new List<object>
            {
                new TopProduct { Position = "1", Name = "Voilà un produit ...", Description = "Superbe", Price = "2 EUR" },
                new TopProduct { Position = "2", Name = "Oh un deuxième !", Description = "Magnifique", Price = "80 EUR" },
                new TopProduct { Position = "3", Name = "C'est sans compter le troisième", Description = "Incroyable", Price = "100 EUR" },
                new TopProduct { Position = "4", Name = "Produit #4", Description = "Magique et révolutionnaire", Price = "499 USD" }
            };
        }
    }
}
