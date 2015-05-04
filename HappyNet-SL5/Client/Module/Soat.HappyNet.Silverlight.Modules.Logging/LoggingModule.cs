// -----------------------------------------------------------------------
// <copyright file="LoggingModule.cs" company="So@t">
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
using Soat.HappyNet.Silverlight.Modules.Logging.Controllers;
using Soat.HappyNet.Silverlight.Modules.Logging.Services;

namespace Soat.HappyNet.Silverlight.Modules.Logging
{
    /// <summary>
    /// Class representing the logging module
    /// </summary>
    public class LoggingModule : IModule
    {
        #region Private members

        /// <summary>
        /// Unity container
        /// </summary>
        private readonly IUnityContainer container;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="container">Unity container</param>
        public LoggingModule(IUnityContainer container)
        {
            this.container = container;
        }

        #endregion

        #region Initialisation

        /// <summary>
        /// Initializes the module
        /// </summary>
        public void Initialize()
        {
            this.RegisterTypesAndServices();

            this.InitializeController();
        }

        /// <summary>
        /// Initializes the controller
        /// </summary>
        private void InitializeController()
        {
            ILoggingController controller = container.Resolve<ILoggingController>();
            this.container.RegisterInstance<ILoggingController>(controller);

            controller.Run();
        }

        /// <summary>
        /// Registers types and services
        /// </summary>
        private void RegisterTypesAndServices()
        {
            this.container.RegisterType<ILoggingController, LoggingController>(new ContainerControlledLifetimeManager());
            this.container.RegisterType<ILoggingService, LoggingService>(new ContainerControlledLifetimeManager());
        }

        #endregion
    }
}
