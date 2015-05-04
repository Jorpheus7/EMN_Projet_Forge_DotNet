// -----------------------------------------------------------------------
// <copyright file="ISalesOrderService.cs" company="So@t">
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
using Soat.HappyNet.Server.Entities;

namespace Soat.HappyNet.Server.Services.SalesOrder
{
    
    /// <summary>
    /// Interface defining the contract of services for sales orders
    /// </summary>
    [ServiceContract]
    public interface ISalesOrderService
    {
        /// <summary>
        /// Gets the current shopping cart
        /// </summary>
        /// <returns>Shopping cart</returns>
        [OperationContract]
        IEnumerable<ShoppingCartItem> GetShoppingCart();
    }
}
