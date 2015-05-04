// -----------------------------------------------------------------------
// <copyright file="IOrdersDataProvider.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using Soat.HappyNet.Server.DataContract.Orders;
using Soat.HappyNet.Server.Entities;

using SalesOrders = Soat.HappyNet.Server.DataContract.Orders.Orders;

namespace Soat.HappyNet.Server.Services.Orders.DataAccessLayer
{
    public interface IOrdersDataProvider
    {
        SalesOrders GetOrders(int pageNumber, int pageSize, string lang, string countryCode);
        SalesOrderHeader GetOrderBySalesOrderId(int salesOrderId, string lang, string countryCode);
    }
}
