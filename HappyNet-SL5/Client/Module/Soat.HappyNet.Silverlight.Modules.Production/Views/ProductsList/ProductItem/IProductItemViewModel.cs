// -----------------------------------------------------------------------
// <copyright file="IProductItemViewModel.cs" company="So@t">
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
using System.Windows.Media.Imaging;

namespace Soat.HappyNet.Silverlight.Modules.Production.Views
{
    /// <summary>
    /// Interface defining the ViewModel for the ProductItem View
    /// </summary>
    public interface IProductItemViewModel
    {
        /// <summary>
        /// View
        /// </summary>
        IProductItemView View { get; }

        /// <summary>
        /// Initialize the current product
        /// </summary>
        /// <param name="productModel">Product model</param>
        void SetProduct(ProductModel productModel);

        /// <summary>
        /// Loads the product photo
        /// </summary>
        void LoadPhoto();

        /// <summary>
        /// Current product model
        /// </summary>
        ProductModel ProductModel { get; }

        /// <summary>
        /// Photo of the product
        /// </summary>
        ImageTools.Image ThumbNailPhoto { get; }

        /// <summary>
        /// Name of the product
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Price
        /// </summary>
        string Price { get; }

        /// <summary>
        /// Command to select a product
        /// </summary>
        ICommand SelectProductCommand { get; }
    }
}
