// -----------------------------------------------------------------------
// <copyright file="LoggingService.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.ServiceModel.Activation;

namespace Soat.HappyNet.Server.Services.Logging
{
    /// <summary>
    /// Class defining the services for logging purpose coming from the Silverlight client (or else)
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class LoggingService : ILoggingService
    {
        #region Constants

        /// <summary>
        /// Constant defining the name of the message category to log info in
        /// </summary>
        private const string CLIENT_INFO_CATEGORY = "Client.Info";
        /// <summary>
        /// Constant defining the name of the message category to log errors in
        /// </summary>
        private const string CLIENT_ERROR_CATEGORY = "Client.Error"; 

        #endregion

        /// <summary>
        /// Writes an information log message
        /// </summary>
        /// <param name="message">Log message</param>
        public void WriteMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                Logger.Write(message, LoggingService.CLIENT_INFO_CATEGORY);
            }
        }

        /// <summary>
        /// Writes an error log message
        /// </summary>
        /// <param name="message">Error message</param>
        public void WriteErrorMessage(string errorMessage)
        {
            if (!string.IsNullOrEmpty(errorMessage))
            {
                Logger.Write(errorMessage, LoggingService.CLIENT_ERROR_CATEGORY);
            }
        }
    }
}
