// -----------------------------------------------------------------------
// <copyright file="IPersonDataProvider.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using Soat.HappyNet.Server.DataContract.Common;
namespace Soat.HappyNet.Server.Services.Authentication.DataAccessLayer
{
    /// <summary>
    /// Interface defining the data access for persons
    /// </summary>
    public interface IPersonDataProvider
    {
        /// <summary>
        /// Look for a registered user based on credentials
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <returns>Registered user, null otherwise</returns>
        User Login(string email, string password);
    }
}
