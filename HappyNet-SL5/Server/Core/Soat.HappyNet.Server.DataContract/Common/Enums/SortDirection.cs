// -----------------------------------------------------------------------
// <copyright file="SortDirection.cs" company="So@t">
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
    /// Cette énumération répertorie les ordres de tri possible
    /// </summary>
    [DataContract]
    public enum SortDirection
    {
        /// <summary>
        /// Ordre de tri ascendant
        /// </summary>
        [EnumMember]
        Ascending,

        /// <summary>
        /// Ordre de tri décendant
        /// </summary>
        [EnumMember]
        Descending
    }
}
