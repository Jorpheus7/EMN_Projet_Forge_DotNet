// -----------------------------------------------------------------------
// <copyright file="AuthOperationInvoker.cs" company="So@t">
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
using System.Web;
using System.Security.Principal;
using System.Web.Security;
using System.Threading;

namespace Soat.HappyNet.Server.Services.Authentication.Authentication
{
    public class AuthOperationInvoker : IOperationInvoker
    {
        IOperationInvoker InnerOperationInvoker { get; set; }

        public AuthOperationInvoker(IOperationInvoker invoker)
        {
            this.InnerOperationInvoker = invoker;
        }

        public object Invoke(object instance, object[] inputs, out object[] outputs)
        {
            string username;
            string password;

            // Gets the incoming headers, looking for credentials
            MessageHeaders messageHeadersElement = OperationContext.Current.IncomingMessageHeaders;
            try
            {
                username = messageHeadersElement.GetHeader<String>("username", "");
                password = messageHeadersElement.GetHeader<String>("password", "");
            }
            catch
            {
                // Not found ? Acces denied !
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Access Denied"), new FaultReason("Access Denied"));
            }

            // Tries to load a user based on credentials
            IPersonService service = new PersonService();
            var user = service.Login(username, password);

            if (user != null)
            {
                // User assigned to the current context
                GenericPrincipal securityPrincipal = new GenericPrincipal(new GenericIdentity(user.UserID.ToString()), null);
                HttpContext.Current.User = securityPrincipal;
                Thread.CurrentPrincipal = securityPrincipal;

                return InnerOperationInvoker.Invoke(instance, inputs, out outputs);
            }
            else
            {
                // Not found ? Acces denied !
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Access Denied"), new FaultReason("Access Denied"));
            }
        }

        #region IOperationInvoker Members

        public object[] AllocateInputs()
        {
            return InnerOperationInvoker.AllocateInputs();
        }

        public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
        {
            return InnerOperationInvoker.InvokeBegin(instance, inputs, callback, state);
        }

        public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
        {
            return InnerOperationInvoker.InvokeEnd(instance, out outputs, result);
        }

        public bool IsSynchronous
        {
            get { return InnerOperationInvoker.IsSynchronous; }
        }

        #endregion
    }
}
