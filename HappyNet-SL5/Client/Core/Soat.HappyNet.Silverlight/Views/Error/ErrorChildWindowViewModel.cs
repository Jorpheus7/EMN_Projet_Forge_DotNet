// -----------------------------------------------------------------------
// <copyright file="ErrorChildWindowViewModel.cs" company="So@t">
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
using Soat.HappyNet.Silverlight.Library.Extensions;
using Soat.HappyNet.Silverlight.Library;
using Soat.HappyNet.Silverlight.Library.Commands;

namespace Soat.HappyNet.Silverlight.Views
{
    /// <summary>
    /// ViewModel to display an error message through a child window
    /// </summary>
    public class ErrorChildWindowViewModel : ViewModel<IErrorChildWindowView>, IErrorChildWindowViewModel
    {
        #region Private Members

        /// <summary>
        /// Unity container
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        #endregion

        #region Properties

        /// <summary>
        /// Command to close
        /// </summary>
        public ICommand CloseCommand { get; private set; }

        private Exception exceptionObject;
        // <summary>
        /// Exception object to display
        /// </summary>
        public Exception ExceptionObject
        {
            get
            {
                return exceptionObject;
            }
            set
            {
                if (this.exceptionObject != value)
                {
                    this.exceptionObject = value;
                    Notify(() => this.ExceptionObject);
                }
            }
        }

        private string errorMessage;
        /// <summary>
        /// Error message
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                if (string.IsNullOrEmpty(errorMessage))
                    return this.ExceptionObject.Message + Environment.NewLine + Environment.NewLine + this.ExceptionObject.StackTrace;
                else
                    return errorMessage;
            }
            set
            {
                if (this.errorMessage != value)
                {
                    this.errorMessage = value;
                    Notify(() => this.ErrorMessage);
                }
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="view">View to use</param>
        /// <param name="eventAggregator">Event aggregator</param>
        public ErrorChildWindowViewModel(IErrorChildWindowView view, IEventAggregator eventAggregator)
        {
            this.InitializeCommands();

            this.eventAggregator = eventAggregator;
            this.View = view;
            this.View.Model = this;
        }

        #endregion

        #region InitializeCommands Method

        /// <summary>
        /// Initializes the commands
        /// </summary>
        public void InitializeCommands()
        {
            this.CloseCommand = new DelegateCommand(OnClose);
        }

        #endregion

        #region OnClose Method

        /// <summary>
        /// Closes the window
        /// </summary>
        public void OnClose()
        {
            this.View.Close();
        }

        #endregion
    }
}
