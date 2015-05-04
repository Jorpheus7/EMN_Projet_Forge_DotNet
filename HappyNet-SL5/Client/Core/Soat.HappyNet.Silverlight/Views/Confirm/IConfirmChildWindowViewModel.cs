// -----------------------------------------------------------------------
// <copyright file="IConfirmChildWindowViewModel.cs" company="So@t">
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

namespace Soat.HappyNet.Silverlight.Views
{
    /// <summary>
    /// Interface defining the ViewModel for the ConfirmChildWindow View
    /// </summary>
    public interface IConfirmChildWindowViewModel
    {
        /// <summary>
        /// View
        /// </summary>
        IConfirmChildWindowView View { get; }

        /// <summary>
        /// Command to cancel
        /// </summary>
        ICommand CancelCommand { get; }

        /// <summary>
        /// Command to validate
        /// </summary>
        ICommand ValidateCommand { get; }
        
        /// <summary>
		/// Text of the button executing the CancelCommand
		/// Exemples : "Cancel", "No".
        /// </summary>
        string CancelButtonText { get; set; }

        /// <summary>
        /// Text of the button executing the ValidateCommand
        /// Exemples : "OK", "Yes".
		/// <summary>
		string ValidateButtonText { get; set; }

		/// <summary>
		/// Title of the message
		/// </summary>
		string MessageTitle { get; set; }

        /// <summary>
        /// Message to display
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// Action to execute after the confirmation
        /// </summary>
        Action<bool?> ConfirmAction { get; set; }
    }
}
