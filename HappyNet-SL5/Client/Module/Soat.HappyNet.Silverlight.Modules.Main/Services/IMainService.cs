// -----------------------------------------------------------------------
// <copyright file="IMainService.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using Soat.HappyNet.Silverlight.Modules.Main.PersonServiceReference;

namespace Soat.HappyNet.Silverlight.Modules.Main.Services
{
    /// <summary>
    /// Interface defining the service of main module
    /// </summary>
    public interface IMainService
    {
        /// <summary>
        /// Authenticate the user
        /// </summary>
        /// <param name="user">User email</param>
        /// <param name="password">Password</param>
        /// <param name="LoginCompleted">Callback method</param>
        void BeginLogin(string user, string password, Soat.HappyNet.Silverlight.Library.ServiceCompleted<User> LoginCompleted);
    }
}
