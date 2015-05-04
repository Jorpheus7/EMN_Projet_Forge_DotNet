// -----------------------------------------------------------------------
// <copyright file="SalesOrderDetail.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Soat.HappyNet.Server.DataContract
{
    /// <summary>
    /// Order detail
    /// </summary>
    [DataContract]
    public class SalesOrderDetail
    {
        /// <summary>
        /// Id for the order
        /// </summary>
        [DataMember]
        public int Identifier { get; private set; }

        /// <summary>
        /// Id for the order's owner
        /// </summary>
        [DataMember]
        public int ParentOrderIdentifier { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public SalesOrderDetail()
        {
            this.Identifier = -1;
            this.ParentOrderIdentifier = -1;
        }
    }
}
