// -----------------------------------------------------------------------
// <copyright file="EmployeesSearchViewModel.cs" company="So@t">
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
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Presentation.Commands;
using System.Collections.ObjectModel;
using Soat.HappyNet.Silverlight.Modules.HumanResources.Model;
using Soat.HappyNet.Silverlight.Infrastructure.Events;
using Soat.HappyNet.Silverlight.Modules.HumanResources.Services;
using Soat.HappyNet.Server.DataContract;
using Soat.HappyNet.Silverlight.Library.Extensions;
using Soat.HappyNet.Silverlight.Library;

namespace Soat.HappyNet.Silverlight.Modules.HumanResources.Views
{
    /// <summary>
    /// Class defining the ViewModel for the EmployeesSearch View
    /// </summary>
    public class EmployeesSearchViewModel : ViewModel<IEmployeesSearchView>, IEmployeesSearchViewModel
    {
        #region Properties

        /// <summary>
        /// Employees collection
        /// </summary>
        public ObservableCollection<EmployeeModel> EmployeeCollection { get; private set; }

        bool isLoading = false;
        /// <summary>
        /// Loading indicator
        /// </summary>
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                if (isLoading != value)
                {
                    isLoading = value;
                    Notify(() => this.IsLoading);
                }
            }
        }

        #endregion

        #region Private members

        /// <summary>
        /// Event aggregator
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        /// <summary>
        /// Service for human resources
        /// </summary>
        private readonly IHumanResourcesService HumanResourcesService; 

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="view">View attached to this ViewModel</param>
        /// <param name="eventAggregator">Event aggregator</param>
        public EmployeesSearchViewModel(IHumanResourcesService humanResourcesService,IEmployeesSearchView view, IEventAggregator eventAggregator)
        {
            this.EmployeeCollection = new ObservableCollection<EmployeeModel>();

            this.HumanResourcesService = humanResourcesService;
            this.eventAggregator = eventAggregator;

            this.View = view;
            this.View.Model = this;

            this.LoadEmployeeCollection();

            eventAggregator.Subscribe<LoginChangedEvent>(OnLoginChanged);
        }

        #endregion

        #region Employees

        /// <summary>
        /// Loads the employee collection
        /// </summary>
        public void LoadEmployeeCollection()
        {
            IsLoading = true;
            this.HumanResourcesService.BeginFindAllEmployees(0, 10, "FirstName", SortDirection.Ascending, FindAllEmployeesCompleted);
        }

        /// <summary>
        /// Treats the return from the GetAllEmployees call
        /// </summary>
        /// <param name="args">Argument du retour</param>
        public void FindAllEmployeesCompleted(ServiceArgs<ObservableCollection<Employee>> args)
        {
            if (args.Error == null)
            {
                this.EmployeeCollection.Clear();
                foreach (var e in args.Result)
                {
                    this.EmployeeCollection.Add(new EmployeeModel()
                    {
                        Identifier = e.Identifier,
                        Title = e.Title,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        MidlleName = e.MiddleName,
                        Suffix = e.suffix,
                        JobTitle = e.JobTitle,
                        PhoneNumber = e.PhoneNumber,
                        PhoneNumberType = e.PhoneNumberType,
                        EmailAddress = e.EmailAddress,
                        EmailPromotion = e.EmailAddress,
                        AddressLine1 = e.AddressLine1,
                        AddressLine2 = e.AddressLine2,
                        City = e.City,
                        StateProvinceName = e.StateProvinceName,
                        PostalCode = e.PostalCode,
                        CountryRegionName = e.CountryRegionName,
                        AdditionalContactInfo = e.AdditionalContactInfo
                    });
                }
            }
            else
            {
                this.eventAggregator.Publish(new MessageShowEvent
                {
                    MessageTitle = "Oops ...",
                    Message = args.Error.Message
                });
            }

            IsLoading = false;
        }

        public void OnLoginChanged(LoginChangedEvent args)
        {
            if (args.IsLogged)
            {
                this.LoadEmployeeCollection();
            }
        }

        #endregion

        #region ICleanable

        public override void Clean()
        {
            EmployeeCollection.Clear();

            base.Clean();
        }

        #endregion
    }
}
