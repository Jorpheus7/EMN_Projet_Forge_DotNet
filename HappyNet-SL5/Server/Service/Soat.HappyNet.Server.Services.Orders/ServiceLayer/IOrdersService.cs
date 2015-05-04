// -----------------------------------------------------------------------
// <copyright file="IOrdersService.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ServiceModel;
using Soat.HappyNet.Server.DataContract;
using Soat.HappyNet.Server.Entities;
using Soat.HappyNet.Server.DataContract.Orders;
using SalesOrders = Soat.HappyNet.Server.DataContract.Orders.Orders;

namespace Soat.HappyNet.Server.Services.Orders
{
    /// <summary>
    /// Interface defining the contract of services for orders
    /// </summary>
    [ServiceContract]
    public interface IOrdersService
    {
        [OperationContract]
        SalesOrders GetOrders(int pageNumber, int pageSize, string lang, string countryCode);

        [OperationContract]
        SalesOrderHeader GetOrderBySalesOrderId(int salesOrderId, string lang, string countryCode);
    }
}
