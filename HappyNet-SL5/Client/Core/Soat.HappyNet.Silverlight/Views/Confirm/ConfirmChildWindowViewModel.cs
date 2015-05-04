// -----------------------------------------------------------------------
// <copyright file="ConfirmChildWindowViewModel.cs" company="So@t">
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
using Soat.HappyNet.Silverlight.Library.Commands;

namespace Soat.HappyNet.Silverlight.Views
{
    /// <summary>
    /// ViewModel to display a confirmation message through a child window
    /// </summary>
    public class ConfirmChildWindowViewModel : ViewModel<IConfirmChildWindowView>, IConfirmChildWindowViewModel
    {
        #region Private Members

        /// <summary>
        /// Unity container
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        #endregion

        #region Properties

        /// <summary>
        /// Command to cancel
        /// </summary>
        public ICommand CancelCommand { get; private set; }

        /// <summary>
        /// Command to validate
        /// </summary>
        public ICommand ValidateCommand { get; private set; }

		private string cancelButtonText;
        /// <summary>
        /// Text of the button executing the CancelCommand
        /// Exemples : "Cancel", "No".
        /// </summary>
		public string CancelButtonText
		{
			get
			{
				return cancelButtonText;
			}
			set
			{
				if (this.cancelButtonText != value)
				{
					this.cancelButtonText = value;
					Notify(() => this.CancelButtonText);
				}
			}
		}

		private string validateButtonText;
        /// <summary>
        /// Text of the button executing the ValidateCommand
        /// Exemples : "OK", "Yes".
        /// <summary>
		public string ValidateButtonText
		{
			get
			{
				return validateButtonText;
			}
			set
			{
				if (this.validateButtonText != value)
				{
					this.validateButtonText = value;
					Notify(() => this.ValidateButtonText);
				}
			}
		}

        /// <summary>
        /// Membre privée Message
        /// </summary>
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
        /// Title of the message
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

        private Action<bool?> confirmAction;
        /// <summary>
        /// Action to execute after the confirmation
        /// </summary>
        public Action<bool?> ConfirmAction
        {
            get
            {
                return confirmAction;
            }
            set
            {
                if (this.confirmAction != value)
                {
                    this.confirmAction = value;
                    Notify(() => this.ConfirmAction);
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
        public ConfirmChildWindowViewModel(IConfirmChildWindowView view, IEventAggregator eventAggregator)
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
            this.CancelCommand = new DelegateCommand(this.OnCancel);
            this.ValidateCommand = new DelegateCommand(this.OnValidate);

			this.CancelButtonText = "Annuler";
			this.ValidateButtonText = "OK";
        }

        #endregion

        #region OnCancel Method

        /// <summary>
        /// Method to handle the cancel
        /// </summary>
        public void OnCancel()
        {
            // Action called after cancel
            this.ConfirmAction(false);

            // Closes the popup
            Close();
        }

        #endregion

        #region OnValidate Method

        /// <summary>
        /// Method to handle the validation
        /// </summary>
        public void OnValidate()
        {
            // Action called after validation
            this.ConfirmAction(true);

            // Fermeture du Popup
            Close();
        }

        #endregion

        void Close()
        {
            this.View.Close();
            this.ConfirmAction = null;
        }
    }
}
