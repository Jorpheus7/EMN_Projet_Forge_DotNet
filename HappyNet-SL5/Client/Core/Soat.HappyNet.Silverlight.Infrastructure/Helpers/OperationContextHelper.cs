// -----------------------------------------------------------------------
// <copyright file="OperationContextHelper.cs" company="So@t">
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
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Soat.HappyNet.Silverlight.Infrastructure.Helpers
{
    public class OperationContextHelper
    {
        /// <summary>
        /// Creates an OperationContextScope for a provided web service proxy
        /// It inserts some authentication metadata within the header of each outgoing request
        /// </summary>
        public static OperationContextScope Create<TChannel>(ClientBase<TChannel> client) where TChannel : class
        {
            OperationContextScope scope =
                new OperationContextScope((IContextChannel)client.InnerChannel);
            MessageHeaders messageHeadersElement =
              OperationContext.Current.OutgoingMessageHeaders;
            messageHeadersElement.Clear();
            messageHeadersElement.Add(
               MessageHeader.CreateHeader("username", "", Globals.UserName));
            messageHeadersElement.Add(
               MessageHeader.CreateHeader("password", "", Globals.Password));

            return scope;
        }
    }
}
