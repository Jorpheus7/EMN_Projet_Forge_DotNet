// -----------------------------------------------------------------------
// <copyright file="LoginStateViewModel.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Presentation.Commands;
using Soat.HappyNet.Silverlight.Library;
using Microsoft.Practices.Unity;
using Soat.HappyNet.Silverlight.Modules.Main.Services;
using Soat.HappyNet.Silverlight.Library.Extensions;
using Soat.HappyNet.Silverlight.Infrastructure.Events;
using Soat.HappyNet.Silverlight.Infrastructure.Localization;
using Soat.HappyNet.Silverlight.Library.Commands;
using Soat.HappyNet.Silverlight.Modules.Main.PersonServiceReference;
using Soat.HappyNet.Silverlight.Infrastructure;

namespace Soat.HappyNet.Silverlight.Modules.Main.Views
{
    /// <summary>
    /// Class defining the ViewModel for the LoginState View
    /// </summary>
    public class LoginStateViewModel : ViewModel<ILoginStateView>, ILoginStateViewModel
    {
        #region Private members

        /// <summary>
        /// Event aggregator
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        /// <summary>
        /// Unity container
        /// </summary>
        private readonly IUnityContainer container;

        /// <summary>
        /// Service principal
        /// </summary>
        private readonly IMainService mainService;

        #endregion

        #region Properties

        private string fullName;
        /// <summary>
        /// Full name of the authenticated user
        /// </summary>
        public string FullName
        {
            get { return fullName; }
            set
            {
                if (fullName != value)
                {
                    fullName = value;
                    Notify(() => this.FullName);
                }
            }
        }

        private string userName;
        /// <summary>
        /// Username of the current user
        /// </summary>
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                if (this.userName != value)
                {
                    this.userName = value;
                    Notify(() => this.UserName);

                    ((DelegateCommand)LoginCommand).RaiseCanExecuteChanged();
                }
            }
        }

        private string password;
        /// <summary>
        /// Password of the current user
        /// </summary>
        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    Notify(() => this.Password);

                    ((DelegateCommand)LoginCommand).RaiseCanExecuteChanged();
                }
            }
        }
        
        private bool isLogged;
        /// <summary>
        /// Logged in status
        /// </summary>
        public bool IsLogged
        {
            get
            {
                return isLogged;
            }
            set
            {
                if (this.isLogged != value)
                {
                    this.isLogged = value;
                    Notify(() => this.IsLogged);
                }
            }
        }

        private bool isLoading;
        /// <summary>
        /// Loading indicator
        /// </summary>
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                if (isLoading != value)
                {
                    isLoading = value;
                    Notify(() => this.IsLoading);
                }
            }
        }
	
        #endregion

        #region Commandes

        /// <summary>
        /// Command to disconnect the user
        /// </summary>
        public ICommand LogoutCommand { get; private set; }

        /// <summary>
        /// Command to login the user
        /// </summary>
        public ICommand LoginCommand { get; private set; } 

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="view">View attached to this ViewModel</param>
        /// <param name="eventAggregator">Event aggregator</param>
        public LoginStateViewModel(ILoginStateView view, 
            IEventAggregator eventAggregator,
            IMainService mainService)
        {
            this.InitializeCommands();

            this.eventAggregator = eventAggregator;
            this.mainService = mainService;

            this.View = view;
            this.View.Model = this;

            this.IsLogged = false;
        }

        #endregion

        #region InitializeCommands Method

        /// <summary>
        /// Initiliazes the commands
        /// </summary>
        public void InitializeCommands()
        {
            this.LoginCommand = new DelegateCommand(this.OnLogin,
                () => !string.IsNullOrEmpty(this.UserName) && !string.IsNullOrEmpty(this.Password));

            this.LogoutCommand = new DelegateCommand(OnLogout);
        }

        #endregion

        #region OnLogin Method

        /// <summary>
        /// Logs in the user with current credentials
        /// </summary>
        public void OnLogin()
        {
            IsLoading = true;
            mainService.BeginLogin(this.UserName, this.Password, LoginCompleted);
        }

        void LoginCompleted(ServiceArgs<User> args)
        {
            IsLogged = args.Result != null;

            if (IsLogged)
            {
                this.Login(args.Result.Email, this.Password, args.Result.UserID, args.Result.FullName);
                eventAggregator.Publish(new NotificationEvent(NotificationType.Validation, Messages.OK_Authentified));
            }
            else
            {
                Logout();

                eventAggregator.Publish(new MessageShowEvent
                {
                    Message = Messages.ERR_NotAuthentified
                });
            }

            // Notify the whole application the login state has changed,
            // after setting all of the Globals variables
            eventAggregator.Publish(new LoginChangedEvent(IsLogged));
            IsLoading = false;
        }

        /// <summary>
        /// Logs in the user with the given credentials
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="userid">User id</param>
        /// <param name="fullname">Fullname</param>
        void Login(string username, string password, int userid, string fullname)
        {
            Globals.UserName = username;
            Globals.Password = password;
            Globals.UserID = userid;
            Globals.FullName = fullname;
            Globals.IsLoggedIn = true;

            IsLogged = true;

            this.Password = null;
            this.FullName = fullname;
        }

        #endregion

        #region OnLogout Method

        /// <summary>
        /// Logs out the user
        /// </summary>
        /// <param name="args">Null</param>
        public void OnLogout()
        {
            Logout();
            eventAggregator.Publish(new NotificationEvent(NotificationType.Validation, Messages.OK_LoggedOut));

            eventAggregator.Publish(new LoginChangedEvent(false));
        }

        /// <summary>
        /// Log out method
        /// </summary>
        void Logout()
        {
            Globals.UserName = this.UserName = null;
            Globals.Password = this.Password = null;
            Globals.FullName = this.FullName = null;
            Globals.IsLoggedIn = IsLogged = false;
            Globals.UserID = -1;
        }

        #endregion
    }
}
