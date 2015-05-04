// -----------------------------------------------------------------------
// <copyright file="IHumanResourcesService.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Soat.HappyNet.Server.DataContract;

namespace Soat.HappyNet.Server.Services.HumanResources
{
    /// <summary>
    /// Interface defining the contract of services for human resources
    /// </summary>
    [ServiceContract]
    public interface IHumanResourcesService
    {
        /// <summary>
        /// Retrieves all the employees
        /// </summary>
        /// <param name="pageNumber">Page number</param>
        /// <param name="pageSize">Employee number per page</param>
        /// <param name="sortColumnName">Sorting column</param>
        /// <param name="sortDirection">Sort direction</param>
        [OperationContract]
        IEnumerable<Employee> FindAllEmployees(int pageNumber, int pageSize, string sortColumnName, SortDirection sortDirection);
    }
}
