// -----------------------------------------------------------------------
// <copyright file="ISalesOrderBusinessProvider.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soat.HappyNet.Server.Services.SalesOrder.DataAccessLayer;

namespace Soat.HappyNet.Server.Services.SalesOrder.BusinessLayer
{
    /// <summary>
    /// Class defining the business layer for sales orders
    /// </summary>
    public interface ISalesOrderBusinessProvider
    {
        /// <summary>
        /// Data Provider for sales orders
        /// </summary>
        ISalesOrderDataProvider SalesOrderDataProvider { get; }
    }
}
