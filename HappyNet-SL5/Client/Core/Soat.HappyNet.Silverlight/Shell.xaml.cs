// -----------------------------------------------------------------------
// <copyright file="Shell.xaml.cs" company="So@t">
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
using Microsoft.Practices.Unity;
using Microsoft.Practices.Composite.Events;
using Soat.HappyNet.Silverlight.Infrastructure.Events;
using Soat.HappyNet.Silverlight.Library.Extensions;
using System.Windows.Navigation;
using Soat.HappyNet.Silverlight.Library.Controls;
using System.Collections.ObjectModel;
using Microsoft.Practices.Composite.Regions;
using Soat.HappyNet.Silverlight.Views;
using Soat.HappyNet.Silverlight.Infrastructure;

namespace Soat.HappyNet.Silverlight
{
    /// <summary>
    /// Shell ( = RootVisual) of the application
    /// </summary>
    public partial class Shell : UserControl
    {
        public NavigationService NavigationService { get; private set; }

        #region Private Members

        /// <summary>
        /// Unity container to instanciate objects
        /// </summary>
        private readonly IUnityContainer container;

        /// <summary>
        /// Event aggregator to communicate throughout the application
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        /// <summary>
        /// Region Manager to manage regions in the application
        /// </summary>
        private readonly IRegionManager regionManager;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="container">Container Unity</param>
        /// <param name="eventAggregator">Event aggregator</param>
        public Shell(IUnityContainer container, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            InitializeComponent();

            this.container = container;
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;

            this.Initialize();
        }

        #endregion

        #region Initialize Method

        /// <summary>
        /// Initializes the shell
        /// </summary>
        public void Initialize()
        {
            // Navigation
            this.MainFrame.Navigated += new NavigatedEventHandler(OnNavigated);
            this.MainFrame.Navigating += new NavigatingCancelEventHandler(OnNavigating);

            this.eventAggregator.Subscribe<NavigateToEvent>(OnNavigateToChanged);

            // Child windows display triggered by the event aggregator
            this.eventAggregator.Subscribe<MessageShowEvent>(OnShowMessage);
            this.eventAggregator.Subscribe<ConfirmShowEvent>(OnConfirmMessage);
            this.eventAggregator.Subscribe<ErrorEvent>(OnErrorMessage);
        }

        #endregion

        #region Navigation Methods

        /// <summary>
        /// Navigation completed
        /// </summary>
        void OnNavigated(object sender, NavigationEventArgs e)
        {
            this.eventAggregator.Publish(new NavigatedEvent(e.Uri));
            Globals.CurrentUri = e.Uri;
        }

        /// <summary>
        /// Navigating ...
        /// </summary>
        void OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            // Global var specifying if we can navigate or not
            if (!Globals.IsNavigationAllowed)
            {
                e.Cancel = true;
                Globals.IsNavigationAllowed = true;
                return;
            }

            ClearRegionManager();
        }

        /// <summary>
        /// Handles the navigation throught the event aggregator
        /// </summary>
        /// <param name="args">Argument containing the Uri to navigate to</param>
        public void OnNavigateToChanged(NavigateToEvent args)
        {
            if (args != null)
            {
                this.MainFrame.Navigate(new Uri(args.RedirectUri, UriKind.RelativeOrAbsolute));
            }
        } 

        #endregion

        #region RegionManager

        /// <summary>
        /// Deletes all the regions from the RegionManager, not to conflict
        /// with the NavigationFramework of Silverlight 3 (/!\ memory leaks)
        /// </summary>
        private void ClearRegionManager()
        {
            var strCollection = new Collection<string>();
            foreach (var region in this.regionManager.Regions)
            {
                // Cleans ViewModels
                foreach (var view in region.Views)
                {
                    if (view is FrameworkElement)
                    {
                        FrameworkElement element = view as FrameworkElement;
                        if (element.DataContext is IDisposable) // DataContext = ViewModel
                        {
                            ((IDisposable)element.DataContext).Dispose();
                        }
                    }
                }
                strCollection.Add(region.Name);
            }
            foreach (var item in strCollection)
            {
                this.regionManager.Regions.Remove(item);
            }
        }

        #endregion

        #region ChildWindows

        /// <summary>
        /// Displays a message within a window
        /// </summary>
        /// <param name="args">Argument containing the info to display</param>
        public void OnShowMessage(MessageShowEvent args)
        {
            var childWindow = this.container.Resolve<IMessageChildWindowViewModel>();
            childWindow.Message = args.Message;
            childWindow.MessageTitle = args.MessageTitle;

            childWindow.View.Show();
        }

        /// <summary>
        /// Displays a message within a confirmation window
        /// </summary>
        /// <param name="args">Argument containing the info to display</param>
        public void OnConfirmMessage(ConfirmShowEvent args)
        {
            var childWindow = this.container.Resolve<IConfirmChildWindowViewModel>();
            childWindow.Message = args.Message;
            childWindow.MessageTitle = args.MessageTitle;
            childWindow.CancelButtonText = args.CancelButtonText;
            childWindow.ValidateButtonText = args.ValidateButtonText;
            childWindow.ConfirmAction = args.ConfirmAction;

            childWindow.View.Show();
        }

        /// <summary>
        /// Displays an error message within a window
        /// </summary>
        /// <param name="args">Argument containing the info to display</param>
        public void OnErrorMessage(ErrorEvent args)
        {
            var childWindow = this.container.Resolve<IErrorChildWindowViewModel>();
            childWindow.ExceptionObject = args.ExceptionObj;
            childWindow.ErrorMessage = args.Message;

            childWindow.View.Show();
        }

        #endregion
    }
}
