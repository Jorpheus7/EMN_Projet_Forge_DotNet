// -----------------------------------------------------------------------
// <copyright file="SalesOrder.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Soat.HappyNet.Server.DataContract
{
    /// <summary>
    /// Represents an order
    /// </summary>
    [DataContract]
    public class SalesOrder
    {
        /// <summary>
        /// Order id
        /// </summary>
        [DataMember]
        public int Identifier { get; set; }

        /// <summary>
        /// Order details
        /// </summary>
        [DataMember]
        public IEnumerable<SalesOrderDetail> SalesOrderDetailCollection { get; private set; }

        /// <summary>
        /// Default order
        /// </summary>
        public SalesOrder()
        {
            this.Identifier = -1;
            this.SalesOrderDetailCollection = new Collection<SalesOrderDetail>();
        }
    }
}
