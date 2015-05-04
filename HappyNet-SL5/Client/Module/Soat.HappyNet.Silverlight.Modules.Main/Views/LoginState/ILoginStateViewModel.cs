// -----------------------------------------------------------------------
// <copyright file="ILoginStateViewModel.cs" company="So@t">
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
    /// Cette interface définit le Vue Model (PresentationModel) pour le LoginState
    /// </summary>
    public interface ILoginStateViewModel
    {
        /// <summary>
        /// View
        /// </summary>
        ILoginStateView View { get; }

        /// <summary>
        /// Username of the current user
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// Password of the current user
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Logged in status
        /// </summary>
        bool IsLogged {get; set;}

        /// <summary>
        /// Command to disconnect the user
        /// </summary>
        ICommand LogoutCommand { get; }

        /// <summary>
        /// Command to login the user
        /// </summary>
        ICommand LoginCommand { get; }
    }
}
