// -----------------------------------------------------------------------
// <copyright file="ISalesManagementService.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using Soat.HappyNet.Silverlight.Library;
using System.Collections.ObjectModel;
using Soat.HappyNet.Server.DataContract;
using Soat.HappyNet.Silverlight.Modules.BackOffice.OrdersServiceReference;

namespace Soat.HappyNet.Silverlight.Modules.BackOffice.Services
{
    public interface ISalesManagementService
    {
        void BeginGetOrders(int pageNumber, ServiceCompleted<Orders> GetOrdersCompleted);
        void BeginGetOrderBySalesOrderId(int salesOrderId, ServiceCompleted<SalesOrderHeader> GetOrderBySalesOrderIdCompleted);
        void BeginGetOrderBySalesOrderId(int salesOrderId, ServiceCompleted<SalesOrderHeader> GetOrderBySalesOrderIdCompleted, object userState);
    }
}
