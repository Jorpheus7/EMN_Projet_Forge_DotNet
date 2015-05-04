// -----------------------------------------------------------------------
// <copyright file="SalesManagementService.cs" company="So@t">
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
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Practices.Unity;
using Soat.HappyNet.Silverlight.Infrastructure.Models;
using Soat.HappyNet.Silverlight.Library;
using Soat.HappyNet.Server.DataContract;
using Soat.HappyNet.Silverlight.Infrastructure.Helpers;
using Soat.HappyNet.Silverlight.Modules.BackOffice.OrdersServiceReference;

namespace Soat.HappyNet.Silverlight.Modules.BackOffice.Services
{
    public class SalesManagementService : ISalesManagementService
    {
        #region Const

        /// <summary>
        /// Configuration name for the client proxy (cf. ServiceReferences.ClientConfig in the Shell Project)
        /// </summary>
        const string ServiceConfigurationName = "OrdersServiceConfiguration";

        #endregion

        #region Private members

        /// <summary>
        /// Web services proxy
        /// </summary>
        private readonly OrdersServiceClient client;

        /// <summary>
        /// Unity container
        /// </summary>
        private readonly IUnityContainer container;

        #endregion
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public SalesManagementService(IUnityContainer container)
        {
            this.container = container;
            this.client = new OrdersServiceClient(SalesManagementService.ServiceConfigurationName);

            client.GetOrdersCompleted += new EventHandler<GetOrdersCompletedEventArgs>(OnGetOrdersCompleted);
            client.GetOrderBySalesOrderIdCompleted += new EventHandler<GetOrderBySalesOrderIdCompletedEventArgs>(OnGetOrderBySalesOrderIdCompleted);
        }

        #region GetOrders

        /// <summary>
        /// Retrieves a list of orders
        /// </summary>
        /// <param name="GetOrdersCompleted">Callback method</param>
        public void BeginGetOrders(int pageIndex,
            ServiceCompleted<Orders> GetOrdersCompleted)
        {
            // Gets current language
            var lang = container.Resolve<Language>();

            // Inject credentials within the header of SOAP messages
            OperationContextHelper.Create(client);

            client.GetOrdersAsync(pageIndex + 1, 20, lang.CultureID, lang.CountryCode,
                new ServiceArgs<Orders>(GetOrdersCompleted));
        }

        void OnGetOrdersCompleted(object sender, GetOrdersCompletedEventArgs e)
        {
            Orders result = new Orders();
            if (e.Error == null)
            {
                result = e.Result as Orders;
            }

            var arg = e.UserState as ServiceArgs<Orders>;
            arg.Complete(result, e);
        }

        #endregion

        #region GetOrderBySalesOrderId

        /// <summary>
        /// Retrieves an order
        /// </summary>
        /// <param name="GetOrdersCompleted">Callback method</param>
        public void BeginGetOrderBySalesOrderId(int salesOrderId,
            ServiceCompleted<SalesOrderHeader> GetOrderBySalesOrderIdCompleted)
        {
            // Gets current language
            var lang = container.Resolve<Language>();

            // Inject credentials within the header of SOAP messages
            OperationContextHelper.Create(client);

            client.GetOrderBySalesOrderIdAsync(salesOrderId, lang.CultureID, lang.CountryCode,
                new ServiceArgs<SalesOrderHeader>(GetOrderBySalesOrderIdCompleted));
        }

        /// <summary>
        /// Retrieves an order
        /// </summary>
        /// <param name="GetOrdersCompleted">Callback method</param>
        public void BeginGetOrderBySalesOrderId(int salesOrderId,
            ServiceCompleted<SalesOrderHeader> GetOrderBySalesOrderIdCompleted, object userState)
        {
            // Gets current language
            var lang = container.Resolve<Language>();

            // Inject credentials within the header of SOAP messages
            OperationContextHelper.Create(client);

            client.GetOrderBySalesOrderIdAsync(salesOrderId, lang.CultureID, lang.CountryCode,
                new ServiceArgs<SalesOrderHeader>(GetOrderBySalesOrderIdCompleted, userState));
        }

        void OnGetOrderBySalesOrderIdCompleted(object sender, GetOrderBySalesOrderIdCompletedEventArgs e)
        {
            SalesOrderHeader result = new SalesOrderHeader();
            if (e.Error == null)
            {
                result = e.Result as SalesOrderHeader;
            }

            var arg = e.UserState as ServiceArgs<SalesOrderHeader>;
            arg.Complete(result, e);
        }

        #endregion
    }
}
