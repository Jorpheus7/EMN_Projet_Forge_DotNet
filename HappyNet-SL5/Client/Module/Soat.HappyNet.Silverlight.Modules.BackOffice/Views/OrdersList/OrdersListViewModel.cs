// -----------------------------------------------------------------------
// <copyright file="OrdersListViewModel.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.Unity;
using Soat.HappyNet.Server.DataContract;
using Soat.HappyNet.Silverlight.Modules.BackOffice.Controllers;
using Soat.HappyNet.Silverlight.Modules.BackOffice.OrdersServiceReference;
using Soat.HappyNet.Silverlight.Modules.BackOffice.Services;
using Soat.HappyNet.Silverlight.Infrastructure.Events;
using Soat.HappyNet.Silverlight.Library;
using Soat.HappyNet.Silverlight.Library.Extensions;
using Soat.HappyNet.Silverlight.Modules.BackOffice.Views.OrdersList;
using Soat.HappyNet.Silverlight.Library.Components.CollectionView;

namespace Soat.HappyNet.Silverlight.Modules.BackOffice.Views
{
    public class OrdersListViewModel : ViewModel<IOrdersListView>, IOrdersListViewModel
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

        private PagedSortableCollectionView<SalesOrderHeader> ordersPagedCollection;
        public PagedSortableCollectionView<SalesOrderHeader> OrdersPagedCollection
        {
            get { return this.ordersPagedCollection; }
            set
            {
                if (value != this.ordersPagedCollection)
                {
                    this.ordersPagedCollection = value;
                    Notify(() => this.OrdersPagedCollection);
                }
            }
        }

        private SalesOrderHeader selectedOrder;
        public SalesOrderHeader SelectedOrder
        {
            get { return selectedOrder; }
            set
            {
                if (selectedOrder != value)
                {
                    selectedOrder = value;
                    Notify(() => this.SelectedOrder);
                }
            }
        }

        private ICommand selectionChangedCommand;
        public ICommand SelectionChangedCommand
        {
            get
            {
                return this.selectionChangedCommand;
            }

            private set
            {
                this.selectionChangedCommand = value;
                Notify(() => this.SelectionChangedCommand);
            }
        }

        #endregion

        #region Private members

        private bool isLoadingANewPage = false;

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

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="view">View attached to this ViewModel</param>
        /// <param name="eventAggregator">Event aggregator</param>
        public OrdersListViewModel(ISalesManagementService salesManagementService,
            IOrdersListView view,
            IEventAggregator eventAggregator)
        {
            this.OrdersService = salesManagementService;
            this.eventAggregator = eventAggregator;
            //this.ordersController = ordersController;

            this.View = view;
            this.View.Model = this;

            this.SelectionChangedCommand = new DelegateCommand<SalesOrderHeader>(this.OnSelectedOrderChanged);

            eventAggregator.Subscribe<LoginChangedEvent>(OnLoginChanged);

            this.OrdersPagedCollection = new PagedSortableCollectionView<SalesOrderHeader>();
            this.OrdersPagedCollection.PageSize = 20;
            this.OrdersPagedCollection.OnRefresh += new EventHandler<RefreshEventArgs>(OrdersPagedCollection_OnRefresh);

            this.LoadOrdersCollection();
        }

        #endregion

        #region PagedSortableCollectionView management

        void OrdersPagedCollection_OnRefresh(object sender, RefreshEventArgs e)
        {
            this.OrdersPagedCollection.CanChangePage = false;
            this.LoadOrdersCollection();
        }

        #endregion


        public void OnLoginChanged(LoginChangedEvent args)
        {
            if (args.IsLogged)
            {
                this.LoadOrdersCollection();
            }
        }

        public void OnSelectedOrderChanged(SalesOrderHeader soh)
        {
            eventAggregator.Publish(new OrderSelectedEvent(soh));
        }

        /// <summary>
        /// Loads the orders collection
        /// </summary>
        public void LoadOrdersCollection()
        {
            IsLoading = true;
            SelectedOrder = null;
            this.OrdersService.BeginGetOrders(this.OrdersPagedCollection.PageIndex, GetOrdersCompleted);
        }

        /// <summary>
        /// Treats the return from the LoadOrdersCollection call
        /// </summary>,
        /// <param name="args">Argument du retour</param>
        public void GetOrdersCompleted(ServiceArgs<Orders> args)
        {
            if (args.Error == null)
            {
                var OrdersResult = args.Result as Orders;

                if (OrdersResult != null)
                {
                    this.OrdersPagedCollection.Clear();
                    foreach (SalesOrderHeader soh in OrdersResult.Result)
                    {
                        this.OrdersPagedCollection.Add(soh);
                    }

                    this.OrdersPagedCollection.TotalItemCount = OrdersResult.TotalCount;
                }
            }
            else
            {
                this.eventAggregator.Publish(new MessageShowEvent
                {
                    MessageTitle = "Oops...",
                    Message = args.Error.Message
                });
            }

            OrdersPagedCollection.CanChangePage = true;
            IsLoading = false;
        }

        #region ICleanable

        public override void Clean()
        {
            this.OrdersPagedCollection.OnRefresh -= new EventHandler<RefreshEventArgs>(OrdersPagedCollection_OnRefresh);
            this.OrdersPagedCollection.Clear();
            
            base.Clean();
        }
        
        #endregion
    }
}
