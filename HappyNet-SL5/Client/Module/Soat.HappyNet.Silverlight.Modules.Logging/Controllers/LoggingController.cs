// -----------------------------------------------------------------------
// <copyright file="LoggingController.cs" company="So@t">
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
using Microsoft.Practices.Unity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Composite.Events;
using Soat.HappyNet.Silverlight.Modules.Logging.Services;
using Soat.HappyNet.Silverlight.Library.Extensions;
using Soat.HappyNet.Silverlight.Infrastructure.Events;

namespace Soat.HappyNet.Silverlight.Modules.Logging.Controllers
{
    /// <summary>
    /// Class defining a controller for logging purpose
    /// </summary>
    public class LoggingController : ILoggingController
    {
        #region Private members

        /// <summary>
        /// Event aggregator
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        /// <summary>
        /// Unity container
        /// </summary>
        private readonly IUnityContainer container;

        #endregion

        #region LoggingService Property

        /// <summary>
        /// Service de gestion des logs
        /// </summary>
        public ILoggingService LoggingService { get; private set; } 

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="regionManager">Region manager</param>
        /// <param name="container">Container Unity</param>
        /// <param name="eventAggregator">Event aggregator</param>
        public LoggingController(ILoggingService loggingService,
            IUnityContainer container, 
            IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.container = container;
            this.LoggingService = loggingService;
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Executes the controller
        /// </summary>
        public void Run()
        {
            this.eventAggregator.Subscribe<LoggingEvent>(OnError);
        }

        #endregion

        #region OnError Method

        /// <summary>
        /// Sends error messages to the log service
        /// </summary>
        /// <param name="args">Argument</param>
        public void OnError(LoggingEvent args)
        {
            if (args.Category == LoggingCategory.Error)
            {
                this.LoggingService.BeginWriteErrorMessage(args.Message);
            }
            else if (args.Category == LoggingCategory.Message)
            {
                this.LoggingService.BeginWriteMessage(args.Message);
            }
        } 

        #endregion
    }
}
