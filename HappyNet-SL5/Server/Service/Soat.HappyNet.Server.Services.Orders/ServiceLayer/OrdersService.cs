// -----------------------------------------------------------------------
// <copyright file="OrdersService.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.ServiceModel;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Soat.HappyNet.Server.DataContract;
using Soat.HappyNet.Server.Entities;
using Soat.HappyNet.Server.Services.Orders.BusinessLayer;
using System.ServiceModel.Activation;
using Soat.HappyNet.Server.DataContract.Orders;
using SalesOrders = Soat.HappyNet.Server.DataContract.Orders.Orders;
using Soat.HappyNet.Server.Services.Authentication.Authentication;

namespace Soat.HappyNet.Server.Services.Orders
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class OrdersService : IOrdersService
    {
        #region Const

        /// <summary>
        /// Section name for unity configuration
        /// </summary>
        const string UNITY_SECTION_NAME = "unity";

        /// <summary>
        /// Container name for Unity configuration
        /// </summary>
        const string UNITY_CONTAINER_NAME = "ordersServiceContainer";

        #endregion

        #region Container Property

        private IUnityContainer container;
        /// <summary>
        /// Unity container
        /// </summary>
        public IUnityContainer Container
        {
            get
            {
                if (this.container == null)
                {
                    this.container = new UnityContainer();
                }
                return this.container;
            }
        }

        #endregion
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public OrdersService()
        {
            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection(OrdersService.UNITY_SECTION_NAME);
            section.Containers[OrdersService.UNITY_CONTAINER_NAME].Configure(this.Container);
        }

        /// <summary>
        /// Gets the list of sales orders
        /// </summary>
        /// <param name="lang">ISO code for language to be used</param>
        /// <param name="countryCode">Country code used for currency</param>
        /// <returns>Orders list</returns>
        [AuthOperationBehavior]
        public SalesOrders GetOrders(int pageNumber, int pageSize, string lang, string countryCode)
        {
            IOrdersBusinessProvider businessProvider = this.Container.Resolve<IOrdersBusinessProvider>();
            return businessProvider.GetOrders(pageNumber, pageSize, lang, countryCode);
        }

        /// <summary>
        /// Gets a sales order header
        /// </summary>
        /// <param name="lang">ISO code for language to be used</param>
        /// <param name="countryCode">Country code used for currency</param>
        /// <returns>SalesOrderHeader</returns>
        [AuthOperationBehavior]
        public SalesOrderHeader GetOrderBySalesOrderId(int salesOrderId, string lang, string countryCode)
        {
            IOrdersBusinessProvider businessProvider = this.Container.Resolve<IOrdersBusinessProvider>();
            return businessProvider.GetOrderBySalesOrderId(salesOrderId, lang, countryCode);
        }
    }
}
