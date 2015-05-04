// -----------------------------------------------------------------------
// <copyright file="Employee.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Soat.HappyNet.Server.DataContract
{
    /// <summary>
    /// Represents an employee
    /// </summary>
    [DataContract]
    public class Employee
    {
        /// <summary>
        /// Employee id
        /// </summary>
        [DataMember]
        public int Identifier { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        [DataMember]
        public string FirstName { get; set; }

		/// <summary>
		/// Middle name
		/// </summary>
		[DataMember]
		public string MiddleName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        [DataMember]
        public string LastName { get; set; }


		/// <summary>
		/// Suffix
		/// </summary>
		[DataMember]
		public string suffix { get; set; }

		/// <summary>
		/// Job title
		/// </summary>
		[DataMember]
		public string JobTitle { get; set; }

		/// <summary>
		/// Phone number
		/// </summary>
		[DataMember]
		public string PhoneNumber { get; set; }

		/// <summary>
		/// Phone number type
		/// </summary>
		[DataMember]
		public string PhoneNumberType { get; set; }

		/// <summary>
		/// Email
		/// </summary>
		[DataMember]
		public string EmailAddress { get; set; }

		/// <summary>
		/// Email promotion
		/// </summary>
		[DataMember]
		public string EmailPromotion { get; set; }

		/// <summary>
		/// Address line 1
		/// </summary>
		[DataMember]
		public string AddressLine1 { get; set; }

		/// <summary>
		/// Address line 2
		/// </summary>
		[DataMember]
		public string AddressLine2 { get; set; }

		/// <summary>
		/// City
		/// </summary>
		[DataMember]
		public string City { get; set; }

		/// <summary>
		/// State name
		/// </summary>
		[DataMember]
		public string StateProvinceName { get; set; }

		/// <summary>
		/// Postal code
		/// </summary>
		[DataMember]
		public string PostalCode { get; set; }

		/// <summary>
		/// Country region name
		/// </summary>
		[DataMember]
		public string CountryRegionName { get; set; }

		/// <summary>
		/// Additional contact info
		/// </summary>
		[DataMember]
		public string AdditionalContactInfo { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Employee()
        {
            this.Identifier = -1;
        }
    }
}
