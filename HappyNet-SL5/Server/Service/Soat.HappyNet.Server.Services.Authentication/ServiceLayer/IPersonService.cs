// -----------------------------------------------------------------------
// <copyright file="IPersonService.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using Soat.HappyNet.Server.DataContract.Common;
using System.ServiceModel;

namespace Soat.HappyNet.Server.Services.Authentication
{
    /// <summary>
    /// Interface defining the contract of services for persons
    /// </summary>
    [ServiceContract]
    public interface IPersonService
    {
        /// <summary>
        /// Look for a registered user based on credentials
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <returns>Registered user, null otherwise</returns>
        [OperationContract]
        User Login(string email, string password);
    }
}
