// -----------------------------------------------------------------------
// <copyright file="IMenuItemViewModel.cs" company="So@t">
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
    /// Interface defining the ViewModel for the MenuItem
    /// </summary>
    public interface IMenuItemViewModel
    {
        /// <summary>
        /// Navigation Uri
        /// </summary>
        string NavigationUri { get; set; }

        /// <summary>
        /// Label for the button
        /// </summary>
        string Name { get; set; }

         /// <summary>
        /// Method returning the name of the menu item
        /// </summary>
        Func<string> NameProvider { get; set; }

        /// <summary>
        /// View
        /// </summary>
        IMenuItemView View { get; }

        /// <summary>
        /// Command to navigate
        /// </summary>
        ICommand NavigateCommand { get; } 
    }
}
