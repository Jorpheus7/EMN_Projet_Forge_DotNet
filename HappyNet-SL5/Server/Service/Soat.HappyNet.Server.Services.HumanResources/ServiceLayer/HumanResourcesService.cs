// -----------------------------------------------------------------------
// <copyright file="HumanResourcesService.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using Soat.HappyNet.Server.Services.HumanResources.BusinessLayer;
using Soat.HappyNet.Server.DataContract;
using System.ServiceModel.Activation;
using Soat.HappyNet.Server.Services.Authentication.Authentication;


namespace Soat.HappyNet.Server.Services.HumanResources
{

    /// <summary>
    /// Class defining the services for human resources
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class HumanResourcesService : IHumanResourcesService
    {
        #region Const

        /// <summary>
        /// Section name for unity configuration
        /// </summary>
        const string UNITY_SECTION_NAME = "unity";

        /// <summary>
        /// Container name for Unity configuration
        /// </summary>
        const string UNITY_CONTAINER_NAME = "humanResourcesContainer";

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
        public HumanResourcesService()
        {
            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection(HumanResourcesService.UNITY_SECTION_NAME);
            section.Containers[HumanResourcesService.UNITY_CONTAINER_NAME].Configure(this.Container);
        }

        /// <summary>
        /// Retrieves all the employees
        /// </summary>
        /// <param name="pageNumber">Page number</param>
        /// <param name="pageSize">Employee number per page</param>
        /// <param name="sortColumnName">Sorting column</param>
        /// <param name="sortDirection">Sort direction</param>
        [AuthOperationBehavior]
        public IEnumerable<Employee> FindAllEmployees(int pageNumber, int pageSize, string sortColumnName, SortDirection sortDirection)
        {
            IHumanResourcesBusinessProvider businessProvider = this.Container.Resolve<IHumanResourcesBusinessProvider>();
            return businessProvider.FindAllEmployees(pageNumber, pageSize,sortColumnName, SortDirection.Ascending);
        }
        
    }
}
