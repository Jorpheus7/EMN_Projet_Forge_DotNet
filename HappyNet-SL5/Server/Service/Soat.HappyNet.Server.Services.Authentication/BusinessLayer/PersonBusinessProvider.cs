// -----------------------------------------------------------------------
// <copyright file="PersonBusinessProvider.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soat.HappyNet.Server.Services.Authentication.DataAccessLayer;
using Soat.HappyNet.Server.DataContract.Common;
using System.Web.Security;
using System.Security.Principal;
using System.Web;
using System.Threading;

namespace Soat.HappyNet.Server.Services.Authentication.BusinessLayer
{
    public class PersonBusinessProvider : IPersonBusinessProvider
    {
        #region Membres
        
        /// <summary>
        /// Data Provider for persons
        /// </summary>
        public IPersonDataProvider AuthDataProvider { get; private set; }

        #endregion

        #region Constructeur

        /// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="personDataProvider">Data provider for persons</param>
        public PersonBusinessProvider(IPersonDataProvider authDataProvider)
		{
            this.AuthDataProvider = authDataProvider;
		}

        #endregion

        /// <summary>
        /// Look for a registered user based on credentials
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <returns>Registered user, null otherwise</returns>
        public User Login(string email, string password)
        {
            return AuthDataProvider.Login(email, password);
        }
    }
}
