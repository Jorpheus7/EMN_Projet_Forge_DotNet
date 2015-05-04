// -----------------------------------------------------------------------
// <copyright file="IProductDetailViewModel.cs" company="So@t">
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
    /// Interface defining the ViewModel for the ProductDetail View
    /// </summary>
    public interface IProductDetailViewModel
    {
        /// <summary>
        /// View
        /// </summary>
        IProductDetailView View { get; }

        /// <summary>
        /// Current product model
        /// </summary>
        ProductModel ProductModel { get; }

        /// <summary>
        /// Products list
        /// </summary>
        ObservableCollection<Product> Products { get; }

        /// <summary>
        /// Name of the selected product
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Price
        /// </summary>
        string Price { get; }

        /// <summary>
        /// Description
        /// </summary>
        string Description { get; }

        /// <summary>
        /// True if there are several products attached to the product model
        /// </summary>
        bool IsMultiProducts { get; }

        /// <summary>
        /// Indicator to know if the product detail has been loaded
        /// </summary>
        bool IsProductLoaded { get; }

        /// <summary>
        /// Selected product
        /// </summary>
        Product CurrentProduct { get; }

        /// <summary>
        /// Command to add the selected product in the shopping cart
        /// </summary>
        ICommand AddToCardCommand { get; }

        /// <summary>
        /// Command to display a photo gallery
        /// </summary>
        ICommand ShowPhotoGalleryCommand { get; }
    }
}
