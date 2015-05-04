// -----------------------------------------------------------------------
// <copyright file="SilverlightFaultBehavior.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Soat.HappyNet.Server.Common.FaultBehavior
{
    /// <summary>
    /// This class represents a behavior to associate to a WCF service
    /// to get exceptions compatible with Silverlight client
    /// </summary>
    public class SilverlightFaultBehavior : BehaviorExtensionElement, IEndpointBehavior
    {
        /// <summary>
        /// Retrieves the SilverlightFaultBehavior type
        /// </summary>
        public override System.Type BehaviorType
        {
            get { return typeof(SilverlightFaultBehavior); }
        }
        
        /// <summary>
        /// Creates an instance of SilverlightFaultBehavior
        /// </summary>
        /// <returns></returns>
        protected override object CreateBehavior()
        {
            return new SilverlightFaultBehavior();
        }

        /// <summary>
        /// Adds parameters to bind to the service endpoint
        /// </summary>
        /// <param name="endpoint">Configuration EndPoint</param>
        /// <param name="bindingParameters">Binding parameters</param>
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        /// <summary>
        /// Applies the behavior to the client runtime
        /// </summary>
        /// <param name="endpoint">Configuration EndPoint</param>
        /// <param name="clientRuntime">Client runtime</param>
        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        /// <summary>
        /// Applies the behavior to the endpoints dispatcher
        /// </summary>
        /// <param name="endpoint">Configuration EndPoint</param>
        /// <param name="endpointDispatcher">Dispatcher EndPoint</param>
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            SilverlightFaultMessageInspector inspector = new SilverlightFaultMessageInspector();
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(inspector);
        }

        /// <summary>
        /// Validates the endpoint
        /// </summary>
        /// <param name="endpoint">Configuration EndPoint</param>
        public void Validate(ServiceEndpoint endpoint)
        {
        }


    }
}