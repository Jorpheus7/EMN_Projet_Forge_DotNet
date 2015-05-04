// -----------------------------------------------------------------------
// <copyright file="MessageChildWindowViewModel.cs" company="So@t">
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
using Soat.HappyNet.Silverlight.Infrastructure.Events;

namespace Soat.HappyNet.Silverlight.Views
{
    /// <summary>
    /// ViewModel to display a message through a child window
    /// </summary>
    public class MessageChildWindowViewModel : ViewModel<IMessageChildWindowView>, IMessageChildWindowViewModel
    {
        #region Private Members

        /// <summary>
        /// Unity container
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        #endregion

        #region Properties

        /// <summary>
        /// Command to validate
        /// </summary>
        public ICommand CloseCommand { get; private set; }

        private string message;
        /// <summary>
        /// Message to display
        /// </summary>
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                if (this.message != value)
                {
                    this.message = value;
                    Notify(() => this.Message);
                }
            }
        }

        private string messageTitle;
        /// <summary>
        /// Title to display
        /// </summary>
        public string MessageTitle
        {
            get
            {
                return messageTitle;
            }
            set
            {
                if (this.messageTitle != value)
                {
                    this.messageTitle = value;
                    Notify(() => this.MessageTitle);
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
        public MessageChildWindowViewModel(IMessageChildWindowView view, IEventAggregator eventAggregator)
        {
            this.InitializeCommands();

            this.eventAggregator = eventAggregator;
            this.View = view;
            this.View.Model = this;
        }

        #endregion

        #region InitializeCommands Method

        /// <summary>
        /// Initializes commands
        /// </summary>
        public void InitializeCommands()
        {
            this.CloseCommand = new DelegateCommand<string>(this.OnClose);
        }

        #endregion

        #region Close

        /// <summary>
        /// Validates the form
        /// </summary>
        /// <param name="args">Argument</param>
        public void OnClose(string args)
        {
            // Closes the window view
            this.View.Close();
        }

        #endregion
    }
}
