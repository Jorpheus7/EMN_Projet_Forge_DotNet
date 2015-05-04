// -----------------------------------------------------------------------
// <copyright file="NotificationsViewModel.cs" company="So@t">
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
using Microsoft.Practices.Unity;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Presentation.Commands;
using Soat.HappyNet.Silverlight.Library.Extensions;
using Soat.HappyNet.Silverlight.Infrastructure.Events;
using Soat.HappyNet.Silverlight.Library;

namespace Soat.HappyNet.Silverlight.Modules.Main.Views
{
    /// <summary>
    /// Class defining the ViewModel for the Notifications View
    /// </summary>
    public class NotificationsViewModel : ViewModel<INotificationsView>, INotificationsViewModel
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

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="view">View attached to this ViewModel</param>
        /// <param name="eventAggregator">Event aggregator</param>
        /// <param name="container">Unity container</param>
        public NotificationsViewModel(INotificationsView view,
              IEventAggregator eventAggregator,
              IUnityContainer container)
        {
            this.container = container;
            this.eventAggregator = eventAggregator;

            this.View = view;
            this.View.Model = this;

            eventAggregator.Subscribe<NotificationEvent>(OnNotification);
        }

        #endregion

        /// <summary>
        /// Handles requests for notification
        /// </summary>
        /// <param name="args"></param>
        public void OnNotification(NotificationEvent args)
        {
            // Let the view handle it
            View.ShowNotification(args);
        }
    }
}
