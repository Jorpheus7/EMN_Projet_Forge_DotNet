// -----------------------------------------------------------------------
// <copyright file="MainController.cs" company="So@t">
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
using Soat.HappyNet.Silverlight.Infrastructure.RegionNames;
using Soat.HappyNet.Silverlight.Infrastructure.Localization;
using Soat.HappyNet.Silverlight.Infrastructure.UriNames;
using Soat.HappyNet.Silverlight.Modules.Main.Views;
using Soat.HappyNet.Silverlight.Library.Extensions;
using Soat.HappyNet.Silverlight.Infrastructure.Events;
using System.Collections.ObjectModel;
using System.Linq;

namespace Soat.HappyNet.Silverlight.Modules.Main.Controllers
{
    /// <summary>
    /// Class defining a controller for main module
    /// </summary>
    public class MainController : IMainController
    {
        #region Properties

        public ObservableCollection<AddMenuButtonEvent> MenuItems { get; private set; }

        #endregion

        #region Private Members

        /// <summary>
        /// Region manager
        /// </summary>
        private readonly IRegionManager regionManager;
        /// <summary>
        /// Unity container
        /// </summary>
        private readonly IUnityContainer container;
        /// <summary>
        /// Event aggregator
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="regionManager">Region manager</param>
        /// <param name="container">Container Unity</param>
        /// <param name="eventAggregator">Event aggregator</param>
        public MainController(IRegionManager regionManager,
            IUnityContainer container,
            IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;
            this.container = container;

            MenuItems = new ObservableCollection<AddMenuButtonEvent>();

            // Catching events requests before the MenuView is instanciated and
            // storing it in MenuItems so it can be retrieved by the MenuViewModel when instanciated
            this.eventAggregator.Subscribe<AddMenuButtonEvent>(AddMenuButton);
            this.eventAggregator.Subscribe<RemoveMenuButtonEvent>(RemoveMenuButton);
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Executes the controller
        /// </summary>
        public void Run()
        {
            // Regions
            this.regionManager.RegisterViewWithRegion(RegionNames.LoginStateRegion, () => this.container.Resolve<ILoginStateViewModel>().View);

            this.regionManager.RegisterViewWithRegion(RegionNames.LanguageSwitchRegion, () => this.container.Resolve<ILanguageSwitchViewModel>().View);

            this.regionManager.RegisterViewWithRegion(RegionNames.NotificationsRegion, () => this.container.Resolve<INotificationsViewModel>().View);
        }

        #endregion

        #region Menu

        public void AddMenuButton(AddMenuButtonEvent args)
        {
            RemoveMenuButton(new RemoveMenuButtonEvent(args.Url));
            MenuItems.Add(args);
        }

        public void RemoveMenuButton(RemoveMenuButtonEvent args)
        {
            var existingMenuItem = MenuItems.Where(m => m.Url == args.Url).FirstOrDefault();
            if (existingMenuItem != null)
                MenuItems.Remove(existingMenuItem);
        }

        #endregion
    }
}
