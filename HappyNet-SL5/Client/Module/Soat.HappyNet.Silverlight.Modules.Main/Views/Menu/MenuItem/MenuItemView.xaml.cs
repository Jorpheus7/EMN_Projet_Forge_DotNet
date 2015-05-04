// -----------------------------------------------------------------------
// <copyright file="MenuItemView.xaml.cs" company="So@t">
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

namespace Soat.HappyNet.Silverlight.Modules.Main.Views
{
    /// <summary>
    /// Class defining the MenuItem View
    /// </summary>
    public partial class MenuItemView : UserControl, IMenuItemView
    {
        #region Model property

        /// <summary>
        /// ViewModel attached to the View
        /// </summary>
        public IMenuItemViewModel Model
        {
            get { return this.DataContext as IMenuItemViewModel; }
            set { this.DataContext = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MenuItemView()
        {
            InitializeComponent();
        }

        #endregion
    }
}
