// -----------------------------------------------------------------------
// <copyright file="Main.Events.cs" company="So@t">
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
using Microsoft.Practices.Composite.Presentation.Events;
using Soat.HappyNet.Server.DataContract;
using Soat.HappyNet.Silverlight.Infrastructure.Localization;
using Microsoft.Practices.Composite.Events;
using Soat.HappyNet.Silverlight.Infrastructure.Models;

namespace Soat.HappyNet.Silverlight.Infrastructure.Events
{
    #region MessageShowEvent

    /// <summary>
    /// Event to open a message window
    /// </summary>
    public class MessageShowEvent : CompositePresentationEvent<MessageShowEvent>
    {
        /// <summary>
        /// Window title
        /// </summary>
        public string MessageTitle { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public MessageShowEvent()
        {
            this.MessageTitle = "Information";
        }
    }

    #endregion

    #region ConfirmShowEvent

    /// <summary>
    /// Event to open a confirmation window
    /// </summary>
    public class ConfirmShowEvent : CompositePresentationEvent<ConfirmShowEvent>
    {
		/// <summary>
        /// Window title
        /// </summary>
        public string MessageTitle { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

		/// <summary>
		/// Text to be displayed in the cancel button
		/// Examples : "Cancel", "No".
		/// </summary>
		public string CancelButtonText { get; set; }

		/// <summary>
		/// Text to be displayed in the validation button
		/// Examples : "OK", "Yes".
		/// </summary>
		public string ValidateButtonText { get; set; }

		/// <summary>
        /// Action to execute after confirmation
        /// </summary>
        public Action<bool?> ConfirmAction { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public ConfirmShowEvent()
        {
			this.MessageTitle = StringLibrary.APP_Confirmation;
			this.CancelButtonText = StringLibrary.APP_Cancel;
            this.ValidateButtonText = StringLibrary.APP_OK;
        }
    }

    #endregion

    #region ErrorEvent

    /// <summary>
    /// Event to open an error window
    /// </summary>
    public class ErrorEvent : CompositePresentationEvent<ErrorEvent>
    {
        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Exception object to display
        /// </summary>
        public Exception ExceptionObj { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public ErrorEvent()
        {
        }
    }

    #endregion

    #region NavigateToChangedEvent Event

    /// <summary>
    /// Event to navigate to a page
    /// </summary>
    public class NavigateToEvent : CompositePresentationEvent<NavigateToEvent>
    {
        /// <summary>
        /// Uri to navigate to
        /// </summary>
        public string RedirectUri { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public NavigateToEvent()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="redirectUri">Uri to navigate to</param>
        public NavigateToEvent(string redirectUri)
        {
            this.RedirectUri = redirectUri;
        }
    }

    #endregion

    #region Navigated Event

    /// <summary>
    /// Event to notify a navigation has been completed
    /// </summary>
    public class NavigatedEvent : CompositePresentationEvent<NavigatedEvent>
    {
        /// <summary>
        /// Uri
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public NavigatedEvent()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uri">Uri</param>
        public NavigatedEvent(Uri uri)
        {
            this.Uri = uri;
        }
    }

    #endregion

    #region LocalizationChangedEvent

    /// <summary>
    /// Event to notify the localization has changed
    /// </summary>
    public class LocalizationChangedEvent : CompositePresentationEvent<LocalizationChangedEvent>
    {
        /// <summary>
        /// Language
        /// </summary>
        public Language Language { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public LocalizationChangedEvent()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lang">Language</param>
        public LocalizationChangedEvent(Language lang)
        {
            this.Language = lang;
        }
    }

    #endregion

    #region Notification Event

    /// <summary>
    /// Enumeration for the kind of notification
    /// </summary>
    public enum NotificationType
    {
        Validation,
        Error
    }

    /// <summary>
    /// Event to display a notification
    /// </summary>
    public class NotificationEvent : CompositePresentationEvent<NotificationEvent>
    {
        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Notification type
        /// </summary>
        public NotificationType Type { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public NotificationEvent()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">Notification type</param>
        /// <param name="message">Message</param>
        public NotificationEvent(NotificationType type, string message)
        {
            this.Message = message;
            this.Type = type;
        }
    }
    #endregion

    #region LoginChangedEvent

    /// <summary>
    /// Event to notify the login has changed
    /// </summary>
    public class LoginChangedEvent : CompositePresentationEvent<LoginChangedEvent>
    {
        /// <summary>
        /// Logged in status
        /// </summary>
        public bool IsLogged { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public LoginChangedEvent()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="isLogged">Logged in status</param>
        public LoginChangedEvent(bool isLogged)
        {
            this.IsLogged = isLogged;
        }
    }

    #endregion

    #region AddMenuButtonEvent

    /// <summary>
    /// Event to add a menu button
    /// </summary>
    public class AddMenuButtonEvent : CompositePresentationEvent<AddMenuButtonEvent>
    {
        public Func<string> NameProvider { get; set; }

        public string Url { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public AddMenuButtonEvent()
        {
        }
    }

    #endregion

    #region RemoveMenuButtonEvent

    /// <summary>
    /// Event to remove a menu button
    /// </summary>
    public class RemoveMenuButtonEvent : CompositePresentationEvent<RemoveMenuButtonEvent>
    {
        public string Url { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public RemoveMenuButtonEvent()
        {
        }

        public RemoveMenuButtonEvent(string url)
        {
            this.Url = url;
        }
    }

    #endregion
}