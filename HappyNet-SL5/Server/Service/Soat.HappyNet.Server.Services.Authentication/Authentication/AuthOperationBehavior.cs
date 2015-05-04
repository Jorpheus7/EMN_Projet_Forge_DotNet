// -----------------------------------------------------------------------
// <copyright file="AuthOperationBehavior.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;

namespace Soat.HappyNet.Server.Services.Authentication.Authentication
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthOperationBehavior : Attribute, IOperationBehavior
    {
        /// <summary>
        /// Applies the behavior
        /// </summary>
        public void ApplyDispatchBehavior(OperationDescription operationDescription, System.ServiceModel.Dispatcher.DispatchOperation dispatchOperation)
        {
            dispatchOperation.Invoker = new AuthOperationInvoker(dispatchOperation.Invoker);
        }

        #region IOperationBehavior Members

        public void AddBindingParameters(OperationDescription operationDescription, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, System.ServiceModel.Dispatcher.ClientOperation clientOperation)
        {
        }

        public void Validate(OperationDescription operationDescription)
        {
        }

        #endregion
    }
}
