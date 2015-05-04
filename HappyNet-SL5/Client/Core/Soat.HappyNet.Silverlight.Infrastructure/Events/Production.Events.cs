// -----------------------------------------------------------------------
// <copyright file="Production.Events.cs" company="So@t">
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
using Microsoft.Practices.Composite.Presentation.Events;

namespace Soat.HappyNet.Silverlight.Infrastructure.Events
{
    /// <summary>
    /// Event to search for a product
    /// </summary>
    public class SearchProductsEvent : CompositePresentationEvent<SearchProductsEvent>
    {
        /// <summary>
        /// Search string
        /// </summary>
        public string SearchString { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public SearchProductsEvent()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="searchString">Search string</param>
        public SearchProductsEvent(string searchString)
        {
            this.SearchString = searchString;
        }
    }

    /// <summary>
    /// Event to select a product
    /// </summary>
    public class ProductSelectedEvent : CompositePresentationEvent<ProductSelectedEvent>
    {
        /// <summary>
        /// Product model id
        /// </summary>
        public int ProductModelID { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public ProductSelectedEvent()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="productModelID">Product model id</param>
        public ProductSelectedEvent(int productModelID)
        {
            this.ProductModelID = productModelID;
        }
    }
}
