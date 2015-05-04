// -----------------------------------------------------------------------
// <copyright file="OrderDetailsViewModel.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Unity;
using Soat.HappyNet.Silverlight.Modules.BackOffice.OrdersServiceReference;
using Soat.HappyNet.Silverlight.Modules.BackOffice.Services;
using Soat.HappyNet.Silverlight.Infrastructure.Events;
using Soat.HappyNet.Silverlight.Library;
using Soat.HappyNet.Silverlight.Library.Extensions;

namespace Soat.HappyNet.Silverlight.Modules.BackOffice.Views
{
    public class OrderDetailsViewModel : ViewModel<IOrderDetailsView>, IOrderDetailsViewModel
    {
        #region Properties

        /// <summary>
        /// Loading indicator
        /// </summary>
        bool isLoading = false;
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                if (isLoading != value)
                {
                    isLoading = value;
                    Notify(() => this.IsLoading);
                }
            }
        }

        /// <summary>
        /// Indicator to know if the order detail has been loaded
        /// </summary>
        private bool isOrderLoaded = false;
        public bool IsOrderLoaded
        {
            get { return isOrderLoaded; }
            set
            {
                if (isOrderLoaded != value)
                {
                    isOrderLoaded = value;
                    Notify(() => this.IsOrderLoaded);
                }
            }
        }

        /// <summary>
        /// Selected order
        /// </summary>
        private SalesOrderHeader currentOrder;
        public SalesOrderHeader CurrentOrder
        {
            get { return currentOrder; }
            set
            {
                if (currentOrder != value)
                {
                    currentOrder = value;
                    Notify(() => this.CurrentOrder);
                }
            }
        }

        #endregion

        #region Private members

        /// <summary>
        /// Event aggregator
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        /// <summary>
        /// Unity container
        /// </summary>
        private readonly IUnityContainer container;

        /// <summary>
        /// Web service of orders
        /// </summary>
        private readonly ISalesManagementService OrdersService;

        private Guid lastGetOrderBySalesOrderId;

        #endregion
        
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="view">View attached to this ViewModel</param>
        /// <param name="eventAggregator">Event aggregator</param>
        public OrderDetailsViewModel(ISalesManagementService salesManagementService,
            IOrderDetailsView view,
            IEventAggregator eventAggregator,
            IUnityContainer container)
        {
            this.OrdersService = salesManagementService;
            this.eventAggregator = eventAggregator;
            this.container = container;

            this.View = view;
            this.View.Model = this;

            eventAggregator.Subscribe<OrderSelectedEvent>(OnOrderSelected);
        }

        #endregion

        public void OnOrderSelected(OrderSelectedEvent args)
        {
            if (args.SelectedOrder != null)
            {
                this.IsLoading = true;
                this.IsOrderLoaded = false;

                VisualStateManager.GoToState(View as Control, "NoDetails", true);

                this.LoadOrder(args.SelectedOrder.SalesOrderID);
            }
        }

        private void LoadOrder(int salesOrderId)
        {
            this.lastGetOrderBySalesOrderId = Guid.NewGuid();

            this.OrdersService.BeginGetOrderBySalesOrderId(salesOrderId, GetOrderBySalesOrderIdCompleted, lastGetOrderBySalesOrderId);
        }

        private void GetOrderBySalesOrderIdCompleted(ServiceArgs<SalesOrderHeader> args)
        {
            // Checks if it's the last call passed to the web service
            if ((Guid)args.UserState != lastGetOrderBySalesOrderId)
                return;

            //if (args.Error == null)
            if (!args.IsResultNullOrError)
            {
                SalesOrderHeader soh = args.Result as SalesOrderHeader;
                if (soh != null)
                {
                    this.CurrentOrder = soh;

                    this.IsLoading = false;
                    this.IsOrderLoaded = true;

                    VisualStateManager.GoToState(View as Control, "DetailsVisible", true);
                }
            }
        }
    }
}
