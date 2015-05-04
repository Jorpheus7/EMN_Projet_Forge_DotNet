// -----------------------------------------------------------------------
// <copyright file="SalesOrderService.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using Soat.HappyNet.Server.Services.SalesOrder.BusinessLayer;
using System.ServiceModel.Activation;
using Soat.HappyNet.Server.Entities;

namespace Soat.HappyNet.Server.Services.SalesOrder
{

    /// <summary>
    /// Class defining the service for sales orders
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SalesOrderService : ISalesOrderService
    {
        #region Const

        /// <summary>
        /// Section name for unity configuration
        /// </summary>
        const string UNITY_SECTION_NAME = "unity";

        /// <summary>
        /// Container name for Unity configuration
        /// </summary>
        const string UNITY_CONTAINER_NAME = "salesOrderServiceContainer";

        #endregion

        #region Container Property

        /// <summary>
        /// Unity container
        /// </summary>
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

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SalesOrderService()
        {
            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection(SalesOrderService.UNITY_SECTION_NAME);
            section.Containers[SalesOrderService.UNITY_CONTAINER_NAME].Configure(this.Container);
        }

        #endregion

        /// <summary>
        /// Gets the current shopping cart
        /// </summary>
        /// <returns>Shopping cart</returns>
        public IEnumerable<ShoppingCartItem> GetShoppingCart()
        {
            return new List<ShoppingCartItem>();
        }
    }
}
