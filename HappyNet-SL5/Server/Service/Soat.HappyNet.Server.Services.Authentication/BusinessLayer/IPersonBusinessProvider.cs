// -----------------------------------------------------------------------
// <copyright file="IPersonBusinessProvider.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using Soat.HappyNet.Server.DataContract.Common;
using Soat.HappyNet.Server.Services.Authentication.DataAccessLayer;
namespace Soat.HappyNet.Server.Services.Authentication.BusinessLayer
{
    public interface IPersonBusinessProvider
    {
        /// <summary>
        /// Data Provider for persons
        /// </summary>
        IPersonDataProvider AuthDataProvider { get; }

        /// <summary>
        /// Look for a registered user based on credentials
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <returns>Registered user, null otherwise</returns>
        User Login(string email, string password);
    }
}
