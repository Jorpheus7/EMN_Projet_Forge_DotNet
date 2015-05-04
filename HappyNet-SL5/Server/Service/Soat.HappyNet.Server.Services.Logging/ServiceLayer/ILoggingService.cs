// -----------------------------------------------------------------------
// <copyright file="ILoggingService.cs" company="So@t">
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

namespace Soat.HappyNet.Server.Services.Logging
{
    /// <summary>
    /// Interface defining the contract of services for logging messages
    /// </summary>
    [ServiceContract]
    public interface ILoggingService
    {
        /// <summary>
        /// Writes an information log message
        /// </summary>
        /// <param name="message">Log message</param>
        [OperationContract(IsOneWay=true)]
        void WriteMessage(string message);

        /// <summary>
        /// Writes an error log message
        /// </summary>
        /// <param name="message">Error message</param>
        [OperationContract(IsOneWay = true)]
        void WriteErrorMessage(string errorMessage);
    }
}
