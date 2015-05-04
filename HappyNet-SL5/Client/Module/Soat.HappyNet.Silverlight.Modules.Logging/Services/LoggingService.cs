// -----------------------------------------------------------------------
// <copyright file="LoggingService.cs" company="So@t">
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
using Soat.HappyNet.Silverlight.Modules.Logging.LoggingServiceReference;
using Soat.HappyNet.Silverlight.Infrastructure.Helpers;

namespace Soat.HappyNet.Silverlight.Modules.Logging.Services
{
    /// <summary>
    /// Class defining the service of logging
    /// </summary>
    public class LoggingService : ILoggingService
    {
        #region Const

        /// <summary>
        /// Configuration name for the client proxy (cf. ServiceReferences.ClientConfig in the Shell Project)
        /// </summary>
        private const string SERVICE_ENDPOINT = "LoggingServiceEndPoint"; 

        #endregion

        #region Private members

        private readonly LoggingServiceClient client;

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public LoggingService()
        {
            client = new LoggingServiceClient(SERVICE_ENDPOINT);
        }

        /// <summary>
        /// Writes an information log message
        /// </summary>
        /// <param name="message">Log message</param>
        public void BeginWriteMessage(string message)
        {
            client.WriteMessageAsync(message);
        }

        /// <summary>
        /// Writes an error log message
        /// </summary>
        /// <param name="message">Error message</param>
        public void BeginWriteErrorMessage(string errorMessage)
        {
            client.WriteErrorMessageAsync(errorMessage);
        }
    }
}
