// -----------------------------------------------------------------------
// <copyright file="MenuItemViewModel.cs" company="So@t">
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

namespace Soat.HappyNet.Silverlight.Modules.Main.Views
{
    /// <summary>
    /// Class defining the ViewModel for the MenuItem
    /// </summary>
    public class MenuItemViewModel : ViewModel<IMenuItemView>, IMenuItemViewModel
    {
        #region Properties

        private string navigationUri;
        /// <summary>
        /// Navigation Uri
        /// </summary>
        public string NavigationUri
        {
            get { return navigationUri; }
            set
            {
                if (navigationUri != value)
                {
                    navigationUri = value;
                    Notify(() => this.NavigationUri);
                }
            }
        }

        private string name;
        /// <summary>
        /// Label for the button
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    Notify(() => this.Name);
                }
            }
        }
	
        private bool isCurrentMenuItem;
        /// <summary>
        /// Wether or not this menu item is currently selected
        /// </summary>
        public bool IsCurrentMenuItem
        {
            get { return isCurrentMenuItem; }
            set
            {
                if (isCurrentMenuItem != value)
                {
                    isCurrentMenuItem = value;
                    Notify(() => this.IsCurrentMenuItem);
                }
            }
        }

        Func<string> nameProvider;
        /// <summary>
        /// Method returning the name of the menu item
        /// </summary>
        public Func<string> NameProvider
        {
            get { return nameProvider; }
            set
            {
                nameProvider = value; this.Name = nameProvider();
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Command to navigate
        /// </summary>
        public ICommand NavigateCommand { get; private set; }

        #endregion

        #region Private members

        /// <summary>
        /// Event aggregator
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="view">View attached to this ViewModel</param>
        /// <param name="eventAggregator">Event aggregator</param>
        public MenuItemViewModel(IMenuItemView view, IEventAggregator eventAggregator)
        {
            this.InitializeCommands();

            this.eventAggregator = eventAggregator;

            this.View = view;
            this.View.Model = this;

            eventAggregator.Subscribe<NavigatedEvent>(OnNavigated);
            eventAggregator.Subscribe<LocalizationChangedEvent>(OnLocalizationChanged);
        }

        #endregion

        #region Localization
        
        /// <summary>
        /// Handles when localization has changed
        /// </summary>
        /// <param name="args"></param>
        public void OnLocalizationChanged(LocalizationChangedEvent args)
        {
            if (nameProvider != null)
            {
                this.Name = nameProvider();
            }
        }

        #endregion

        #region Initialisation Method

        /// <summary>
        /// Initializes the commands
        /// </summary>
        void InitializeCommands()
        {
            this.NavigateCommand = new DelegateCommand(OnNavigateToChanged);
        }

        #endregion

        #region Navigation

        /// <summary>
        /// Handles the redirection to the requested page
        /// </summary>
        /// <param name="args">null</param>
        public void OnNavigateToChanged()
        {
            IsCurrentMenuItem = true;
            
            this.eventAggregator.Publish(new NavigateToEvent() { RedirectUri = NavigationUri });
        }

        /// <summary>
        /// Handles whenever we have navigated to a page, to select the right menu item
        /// </summary>
        /// <param name="args"></param>
        public void OnNavigated(NavigatedEvent args)
        {
            string uri = args.Uri.ToString();
            int position = uri.IndexOf('/', 0, 1);
            if (position > 0)
            {
                uri = uri.Substring(0, position);
            }

            IsCurrentMenuItem = uri == NavigationUri;
        }

        #endregion
    }
}
