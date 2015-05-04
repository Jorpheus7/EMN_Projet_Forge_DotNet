// -----------------------------------------------------------------------
// <copyright file="IHumanResourcesService.cs" company="So@t">
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
using Soat.HappyNet.Silverlight.Infrastructure;
using Soat.HappyNet.Silverlight.Modules.HumanResources.HumanResourcesServiceReference;
using Soat.HappyNet.Server.DataContract;
using Soat.HappyNet.Silverlight.Library;

namespace Soat.HappyNet.Silverlight.Modules.HumanResources.Services
{
    /// <summary>
    /// Interface defining the service of human resources
    /// </summary>
    public interface IHumanResourcesService
    {
        #region BeginFindAllEmployees Method

        /// <summary>
        /// Retrieves a listing of all employees
        /// </summary>
        /// <param name="pageNumber">Page number</param>
        /// <param name="pageSize">Number of employees per page</param>
        /// <param name="sortColumnName">Sorting column</param>
        /// <param name="sortDirection">Sort direction</param>
        /// <param name="FindAllEmployeesCompleted">Callback method</param>
        void BeginFindAllEmployees(int pageNumber, int pageSize, string sortColumnName, Soat.HappyNet.Server.DataContract.SortDirection sortDirection,
            ServiceCompleted<ObservableCollection<Employee>> FindAllEmployeesCompleted);

        #endregion
    }
}
