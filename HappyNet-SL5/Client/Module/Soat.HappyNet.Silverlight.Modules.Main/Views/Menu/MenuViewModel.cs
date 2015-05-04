// -----------------------------------------------------------------------
// <copyright file="MenuViewModel.cs" company="So@t">
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
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Presentation.Commands;
using Soat.HappyNet.Silverlight.Infrastructure.Events;
using Soat.HappyNet.Silverlight.Infrastructure.UriNames;
using Soat.HappyNet.Silverlight.Library.Extensions;
using Soat.HappyNet.Silverlight.Infrastructure;
using Soat.HappyNet.Silverlight.Library;
using System.Windows.Data;
using Soat.HappyNet.Silverlight.Infrastructure.Localization;
using Soat.HappyNet.Silverlight.Library.Commands;
using System.Collections.ObjectModel;
using Microsoft.Practices.Unity;
using Soat.HappyNet.Silverlight.Modules.Main.Controllers;

namespace Soat.HappyNet.Silverlight.Modules.Main.Views
{
    /// <summary>
    /// Class defining the ViewModel for the MenuItem
    /// </summary>
    public class MenuViewModel : ViewModel<IMenuView>, IMenuViewModel
    {
        #region Propriétés

        public ObservableCollection<IMenuItemViewModel> MenuItems { get; private set; }

        #endregion

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

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="view">View attached to this ViewModel</param>
        /// <param name="eventAggregator">Event aggregator</param>
        public MenuViewModel(IMenuView view, IUnityContainer container, IEventAggregator eventAggregator)
        {
            this.container = container;
            this.eventAggregator = eventAggregator;

            MenuItems = new ObservableCollection<IMenuItemViewModel>();

            this.View = view;
            this.View.Model = this;

            this.eventAggregator.Subscribe<AddMenuButtonEvent>(AddMenuButton);
            this.eventAggregator.Subscribe<RemoveMenuButtonEvent>(RemoveMenuButton);

            InitializeButtons();
        }

        /// <summary>
        /// Initialize buttons from the controller
        /// </summary>
        void InitializeButtons()
        {
            var controller = container.Resolve<IMainController>();
            foreach (var arg in controller.MenuItems)
            {
                AddMenuButton(arg);
            }
        }

        #endregion

        public void AddMenuButton(AddMenuButtonEvent args)
        {
            RemoveMenuButton(new RemoveMenuButtonEvent(args.Url));

            var menuItem = container.Resolve<IMenuItemViewModel>();
            menuItem.NameProvider = args.NameProvider;
            menuItem.NavigationUri = args.Url;

            MenuItems.Add(menuItem);
        }

        public void RemoveMenuButton(RemoveMenuButtonEvent args)
        {
            var existingMenuItem = MenuItems.Where(m => m.NavigationUri == args.Url).FirstOrDefault();
            if (existingMenuItem != null)
                MenuItems.Remove(existingMenuItem);
        }
    }
}
