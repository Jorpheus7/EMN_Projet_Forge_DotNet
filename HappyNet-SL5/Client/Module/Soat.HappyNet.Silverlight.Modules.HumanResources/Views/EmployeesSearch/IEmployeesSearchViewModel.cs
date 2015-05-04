// -----------------------------------------------------------------------
// <copyright file="IEmployeesSearchViewModel.cs" company="So@t">
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
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Presentation.Commands;
using Soat.HappyNet.Silverlight.Modules.HumanResources.Model;
using System.Collections.ObjectModel;
using Soat.HappyNet.Silverlight.Modules.HumanResources.Services;

namespace Soat.HappyNet.Silverlight.Modules.HumanResources.Views
{
    /// <summary>
    /// Interface defining the ViewModel for the EmployeesSearch View
    /// </summary>
    public interface IEmployeesSearchViewModel
    {
        /// <summary>
        /// View
        /// </summary>
        IEmployeesSearchView View { get; }

        /// <summary>
        /// Employees collection
        /// </summary>
        ObservableCollection<EmployeeModel> EmployeeCollection { get; }

        /// <summary>
        /// Loads the employee collection
        /// </summary>
        void LoadEmployeeCollection();
    }
}
