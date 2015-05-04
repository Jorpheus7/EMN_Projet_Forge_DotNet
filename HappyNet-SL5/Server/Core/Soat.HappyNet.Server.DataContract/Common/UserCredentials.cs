using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soat.HappyNet.Server.DataContract.Common
{
    public class UserCredentials
    {
        #region Constants
        public const string WS_NAMESPACE = "urn:Soat.AdventureWorks.Security";
        #endregion

        #region Public Properties
        public string UserName { get; set; }
        public string Password { get; set; }
        #endregion

        #region Constructors
        public UserCredentials()
        {
        }

        public UserCredentials(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
        #endregion
    }
}
