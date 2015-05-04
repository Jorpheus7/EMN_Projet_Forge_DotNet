// -----------------------------------------------------------------------
// <copyright file="HumanResourcesDataProvider.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Soat.HappyNet.Server.DataContract;

namespace Soat.HappyNet.Server.Services.HumanResources.DataAccessLayer
{
	/// <summary>
	/// Access data related to human resources
	/// </summary>
    public class HumanResourcesDataProvider : IHumanResourcesDataProvider
	{
		#region Constants

        private const string ALL_EMPLOYEE_VIEW = "HumanResources.uspGetAllEmployees";
		private const string CDatabase = "AdventureWorks2008";

		public const string C_Identifier = "BusinessEntityId";
		public const string C_Title = "Title";
		public const string C_FirstName = "FirstName";
		public const string C_MiddleName = "MiddleName";
		public const string C_LastName = "LastName";
		public const string C_suffix = "suffix";
		public const string C_JobTitle = "JobTitle";
		public const string C_PhoneNumber = "PhoneNumber";
		public const string C_PhoneNumberType = "PhoneNumberType";
		public const string C_EmailAddress = "EmailAddress";
		public const string C_EmailPromotion = "EmailPromotion";
		public const string C_AddressLine1 = "AddressLine1";
		public const string C_AddressLine2 = "AddressLine2";
		public const string C_City = "City";
		public const string C_StateProvinceName = "StateProvinceName";
		public const string C_PostalCode = "PostalCode";
		public const string C_CountryRegionName = "CountryRegionName";
		public const string C_AdditionalContactInfo = "AdditionalContactInfo";

		#endregion Constants

		#region Selection

        /// <summary>
        /// Gets all the employees
        /// </summary>
        /// <param name="sortColumnName">Sorting column</param>
        /// <param name="sortDirection">Sort direction</param>
        /// <returns>Employees list</returns>
		public IEnumerable<Employee> FindAllEmployees(string sortColumnName, SortDirection sortDirection)
		{
			Database db = DatabaseFactory.CreateDatabase(CDatabase);

			using (DbCommand dbCommand = db.GetStoredProcCommand(ALL_EMPLOYEE_VIEW))
			{
				using (IDataReader dataReader = db.ExecuteReader(dbCommand))
				{					
					while (dataReader.Read())
					{
							Employee myEmployee = new Employee();
							int Identifier = dataReader.GetOrdinal(C_Identifier);
							if (!dataReader.IsDBNull(Identifier)) { myEmployee.Identifier = Convert.ToInt32(dataReader.GetValue(Identifier)); }
							int Title = dataReader.GetOrdinal(C_Title);
							if (!dataReader.IsDBNull(Title)) { myEmployee.Title = dataReader.GetValue(Title).ToString(); }
							int FirstName = dataReader.GetOrdinal(C_MiddleName);
							if (!dataReader.IsDBNull(FirstName)) { myEmployee.FirstName = dataReader.GetValue(FirstName).ToString(); }
							int LastName = dataReader.GetOrdinal(C_LastName);
							if (!dataReader.IsDBNull(LastName)) { myEmployee.LastName = dataReader.GetValue(LastName).ToString(); }
							int suffix = dataReader.GetOrdinal(C_suffix);
							if (!dataReader.IsDBNull(suffix)) { myEmployee.suffix = dataReader.GetValue(suffix).ToString(); }
							int JobTitle = dataReader.GetOrdinal(C_JobTitle);
							if (!dataReader.IsDBNull(JobTitle)) { myEmployee.JobTitle = dataReader.GetValue(JobTitle).ToString(); }
							int PhoneNumber = dataReader.GetOrdinal(C_PhoneNumber);
							if (!dataReader.IsDBNull(PhoneNumber)) { myEmployee.PhoneNumber = dataReader.GetValue(PhoneNumber).ToString(); }
							int PhoneNumberType = dataReader.GetOrdinal(C_PhoneNumberType);
							if (!dataReader.IsDBNull(PhoneNumberType)) { myEmployee.PhoneNumberType = dataReader.GetValue(PhoneNumberType).ToString(); }
							int EmailAddress = dataReader.GetOrdinal(C_EmailAddress);
							if (!dataReader.IsDBNull(EmailAddress)) { myEmployee.EmailAddress = dataReader.GetValue(EmailAddress).ToString(); }
							int AddressLine1 = dataReader.GetOrdinal(C_AddressLine1);
							if (!dataReader.IsDBNull(AddressLine1)) { myEmployee.AddressLine1 = dataReader.GetValue(AddressLine1).ToString(); }
							int AddressLine2 = dataReader.GetOrdinal(C_AddressLine2);
							if (!dataReader.IsDBNull(AddressLine2)) { myEmployee.AddressLine2 = dataReader.GetValue(AddressLine2).ToString(); }
							int City = dataReader.GetOrdinal(C_City);
							if (!dataReader.IsDBNull(City)) { myEmployee.City = dataReader.GetValue(City).ToString(); }
							int StateProvinceName = dataReader.GetOrdinal(C_StateProvinceName);
							if (!dataReader.IsDBNull(StateProvinceName)) { myEmployee.StateProvinceName = dataReader.GetValue(StateProvinceName).ToString(); }
							int PostalCode = dataReader.GetOrdinal(C_PostalCode);
							if (!dataReader.IsDBNull(PostalCode)) { myEmployee.PostalCode = dataReader.GetValue(PostalCode).ToString(); }
							int CountryRegionName = dataReader.GetOrdinal(C_CountryRegionName);
							if (!dataReader.IsDBNull(CountryRegionName)) { myEmployee.CountryRegionName = dataReader.GetValue(CountryRegionName).ToString(); }
							int AdditionalContactInfo = dataReader.GetOrdinal(C_AdditionalContactInfo);
							if (!dataReader.IsDBNull(AdditionalContactInfo)) { myEmployee.AdditionalContactInfo = dataReader.GetValue(AdditionalContactInfo).ToString(); }
							yield return myEmployee;
					}
				}
			}
		}

		#endregion Selection
	}
}
