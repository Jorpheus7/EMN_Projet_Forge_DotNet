// -----------------------------------------------------------------------
// <copyright file="SilverlightFaultMessageInspector.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel;

namespace Soat.HappyNet.Server.Common.FaultBehavior
{
    /// <summary>
    /// This class inspects the exception messages on WCF server side
    /// </summary>
    public class SilverlightFaultMessageInspector : IDispatchMessageInspector
    {

        object IDispatchMessageInspector.AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            // Do nothing to the incoming message
            return null;
        }

        void IDispatchMessageInspector.BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            if (reply != null && reply.IsFault)
            {
                HttpResponseMessageProperty property = new HttpResponseMessageProperty();
                property.StatusCode = System.Net.HttpStatusCode.OK; // 200

                reply.Properties[HttpResponseMessageProperty.Name] = property;
            }
        }

    }
}
