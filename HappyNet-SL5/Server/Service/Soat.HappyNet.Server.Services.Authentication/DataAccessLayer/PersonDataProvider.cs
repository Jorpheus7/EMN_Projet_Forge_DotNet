// -----------------------------------------------------------------------
// <copyright file="PersonDataProvider.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Soat.HappyNet.Server.Entities;
using System.Security.Cryptography;
using Soat.HappyNet.Server.DataContract.Common;

namespace Soat.HappyNet.Server.Services.Authentication.DataAccessLayer
{
    /// <summary>
    /// Class defining the data access for persons
    /// </summary>
    public class PersonDataProvider : IPersonDataProvider
    {
        /// <summary>
        /// Look for a registered user based on credentials
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <returns>Registered user, null otherwise</returns>
        public User Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            try
            {
                using (AdventureWorksModel model = new AdventureWorksModel())
                {
                    // Looks for the first user matching the email (let's suppose the email is unique ...)
                    var user = (from p in model.Person.Include("Password").Include("EmailAddress")
                            where p.EmailAddress.Any(e => e.EmailAddress1.Trim().ToLower() == email.Trim().ToLower())
                            select p).FirstOrDefault();

                    if (user != null && user.Password != null)
                    {
                        // Password hash = MD5( login + password + password salt )
                        string passwordHash = MD5(string.Format("{0}{1}{2}", email, password.Trim(), user.Password.PasswordSalt)) + "=";
                        if (passwordHash == user.Password.PasswordHash)
                        {
                            return new User
                            {
                                UserID = user.BusinessEntityID,
                                Email = email,
                                FullName = string.Format("{0} {1}", user.FirstName, user.LastName)
                            };
                        }
                    }
                }
            }
            catch (ApplicationException e)
            {
                bool rethrow = ExceptionPolicy.HandleException(e, "Web UI Exception Policy");

                if (rethrow)
                {
                    throw;
                }
            }
            finally
            {
                Logger.Write("End Login", "General");
            }

            return null;
        }

        /// <summary>
        /// Encodes a string into MD5 encryption
        /// </summary>
        /// <param name="password">String to encode</param>
        /// <returns>MD5 version of the string</returns>
        private static string MD5(string password)
        {
            byte[] textBytes = System.Text.Encoding.Default.GetBytes(password);
            try
            {
                System.Security.Cryptography.MD5CryptoServiceProvider cryptHandler;
                cryptHandler = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] hash = cryptHandler.ComputeHash(textBytes);
                string ret = "";
                foreach (byte a in hash)
                {
                    if (a < 16)
                        ret += "0" + a.ToString("x");
                    else
                        ret += a.ToString("x");
                }
                return ret;
            }
            catch
            {
                throw;
            }
        }
    }
}
