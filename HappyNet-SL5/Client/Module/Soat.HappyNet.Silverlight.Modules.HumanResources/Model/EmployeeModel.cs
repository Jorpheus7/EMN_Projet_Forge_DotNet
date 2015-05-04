// -----------------------------------------------------------------------
// <copyright file="EmployeeModel.cs" company="So@t">
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
using Soat.HappyNet.Silverlight.Library;

namespace Soat.HappyNet.Silverlight.Modules.HumanResources.Model
{
    /// <summary>
    /// Represents an employee for display
    /// </summary>
    public class EmployeeModel : ViewModel
    {
        #region Identifier Property

        private int identifier;

        /// <summary>
        /// Employee ID
        /// </summary>
        public int Identifier
        {
            get
            {
                return identifier;
            }
            set
            {
                if (this.identifier != value)
                {
                    this.identifier = value;
                    Notify(() => this.Identifier);
                }
            }
        }

        #endregion

        #region Title Property

        private string title;

        /// <summary>
        /// Title
        /// </summary>
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (this.title != value)
                {
                    this.title = value;
                    Notify(() => this.Title);
                }
            }
        }

        #endregion

        #region FirstName Property

        private string firstName;

        /// <summary>
        /// Firstname
        /// </summary>
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (this.firstName != value)
                {
                    this.firstName = value;
                    Notify(() => this.FirstName);
                }
            }
        }

        #endregion

        #region LastName Property

        private string lastName;

        /// <summary>
        /// Lastname
        /// </summary>
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                if (this.lastName != value)
                {
                    this.lastName = value;
                    Notify(() => this.LastName);
                }
            }
        }

        #endregion

		#region MidlleName Property

		private string middleName;

		/// <summary>
		/// Midllename
		/// </summary>
		public string MidlleName
		{
			get
			{
				return middleName;
			}
			set
			{
				if (this.middleName != value)
				{
					this.middleName = value;
					Notify(() => this.MidlleName);
				}
			}
		}

		#endregion

		#region Suffix Property

		private string suffix;

		/// <summary>
		/// Suffix
		/// </summary>
		public string Suffix
		{
			get
			{
				return suffix;
			}
			set
			{
				if (this.suffix != value)
				{
					this.suffix = value;
					Notify(() => this.Suffix);
				}
			}
		}

		#endregion
		
		#region Jobtitle Property

		private string jobtitle;


		/// <summary>
		/// Job title
		/// </summary>
		public string JobTitle
		{
			get
			{
				return jobtitle;
			}
			set
			{
				if (this.jobtitle != value)
				{
					this.jobtitle = value;
					Notify(() => this.JobTitle);
				}
			}
		}

		#endregion

		#region PhoneNumber Property

		private string phoneNumber;

		/// <summary>
		/// Phone number
		/// </summary>
		public string PhoneNumber
		{
			get
			{
				return phoneNumber;
			}
			set
			{
				if (this.phoneNumber != value)
				{
					this.phoneNumber = value;
					Notify(() => this.PhoneNumber);
				}
			}
		}

		#endregion
		
        #region PhoneNumberType Property

        private string phoneNumberType;

        /// <summary>
        /// Phone number type
        /// </summary>
        public string PhoneNumberType
        {
            get
            {
                return phoneNumberType;
            }
            set
            {
                if (this.phoneNumberType != value)
                {
                    this.phoneNumberType = value;
                    Notify(() => this.PhoneNumberType);
                }
            }
        } 

        #endregion

        #region EmailAddress Property

        private string emailAddress;

        /// <summary>
        /// Email address
        /// </summary>
        public string EmailAddress
        {
            get
            {
                return emailAddress;
            }
            set
            {
                if (this.emailAddress != value)
                {
                    this.emailAddress = value;
                    Notify(() => this.EmailAddress);
                }
            }
        } 

        #endregion
	
        #region EmailPromotion Property

        private string emailPromotion;

        /// <summary>
        /// Email promotion
        /// </summary>
        public string EmailPromotion
        {
            get
            {
                return emailPromotion;
            }
            set
            {
                if (this.emailPromotion != value)
                {
                    this.emailPromotion = value;
                    Notify(() => this.EmailPromotion);
                }
            }
        } 

        #endregion

        #region AddressLine1 Property

        private string addressLine1;

        /// <summary>
        /// Address line 1
        /// </summary>
        public string AddressLine1
        {
            get
            {
                return addressLine1;
            }
            set
            {
                if (this.addressLine1 != value)
                {
                    this.addressLine1 = value;
                    Notify(() => this.AddressLine1);
                }
            }
        } 

        #endregion
		
		#region AddressLine2 Property

		private string addresLine2;

		/// <summary>
		/// Address line 2
		/// </summary>
		public string AddressLine2
		{
			get
			{
				return addresLine2;
			}
			set
			{
				if (this.addresLine2 != value)
				{
					this.addresLine2 = value;
					Notify(() => this.AddressLine2);
				}
			}
		}

		#endregion
		
        #region City Property

        private string city;

        /// <summary>
        /// City
        /// </summary>
        public string City
        {
            get
            {
                return city;
            }
            set
            {
                if (this.city != value)
                {
                    this.city = value;
                    Notify(() => this.City);
                }
            }
        } 

        #endregion

        #region StateProvinceName Property

        private string stateProvinceName;

        /// <summary>
        /// State province name
        /// </summary>
        public string StateProvinceName
        {
            get
            {
                return stateProvinceName;
            }
            set
            {
                if (this.stateProvinceName != value)
                {
                    this.stateProvinceName = value;
                    Notify(() => this.StateProvinceName);
                }
            }
        } 

        #endregion
		
        #region PostalCode Property

        private string postalCode;

        /// <summary>
        /// Postal code
        /// </summary>
        public string PostalCode
        {
            get
            {
                return postalCode;
            }
            set
            {
                if (this.postalCode != value)
                {
                    this.postalCode = value;
                    Notify(() => this.PostalCode);
                }
            }
        } 

        #endregion
		
        #region CountryRegionName Property

        private string countryRegionName;

        /// <summary>
        /// Country region name
        /// </summary>
        public string CountryRegionName
        {
            get
            {
                return countryRegionName;
            }
            set
            {
                if (this.countryRegionName != value)
                {
                    this.countryRegionName = value;
                    Notify(() => this.CountryRegionName);
                }
            }
        } 

        #endregion

        #region AdditionalContactInfo Property

        private string additionalContactInfo;

        /// <summary>
        /// Additional contact info
        /// </summary>
        public string AdditionalContactInfo
        {
            get
            {
                return additionalContactInfo;
            }
            set
            {
                if (this.additionalContactInfo != value)
                {
                    this.additionalContactInfo = value;
                    Notify(() => this.AdditionalContactInfo);
                }
            }
        } 

        #endregion
    }
}
