// -----------------------------------------------------------------------
// <copyright file="SalesOrder.Events.cs" company="So@t">
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
    /// Event to add a product to the shopping cart
    /// </summary>
    public class AddToCartEvent : CompositePresentationEvent<AddToCartEvent>
    {
        /// <summary>
        /// Product id
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Cost
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Quantity of products to add to the shopping cart
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public AddToCartEvent()
        {
            this.Quantity = 1;
        }

        /// <summary>
        /// Constructor (quantity = 1)
        /// </summary>
        /// <param name="productID">Product id</param>
        /// <param name="productName">Product name</param>
        public AddToCartEvent(int productID, string productName)
            :this(productID, productName, 1)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="productID">Product id</param>
        /// <param name="productName">Product name</param>
        /// <param name="quantity">Quantity</param>
        public AddToCartEvent(int productID, string productName, int quantity)
        {
            this.ProductID = productID;
            this.ProductName = productName;
            this.Quantity = quantity;
        }
    }
}
