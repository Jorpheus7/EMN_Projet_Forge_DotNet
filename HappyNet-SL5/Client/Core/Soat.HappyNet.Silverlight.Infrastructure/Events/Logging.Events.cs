// -----------------------------------------------------------------------
// <copyright file="Logging.Events.cs" company="So@t">
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

namespace Soat.HappyNet.Silverlight.Infrastructure.Events
{
    /// <summary>
    /// Enumeration for the logging category
    /// </summary>
    public enum LoggingCategory
    {
        Message,
        Error
    }

    /// <summary>
    /// Event to create a log
    /// </summary>
    public class LoggingEvent : CompositePresentationEvent<LoggingEvent>
    {
        /// <summary>
        /// Category
        /// </summary>
        public LoggingCategory Category { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public LoggingEvent()
        {
        }

        /// <summary>
        /// Constructor (error category)
        /// </summary>
        /// <param name="message">Error message</param>
        public LoggingEvent(string message)
        {
            this.Message = message;
            this.Category = LoggingCategory.Error;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="category">Category</param>
        public LoggingEvent(string message, LoggingCategory category)
        {
            this.Message = message;
            this.Category = category;
        }
    }
}
