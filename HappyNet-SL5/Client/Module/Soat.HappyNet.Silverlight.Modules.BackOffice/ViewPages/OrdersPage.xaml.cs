// -----------------------------------------------------------------------
// <copyright file="OrdersPage.xaml.cs" company="So@t">
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
using Soat.HappyNet.Silverlight.Infrastructure.Components;
using Soat.HappyNet.Silverlight.Infrastructure.Localization;

namespace Soat.HappyNet.Silverlight.Modules.BackOffice.ViewPages
{
    public partial class OrdersPage : LocalizedPage
    {
        public OrdersPage()
            : base()
        {
            InitializeComponent();
        }

        protected override void SetTitle()
        {
            this.Title = StringLibrary.PAGE_Orders;
        }
    }
}
