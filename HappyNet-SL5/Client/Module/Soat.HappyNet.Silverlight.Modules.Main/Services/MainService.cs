// -----------------------------------------------------------------------
// <copyright file="MainService.cs" company="So@t">
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
using Microsoft.Practices.Unity;
using Soat.HappyNet.Silverlight.Library;
using Soat.HappyNet.Silverlight.Modules.Main.PersonServiceReference;
using Soat.HappyNet.Silverlight.Infrastructure.Helpers;

namespace Soat.HappyNet.Silverlight.Modules.Main.Services
{
    /// <summary>
    /// Class defining the service of main module
    /// </summary>
    public class MainService : IMainService
    {
        #region Const

        /// <summary>
        /// Configuration name for the client proxy (cf. ServiceReferences.ClientConfig in the Shell Project)
        /// </summary>
        const string ServiceConfigurationName = "PersonServiceConfiguration";

        #endregion
        
        #region Private members

        /// <summary>
        /// Web services proxy
        /// </summary>
        private readonly PersonServiceClient client;

        /// <summary>
        /// Unity container
        /// </summary>
        private readonly IUnityContainer container;

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainService(IUnityContainer container)
        {
            this.container = container;

            this.client = new PersonServiceClient(MainService.ServiceConfigurationName);

            client.LoginCompleted += new EventHandler<LoginCompletedEventArgs>(OnLoginCompleted);
        }

        #region Login

        /// <summary>
        /// Authenticate the user
        /// </summary>
        /// <param name="user">User email</param>
        /// <param name="password">Password</param>
        /// <param name="LoginCompleted">Callback method</param>
        public void BeginLogin(string user, string password, ServiceCompleted<User> LoginCompleted)
        {
            client.LoginAsync(user, password,
                new ServiceArgs<User>(LoginCompleted));
        }

        void OnLoginCompleted(object sender, LoginCompletedEventArgs e)
        {
            User result = null;
            if (e.Error == null)
            {
                result = e.Result;
            }

            var arg = e.UserState as ServiceArgs<User>;
            arg.Complete(result, e);
        }

        #endregion
    }
}
