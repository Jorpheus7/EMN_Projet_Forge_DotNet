// -----------------------------------------------------------------------
// <copyright file="BackOffice.Events.cs" company="So@t">
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
using Microsoft.Practices.Composite.Presentation.Events;
using Soat.HappyNet.Silverlight.Modules.BackOffice.OrdersServiceReference;

namespace Soat.HappyNet.Silverlight.Infrastructure.Events
{
    public class PageChangedEvent : CompositePresentationEvent<PageChangedEvent>
    {
        public int PageNumber { get; private set; }

        public PageChangedEvent()
        {

        }

        public PageChangedEvent(int pageNumber)
        {
            PageNumber = pageNumber;
        }
    }


    public class OrderSelectedEvent : CompositePresentationEvent<OrderSelectedEvent>
    {
        public SalesOrderHeader SelectedOrder { get; private set; }

        public OrderSelectedEvent()
        {
        }

        public OrderSelectedEvent(SalesOrderHeader selectedOrder)
        {
            this.SelectedOrder = selectedOrder;
        }
    }
}
