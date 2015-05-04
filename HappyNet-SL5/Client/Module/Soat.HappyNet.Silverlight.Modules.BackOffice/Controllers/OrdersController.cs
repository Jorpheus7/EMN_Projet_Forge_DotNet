// -----------------------------------------------------------------------
// <copyright file="OrdersController.cs" company="So@t">
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
using Soat.HappyNet.Silverlight.Modules.BackOffice.Views;
using Microsoft.Practices.Composite.Events;
using Soat.HappyNet.Silverlight.Modules.BackOffice.OrdersServiceReference;
using Soat.HappyNet.Silverlight.Infrastructure.Events;
using Soat.HappyNet.Silverlight.Library.Extensions;
using Soat.HappyNet.Silverlight.Infrastructure.Localization;
using Soat.HappyNet.Silverlight.Infrastructure.UriNames;

namespace Soat.HappyNet.Silverlight.Modules.BackOffice.Controllers
{
    public class OrdersController : IOrdersController
    {
        /// <summary>
        /// Event aggregator
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        /// <summary>
        /// Order details view model
        /// </summary>
        private readonly IOrderDetailsViewModel orderDetailsViewModel;

        public OrdersController(IEventAggregator eventAggregator, IOrderDetailsViewModel orderDetailsViewModel)
        {
            this.eventAggregator = eventAggregator;
            this.orderDetailsViewModel = orderDetailsViewModel;
        }

        public void Run()
        {
            // Subscribes to authentification event to display (or not) the button menu to access orders
            eventAggregator.Subscribe<LoginChangedEvent>(OnLoginChanged);
        }

        public void SelectedOrderChanged(SalesOrderHeader salesOrderHeader)
        {
            //this.orderDetailsViewModel.SetCurrentOrder(salesOrderHeader);
        }

        public void OnLoginChanged(LoginChangedEvent args)
        {
            if (args.IsLogged)
            {
                this.eventAggregator.Publish(new AddMenuButtonEvent()
                {
                    NameProvider = () => StringLibrary.MENU_BackOffice,
                    Url = UriNames.BackOfficeUri
                });
            }
            else
            {
                this.eventAggregator.Publish(new RemoveMenuButtonEvent()
                {
                    Url = UriNames.BackOfficeUri
                });
            }
        }
    }
}
