// -----------------------------------------------------------------------
// <copyright file="ShoppingCartView.xaml.cs" company="So@t">
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

namespace Soat.HappyNet.Silverlight.Modules.SalesOrder.Views
{
    /// <summary>
    /// Class defining the ShoppingCart View
    /// </summary>
    public partial class ShoppingCartView : UserControl, IShoppingCartView
    {
        #region Model property

        /// <summary>
        /// ViewModel attached to the View
        /// </summary>
        public IShoppingCartViewModel Model
        {
            get { return this.DataContext as IShoppingCartViewModel; }
            set { this.DataContext = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ShoppingCartView()
        {
            InitializeComponent();
        }

        #endregion

        #region Shopping Cart
        /// <summary>
        /// Displays the shopping cart
        /// </summary>
        public void ShowShoppingCart()
        {
            this.ShoppingCartExpander.IsExpanded = true;
        }

        /// <summary>
        /// Hides the shopping cart
        /// </summary>
        public void HideShoppingCart()
        {
            this.ShoppingCartExpander.IsExpanded = false;
        }

        #endregion
    }
}
