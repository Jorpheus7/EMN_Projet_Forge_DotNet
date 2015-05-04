// -----------------------------------------------------------------------
// <copyright file="ServiceArgs.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Text;
using System.Windows;
using Soat.HappyNet.Silverlight.Library.Components;

namespace Soat.HappyNet.Silverlight.Library
{
    public delegate void ServiceCompleted<T>(ServiceArgs<T> arg);

    public class ServiceArgs<T>
    {
        /// <summary>
        /// Client callback method
        /// </summary>
        ServiceCompleted<T> CompletedCallback { get; set; }

        /// <summary>
        /// Result from the web service
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// Error exception returned by the web service
        /// </summary>
        public Exception Error { get; set; }

        /// <summary>
        /// Objet que l'on souhaite voir revenir au retour du web service
        /// </summary>
        public object UserState { get; set; }

        /// <summary>
        /// Test whether there was an error and if the result is null
        /// </summary>
        public bool IsResultNullOrError
        {
            get
            {
                return (Error != null || Result == null);
            }
        }

        /// <summary>
        /// DateTime of the web service call
        /// </summary>
        public DateTime BeginDateTime { get; set; }
        /// <summary>
        /// DateTime of the web service response
        /// </summary>
        public DateTime EndDateTime { get; set; }

        /// <summary>
        /// Unique ID of the call
        /// </summary>
        Guid ID;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="callback">Callback method</param>
        public ServiceArgs(ServiceCompleted<T> callback)
            : this(callback, null)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="callback">Callback method</param>
        /// <param name="userState">Argument which can be retrieved when getting the response from the web service</param>
        public ServiceArgs(ServiceCompleted<T> callback, object userState)
        {
            ID = Guid.NewGuid();

            this.CompletedCallback = callback;
            this.UserState = userState;
            BeginDateTime = DateTime.Now;
        }

        /// <summary>
        /// Completes the web service response by calling the callback method and stuff
        /// </summary>
        /// <param name="result">Result from the web service</param>
        /// <param name="arg">Argument returned by the web service</param>
        public void Complete(T result, AsyncCompletedEventArgs arg)
        {
            this.EndDateTime = DateTime.Now;

            this.Result = result;
            if (arg != null)
            {
                this.Error = arg.Error;
            }

            if (this.CompletedCallback != null)
            {
                this.CompletedCallback(this);
            }

            this.CompletedCallback = null;
        }
    }
}
