// -----------------------------------------------------------------------
// <copyright file="IHumanResourcesDataProvider.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using Soat.HappyNet.Server.DataContract;


namespace Soat.HappyNet.Server.Services.HumanResources.DataAccessLayer
{
    /// <summary>
    /// Interface defining all the methods to access persons data
    /// </summary>
    public interface IHumanResourcesDataProvider
    {
        /// <summary>
        /// Gets all the employees
        /// </summary>
        /// <param name="sortColumnName">Sorting column</param>
        /// <param name="sortDirection">Sort direction</param>
        /// <returns>Employees list</returns>
        IEnumerable<Employee> FindAllEmployees(string sortColumnName, SortDirection sortDirection);
		
    }
}
