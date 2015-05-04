// -----------------------------------------------------------------------
// <copyright file="ShoppingCart.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Soat.HappyNet.Silverlight.Modules.SalesOrder.SalesOrderServiceReference;

namespace Soat.HappyNet.Silverlight.Modules.SalesOrder.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Items = new ObservableCollection<ShoppingCartItem>();
        }

        public ObservableCollection<ShoppingCartItem> Items { get; private set; }
    }
}
