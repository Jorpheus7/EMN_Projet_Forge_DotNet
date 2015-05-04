// -----------------------------------------------------------------------
// <copyright file="PersonService.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using Microsoft.Practices.Unity;
using Soat.HappyNet.Server.Services.Authentication.BusinessLayer;
using System.ServiceModel.Channels;
using System.ServiceModel;
using Soat.HappyNet.Server.DataContract.Common;
using System.Security.Principal;
using System.Web.Security;
using System.Web;
using System.Threading;
using System.ServiceModel.Activation;

namespace Soat.HappyNet.Server.Services.Authentication
{
    /// <summary>
    /// Class defining the service for persons
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PersonService : IPersonService
    {
        #region Const

        /// <summary>
        /// Section name for unity configuration
        /// </summary>
        const string UNITY_SECTION_NAME = "unity";

        /// <summary>
        /// Container name for Unity configuration
        /// </summary>
        const string UNITY_CONTAINER_NAME = "authenticationServiceContainer";

        #endregion

        #region Container Property

        private IUnityContainer container;
        /// <summary>
        /// Unity container
        /// </summary>
        public IUnityContainer Container
        {
            get
            {
                if (this.container == null)
                {
                    this.container = new UnityContainer();
                }
                return this.container;
            }
        }

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public PersonService()
        {
            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection(PersonService.UNITY_SECTION_NAME);
            section.Containers[PersonService.UNITY_CONTAINER_NAME].Configure(this.Container);
        }

        #region Service methods

        /// <summary>
        /// Look for a registered user based on credentials
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <returns>Registered user, null otherwise</returns>
        public User Login(string email, string password)
        {
            IPersonBusinessProvider businessProvider = this.Container.Resolve<IPersonBusinessProvider>();
            var user = businessProvider.Login(email, password);

            return user;
        }

        #endregion
    }
}
