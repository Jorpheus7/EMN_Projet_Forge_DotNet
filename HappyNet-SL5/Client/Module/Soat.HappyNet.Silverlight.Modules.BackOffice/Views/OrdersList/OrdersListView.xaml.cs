// -----------------------------------------------------------------------
// <copyright file="OrdersListView.xaml.cs" company="So@t">
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

namespace Soat.HappyNet.Silverlight.Modules.BackOffice.Views
{
    public partial class OrdersListView : UserControl, IOrdersListView
    {
        #region Model property

        public IOrdersListViewModel Model
        {
            get { return this.DataContext as IOrdersListViewModel; }
            set { this.DataContext = value; }
        }

        #endregion

        #region Constructor

        public OrdersListView()
        {
            InitializeComponent();
        } 

        #endregion
    }
}
