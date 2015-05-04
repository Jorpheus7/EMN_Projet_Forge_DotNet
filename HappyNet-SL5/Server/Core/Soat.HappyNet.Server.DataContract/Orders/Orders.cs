// -----------------------------------------------------------------------
// <copyright file="Orders.cs" company="So@t">
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

namespace Soat.HappyNet.Server.DataContract.Orders
{
    [DataContract]
    public class Orders
    {
        [DataMember]
        public IEnumerable<Soat.HappyNet.Server.Entities.SalesOrderHeader> Result;

        [DataMember]
        public int TotalCount;

        [DataMember]
        public int PageNumber;
    }
}
