// -----------------------------------------------------------------------
// <copyright file="HumanResourcesService.cs" company="So@t">
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
using System.Linq;
using Microsoft.Practices.Composite.Events;
using Soat.HappyNet.Silverlight.Infrastructure;
using Soat.HappyNet.Silverlight.Modules.HumanResources.HumanResourcesServiceReference;
using Soat.HappyNet.Server.DataContract;
using Soat.HappyNet.Silverlight.Infrastructure.Events;
using Soat.HappyNet.Silverlight.Library.Extensions;
using Soat.HappyNet.Silverlight.Infrastructure.Helpers;
using Soat.HappyNet.Silverlight.Library;

namespace Soat.HappyNet.Silverlight.Modules.HumanResources.Services
{
    /// <summary>
    /// Class defining the service of human resources
    /// </summary>
    public class HumanResourcesService : IHumanResourcesService
    {
        #region Const

        /// <summary>
        /// Configuration name for the client proxy (cf. ServiceReferences.ClientConfig in the Shell Project)
        /// </summary>
        const string ServiceConfigurationName = "HumanResourcesServiceConfiguration";

        #endregion
        
        #region Private members

        /// <summary>
        /// Event aggregator
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        /// <summary>
        /// Web services proxy
        /// </summary>
        private readonly HumanResourcesServiceClient client;

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="eventAggregator">Event aggregator</param>
        public HumanResourcesService(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            this.client = new HumanResourcesServiceClient(HumanResourcesService.ServiceConfigurationName);
            client.FindAllEmployeesCompleted += new EventHandler<FindAllEmployeesCompletedEventArgs>(this.OnFindAllEmployeesCompleted);
        }

        #region FindAllEmployees

        /// <summary>
        /// Retrieves a listing of all employees
        /// </summary>
        /// <param name="pageNumber">Page number</param>
        /// <param name="pageSize">Number of employees per page</param>
        /// <param name="sortColumnName">Sorting column</param>
        /// <param name="sortDirection">Sort direction</param>
        /// <param name="FindAllEmployeesCompleted">Callback method</param>
        public void BeginFindAllEmployees(int pageNumber, int pageSize,string sortColumnName, SortDirection sortDirection,
            ServiceCompleted<ObservableCollection<Employee>> FindAllEmployeesCompleted)
        {
            // Inject credentials within the header of SOAP messages
            OperationContextHelper.Create(client);

            client.FindAllEmployeesAsync(pageNumber, pageSize, sortColumnName, sortDirection,
                new ServiceArgs<ObservableCollection<Employee>>(FindAllEmployeesCompleted));
        }

        public void OnFindAllEmployeesCompleted(object sender, FindAllEmployeesCompletedEventArgs e)
        {
            ObservableCollection<Employee> result = null;
 
            if (e.Error == null)
            {
                result = e.Result;
            }

            var args = e.UserState as ServiceArgs<ObservableCollection<Employee>>;
            args.Complete(result, e);
        }

        #endregion
  
    }
}
