// -----------------------------------------------------------------------
// <copyright file="SalesOrderBusinessProvider.cs" company="So@t">
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
    public class SalesOrderBusinessProvider : ISalesOrderBusinessProvider
    {
        #region SalesDataProvider Property

        /// <summary>
        /// Data Provider for sales orders
        /// </summary>
        public ISalesOrderDataProvider SalesOrderDataProvider { get; private set; } 

        #endregion
        
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="personDataProvider">Data provider for human resources</param>
        public SalesOrderBusinessProvider(ISalesOrderDataProvider salesOrderDataProvider)
        {
            this.SalesOrderDataProvider = salesOrderDataProvider;
        } 

        #endregion

        
    }
}
