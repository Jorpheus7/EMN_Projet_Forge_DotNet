// -----------------------------------------------------------------------
// <copyright file="IOrdersBusinessProvider.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Soat.HappyNet.Server.DataContract;
using Soat.HappyNet.Server.DataContract.Orders;
using Soat.HappyNet.Server.Entities;
using Soat.HappyNet.Server.Services.Orders.DataAccessLayer;

using SalesOrders = Soat.HappyNet.Server.DataContract.Orders.Orders;

namespace Soat.HappyNet.Server.Services.Orders.BusinessLayer
{
    public interface IOrdersBusinessProvider
    {
        IOrdersDataProvider OrdersDataProvider { get; }

        SalesOrders GetOrders(int pageNumber, int pageSize, string lang, string countryCode);
        SalesOrderHeader GetOrderBySalesOrderId(int salesOrderId, string lang, string countryCode);
    }
}
