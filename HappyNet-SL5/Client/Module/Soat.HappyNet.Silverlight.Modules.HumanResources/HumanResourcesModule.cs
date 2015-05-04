// -----------------------------------------------------------------------
// <copyright file="HumanResourcesModule.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Composite.Regions;
using Soat.HappyNet.Silverlight.Modules.HumanResources.Services;
using Soat.HappyNet.Silverlight.Modules.HumanResources.Views;
using Soat.HappyNet.Silverlight.Infrastructure.Localization;
using Soat.HappyNet.Silverlight.Infrastructure.UriNames;
using Soat.HappyNet.Silverlight.Infrastructure.RegionNames;

namespace Soat.HappyNet.Silverlight.Modules.HumanResources
{
    /// <summary>
    /// Class representing the human resources module
    /// </summary>
    public class HumanResourcesModule : IModule
    {
        #region Private Members

        /// <summary>
        /// Unity container
        /// </summary>
        private readonly IUnityContainer container;

        /// <summary>
        /// Region manager
        /// </summary>
        private readonly IRegionManager regionManager;

        #endregion
                
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="container">Unity container</param>
        /// <param name="regionManager">Region manager</param>
        public HumanResourcesModule(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Initiliazes the module
        /// </summary>
        public void Initialize()
        {
            this.RegisterTypesAndServices();

            this.RegisterRegions();
        }

        /// <summary>
        /// Registers types and services
        /// </summary>
        private void RegisterTypesAndServices()
        {
            // Services
            this.container.RegisterType<IHumanResourcesService, HumanResourcesService>(new ContainerControlledLifetimeManager());

            // Register Views / ViewModels
            this.container.RegisterType<IEmployeesSearchView, EmployeesSearchView>();
            this.container.RegisterType<IEmployeesSearchViewModel, EmployeesSearchViewModel>();
        }

        /// <summary>
        /// Register regions
        /// </summary>
        void RegisterRegions()
        {
            // Regions
            this.regionManager.RegisterViewWithRegion(RegionNames.EmployeesSearchRegion, () => this.container.Resolve<IEmployeesSearchViewModel>().View);
        }

        #endregion
    }
}
