// -----------------------------------------------------------------------
// <copyright file="IProductsNavigationViewModel.cs" company="So@t">
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
using Soat.HappyNet.Silverlight.Modules.Production.ProductionServiceReference;
using System.Collections.ObjectModel;

namespace Soat.HappyNet.Silverlight.Modules.Production.Views
{
    /// <summary>
    /// Interface defining the ViewModel for the ProductsNavigation View
    /// </summary>
    public interface IProductsNavigationViewModel
    {
        /// <summary>
        /// View
        /// </summary>
        IProductsNavigationView View { get; }

        /// <summary>
        /// Current category
        /// </summary>
        ProductCategory CurrentCategory { get; set; }

        /// <summary>
        /// Current sub-category
        /// </summary>
        ProductSubcategory CurrentSubCategory { get; set; }

        /// <summary>
        /// True if loading failed ... :(
        /// </summary>
        bool IsError { get; }

        /// <summary>
        /// Loading indicator
        /// </summary>
        bool IsLoading { get; }

        /// <summary>
        /// Categories
        /// </summary>
        ObservableCollection<ProductCategory> Categories { get; }

        /// <summary>
        /// Command for category or sub-category selection
        /// </summary>
        ICommand CategorySelectionCommand { get; }
    }
}
