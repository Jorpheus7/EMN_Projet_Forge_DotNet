// -----------------------------------------------------------------------
// <copyright file="OrdersDataProvider.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Collections.Generic;
using Soat.HappyNet.Server.DataContract;
using Soat.HappyNet.Server.Common.Extensions;
using Soat.HappyNet.Server.Entities;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Soat.HappyNet.Server.DataContract.Orders;
using SalesOrders = Soat.HappyNet.Server.DataContract.Orders.Orders;

namespace Soat.HappyNet.Server.Services.Orders.DataAccessLayer
{

    public class OrdersDataProvider : IOrdersDataProvider
    {
        public SalesOrders GetOrders(int pageNumber, int pageSize, string lang, string countryCode)
        {
            SalesOrders res = new SalesOrders();

            try
            {
                using (AdventureWorksModel model = new AdventureWorksModel())
                {
                    res.TotalCount = (from so in model.SalesOrderHeader
                                      select so).Count();
                    res.PageNumber = pageNumber;

                    res.Result = (from so in model.SalesOrderHeader
                                .Include("Customer")
                                .Include("Customer.Person")
                                  orderby so.SalesOrderNumber descending
                                  select so)
                            .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            finally
            {
                Logger.Write("End GetOrders", "General");
            }

            return res;
        }

        public SalesOrderHeader GetOrderBySalesOrderId(int salesOrderId, string lang, string countryCode)
        {
            SalesOrderHeader soh = new SalesOrderHeader();
            try
            {
                using (AdventureWorksModel model = new AdventureWorksModel())
                {
                    var q = (from so in model.SalesOrderHeader
                                .Include("Address")
                                .Include("Customer")
                                .Include("Customer.Person")
                                .Include("SalesOrderDetail")
                             where so.SalesOrderID == salesOrderId
                             select so).FirstOrDefault();

                    soh = q as SalesOrderHeader;

                    return soh;
                }
            }
            finally
            {
                Logger.Write("End GetOrderBySalesOrderId", "General");
            }
        }
    }
}
