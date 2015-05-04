// -----------------------------------------------------------------------
// <copyright file="ILoggingService.cs" company="So@t">
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

namespace Soat.HappyNet.Silverlight.Modules.Logging.Services
{
    /// <summary>
    /// Class defining the service of logging
    /// </summary>
    public interface ILoggingService
    {
        /// <summary>
        /// Writes an information log message
        /// </summary>
        /// <param name="message">Log message</param>
        void BeginWriteMessage(string message);

        /// <summary>
        /// Writes an error log message
        /// </summary>
        void BeginWriteErrorMessage(string errorMessage);
    }
}
