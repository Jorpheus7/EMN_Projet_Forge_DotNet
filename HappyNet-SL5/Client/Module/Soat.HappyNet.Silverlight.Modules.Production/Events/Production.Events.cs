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
using Soat.HappyNet.Silverlight.Modules.Production.ProductionServiceReference;

namespace Soat.HappyNet.Silverlight.Infrastructure.Events
{
    public class CategorySelectedEvent : CompositePresentationEvent<CategorySelectedEvent>
    {
        public ProductCategory SelectedCategory { get; private set; }
        public ProductSubcategory SelectedSubCategory { get; private set; }

        public CategorySelectedEvent()
        {
        }

        public CategorySelectedEvent(ProductSubcategory subcategory)
        {
            this.SelectedSubCategory = subcategory;
        }

        public CategorySelectedEvent(ProductCategory category)
        {
            this.SelectedCategory = category;
        }
    }
}
