// -----------------------------------------------------------------------
// <copyright file="OrdersBusinessProvider.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Net;
using Soat.HappyNet.Server.Common;
using Soat.HappyNet.Server.Entities;
using Soat.HappyNet.Server.Services.Orders.DataAccessLayer;
using Soat.HappyNet.Server.DataContract;
using System.Collections.Generic;
using Soat.HappyNet.Server.DataContract.Orders;
using SalesOrders = Soat.HappyNet.Server.DataContract.Orders.Orders;

namespace Soat.HappyNet.Server.Services.Orders.BusinessLayer
{
    public class OrdersBusinessProvider : IOrdersBusinessProvider
    {
        #region Membres
        
        /// <summary>
        /// Data Provider for production
        /// </summary>
        public IOrdersDataProvider OrdersDataProvider { get; private set; }

        #endregion

        #region Constructeur

        /// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="personDataProvider">Data provider for production</param>
        public OrdersBusinessProvider(IOrdersDataProvider ordersDataProvider)
		{
            this.OrdersDataProvider = ordersDataProvider;
		}

        #endregion

        public SalesOrders GetOrders(int pageNumber, int pageSize, string lang, string countryCode)
        {
            return this.OrdersDataProvider.GetOrders(pageNumber, pageSize, lang, countryCode);
        }


        public SalesOrderHeader GetOrderBySalesOrderId(int salesOrderId, string lang, string countryCode)
        {
            return this.OrdersDataProvider.GetOrderBySalesOrderId(salesOrderId, lang, countryCode);
        }
    }
}
