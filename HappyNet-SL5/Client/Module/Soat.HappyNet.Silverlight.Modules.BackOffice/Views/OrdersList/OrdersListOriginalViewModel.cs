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

namespace Soat.HappyNet.Silverlight.Modules.BackOffice.Views
{
    public class OrdersListViewModel : ViewModel<IOrdersListView>, IOrdersListViewModel, IPagedCollectionView
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

        public SalesOrderHeader[] Orders { get; set; }

        private PagedCollectionView ordersPagedCollection;
        public PagedCollectionView OrdersPagedCollection {
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

        private ICommand pageChangedCommand;
        public ICommand PageChangedCommand
        {
            get
            {
                return this.pageChangedCommand;
            }

            private set
            {
                this.pageChangedCommand = value;
                Notify(() => this.PageChangedCommand);
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
            this.Orders = new SalesOrderHeader[1];
            //this.OrdersPagedCollection = new PagedCollectionView(this.Orders);

            this.OrdersService = salesManagementService;
            this.eventAggregator = eventAggregator;
            //this.ordersController = ordersController;

            this.View = view;
            this.View.Model = this;

            this.SelectionChangedCommand = new DelegateCommand<SalesOrderHeader>(this.OnSelectedOrderChanged);
            this.PageChangedCommand = new DelegateCommand<int>(this.OnPageChanged);

            eventAggregator.Subscribe<LoginChangedEvent>(OnLoginChanged);

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

        public void OnPageChanged(int arg)
        {
            if (arg >= 0)
            {
                if (arg != this.PageIndex && isLoadingANewPage == false)
                {
                    this._pageIndex = arg;
                    this.LoadOrdersCollection();
                }
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
            this.OrdersService.BeginGetOrders(this.PageIndex, GetOrdersCompleted);
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
                    if (this.Orders.Length != OrdersResult.TotalCount)
                    {
                        CleanOrders(OrdersResult.TotalCount);
                    }

                    this.TotalItemCount = OrdersResult.TotalCount;

                    int i;
                    int taille = OrdersResult.Result.Count;
                    for (i = 0; i < taille; i++)
                    {
                        this.Orders[20 * PageIndex + i] = OrdersResult.Result[i];
                    }
                    //this.OrdersPagedCollection.Refresh();

                    int newPageIndex = this.PageIndex;
                    isLoadingANewPage = true;

                    this.OrdersPagedCollection = new PagedCollectionView(this.Orders);

                    if (this.OrdersPagedCollection.PageIndex != newPageIndex)
                    {
                        this.PageIndex = newPageIndex;
                        this.OrdersPagedCollection.MoveToPage(this.PageIndex);
                        this.SelectedOrder = null;
                    }

                    isLoadingANewPage = false;
                }
                else
                {
                    this.eventAggregator.Publish(new MessageShowEvent
                    {
                        MessageTitle = "Oops...",
                        Message = args.Error.Message
                    });
                }
            }
            IsLoading = false;
        }

        private void CleanOrders(int size)
        {
            this.Orders = new SalesOrderHeader[size];
        }

        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>

        #region IPagedCollectionView Members

        #region Properties

        private bool _canChangePage;
        public bool CanChangePage
        {
            get { return this._canChangePage; }
        }

        private bool _isPageChanging;
        public bool IsPageChanging
        {
            get { return this._isPageChanging; }
        }

        private int _itemCount;
        public int ItemCount
        {
            get { return this._itemCount; }
        }

        private int _pageIndex;
        public int PageIndex
        {
            get { return this._pageIndex; }
            private set
            {
                if (_pageIndex != value)
                {
                    _pageIndex = value;
                    Notify(() => this.PageIndex);
                }
            }
        }

        private int _pageSize;
        public int PageSize
        {
            get
            {
                return this._pageSize;
            }
            set
            {
                // TODO
                this._pageSize = value;
            }
        }

        private int _totalItemCount;
        public int TotalItemCount
        {
            get { return this._totalItemCount; }
            set
            {
                if (_totalItemCount != value)
                {
                    _totalItemCount = value;
                    Notify(() => this.TotalItemCount);
                }
            }
        }

        #endregion

        #region Private members

        private int PageCount
        {
            get { return (this.PageSize > 0) ? Math.Max(1, (int)Math.Ceiling((double)this.ItemCount / this.PageSize)) : 0; }
        }

        private IEnumerable _sourceCollection;

        #endregion

        #region Events

        public event EventHandler<EventArgs> PageChanged;

        public event EventHandler<PageChangingEventArgs> PageChanging;

        #endregion

        private delegate IEnumerable FetchDataDelegate(int pageIndex, int pageSize);

        private FetchDataDelegate fetchData;

        public bool MoveToFirstPage()
        {
            return this.MoveToPage(0);
        }

        public bool MoveToLastPage()
        {
            if (this.TotalItemCount != -1 && this.PageSize > 0)
            {
                return this.MoveToPage(this.PageCount - 1);
            }
            else
            {
                return false;
            }
        }

        public bool MoveToNextPage()
        {
            return this.MoveToPage(this.PageIndex + 1);
        }

        public bool MoveToPreviousPage()
        {
            return this.MoveToPage(this.PageIndex - 1);
        }

        public bool MoveToPage(int pageIndex)
        {
            // Appel au ws ?

            // Mise à jour du nombre d'éléments

            return false;
        }

        #endregion

        #region INotifyPropertyChanged Members

        //public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
