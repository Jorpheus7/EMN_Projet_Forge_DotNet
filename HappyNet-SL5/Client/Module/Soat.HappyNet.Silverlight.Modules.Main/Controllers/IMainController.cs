// -----------------------------------------------------------------------
// <copyright file="IMainController.cs" company="So@t">
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
using System.Collections.ObjectModel;
using Soat.HappyNet.Silverlight.Infrastructure.Events;

namespace Soat.HappyNet.Silverlight.Modules.Main.Controllers
{
    /// <summary>
    /// Interface defining a controller for production
    /// </summary>
    public interface IMainController
    {
        /// <summary>
        /// Executes the controller
        /// </summary>
        void Run();

        ObservableCollection<AddMenuButtonEvent> MenuItems { get; }
    }
}
