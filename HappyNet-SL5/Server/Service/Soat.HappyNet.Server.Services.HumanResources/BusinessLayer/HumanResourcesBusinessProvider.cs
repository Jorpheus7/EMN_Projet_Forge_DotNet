// -----------------------------------------------------------------------
// <copyright file="HumanResourcesBusinessProvider.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soat.HappyNet.Server.Services.HumanResources.DataAccessLayer;
using Soat.HappyNet.Server.DataContract;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Soat.HappyNet.Server.Services.HumanResources.BusinessLayer
{
	/// <summary>
	/// Class defining the business layer for human resources
	/// </summary>
	public class HumanResourcesBusinessProvider : IHumanResourcesBusinessProvider
    {
        #region Properties

        /// <summary>
		/// Data Provider for human resources
		/// </summary>
        public IHumanResourcesDataProvider HumanResDataProvider { get; private set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="personDataProvider">Data provider for human resources</param>
        public HumanResourcesBusinessProvider(IHumanResourcesDataProvider hrDataProvider)
		{
            this.HumanResDataProvider = hrDataProvider;
		}

		#endregion

		#region FindAllEmployees Method

		/// <summary>
		/// Retrieves all the employees
		/// </summary>
		/// <param name="pageNumber">Page number</param>
		/// <param name="pageSize">Employee number per page</param>
		/// <param name="sortColumnName">Sorting column</param>
		/// <param name="sortDirection">Sort direction</param>
		public IEnumerable<Employee> FindAllEmployees(int pageNumber, int pageSize, string sortColumnName, SortDirection sortDirection)
		{
			try
			{
				IEnumerable<Employee> myPersons = HumanResDataProvider.FindAllEmployees(sortColumnName, sortDirection);
				return myPersons;
			}
			catch (SystemException e)
			{
				bool rethrow = ExceptionPolicy.HandleException(e, "Web UI Exception Policy");

				if (rethrow)
				{
					throw;
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

				Logger.Write("End FindAllEmployees", "General");
			}
			return null;
		}

		#endregion
	}
}
