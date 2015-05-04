// -----------------------------------------------------------------------
// <copyright file="INotificationsViewModel.cs" company="So@t">
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
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace Soat.HappyNet.Silverlight.Modules.Main.Views
{
    /// <summary>
    /// Interface defining the ViewModel for the Notifications View
    /// </summary>
    public interface INotificationsViewModel
    {
        /// <summary>
        /// View
        /// </summary>
        INotificationsView View { get; }
    }
}
