// -----------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using Soat.HappyNet.Silverlight.Library;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Composite.Events;
using Soat.HappyNet.Silverlight.Infrastructure.Events;
using Soat.HappyNet.Silverlight.Library.Extensions;
using Soat.HappyNet.Silverlight.Library.Components.Mouse;
using System.Net.Browser;
using Soat.HappyNet.Silverlight.Views;

namespace Soat.HappyNet.Silverlight
{
    /// <summary>
    /// Represents the application AdventureWorks
    /// </summary>
    public partial class App : Application, INavigationApplication
    {
        /// <summary>
        /// Main frame (Navigation Framework) for the application
        /// </summary>
        public Frame MainFrame { get; set; }

        /// <summary>
        /// Unity container
        /// </summary>
        public IUnityContainer Container { get; private set; }

        /// <summary>
        /// Event aggregator
        /// </summary>
        public IEventAggregator EventAggregator { get; private set; }

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public App()
        {
            this.Startup += this.Application_Startup;
            this.UnhandledException += this.Application_UnhandledException;
            
            InitializeComponent();

            // Handles the scrolling throught mouse wheel
            ScrollableViews.Initialize();
        }

        #endregion

        #region Application_Startup Method

        /// <summary>
        /// Application boot
        /// </summary>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Activates the http client stack with support for FaultExceptions
            HttpWebRequest.RegisterPrefix("http://", WebRequestCreator.ClientHttp);
            HttpWebRequest.RegisterPrefix("https://", WebRequestCreator.ClientHttp);

            Bootstrapper bootstrapper = new Bootstrapper();
            bootstrapper.Run();

            this.Container = bootstrapper.Container;
            this.EventAggregator = Container.Resolve<IEventAggregator>();

            this.EventAggregator.Publish(new LoggingEvent("Application started", LoggingCategory.Message));
        }

        #endregion

        #region Application_UnhandledException Method

        /// <summary>
        /// Handles application exceptions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            //if (!System.Diagnostics.Debugger.IsAttached)
            //{

            // NOTE: This will allow the application to continue running after an exception has been thrown
            // but not handled. 
            // For production applications this error handling should be replaced with something that will 
            // report the error to the website and stop the application.

            e.Handled = true;

            string es = e.ExceptionObject.Message;
            string s = e.ExceptionObject.StackTrace;

            if (this.EventAggregator != null)
            {
                // Sends the error to the service handling logs
                this.EventAggregator.Publish(new LoggingEvent(String.Format("{0}\n{1}", es, s)));

                if (this.Container != null)
                {
                    // Displays the error message
                    var model = this.Container.Resolve<IErrorChildWindowViewModel>();
                    if (model != null)
                    {
                        model.ExceptionObject = e.ExceptionObject;
                        model.View.Show();
                    }
                }
            }
        }

        #endregion
    }
}