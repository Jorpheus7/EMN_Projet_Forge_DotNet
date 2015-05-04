// -----------------------------------------------------------------------
// <copyright file="NotificationsView.xaml.cs" company="So@t">
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
using Soat.HappyNet.Silverlight.Infrastructure.Events;
using Soat.HappyNet.Silverlight.Library.Controls;

namespace Soat.HappyNet.Silverlight.Modules.Main.Views
{
    /// <summary>
    /// Class defining the Notifications View
    /// </summary>
    public partial class NotificationsView : UserControl, INotificationsView
    {
        #region Model property

        /// <summary>
        /// ViewModel attached to the View
        /// </summary>
        public INotificationsViewModel Model
        {
            get { return this.DataContext as INotificationsViewModel; }
            set { this.DataContext = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public NotificationsView()
        {
            InitializeComponent();
        }

        #endregion

        #region Notification Methods

        /// <summary>
        /// Show a notification
        /// </summary>
        /// <param name="args">Notification argument</param>
        public void ShowNotification(NotificationEvent args)
        {
            Message msg = new Message();
            msg.Text = args.Message;
            if (args.Type == NotificationType.Error)
            {
                msg.BackgroundColor = Application.Current.Resources["ErrorBackgroundBrush"] as Brush;
            }
            else if (args.Type == NotificationType.Validation)
            {
                msg.BackgroundColor = Application.Current.Resources["ValidBackgroundBrush"] as Brush;
            }

            NotificationsContainer.Children.Insert(0, msg);
            msg.Show();
        }

        #endregion
    }
}
