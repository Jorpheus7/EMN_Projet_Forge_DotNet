// -----------------------------------------------------------------------
// <copyright file="ProductsSearchViewModel.cs" company="So@t">
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
using Microsoft.Practices.Unity;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Presentation.Commands;
using Soat.HappyNet.Silverlight.Library;
using Soat.HappyNet.Silverlight.Library.Extensions;
using Soat.HappyNet.Silverlight.Infrastructure.Events;
using Soat.HappyNet.Silverlight.Modules.Production.ProductionServiceReference;
using System.Collections.ObjectModel;

using IProductionService = Soat.HappyNet.Silverlight.Modules.Production.Services.IProductionService;

namespace Soat.HappyNet.Silverlight.Modules.Production.Views
{
    /// <summary>
    /// Class defining the ViewModel for the ProductsSearch View
    /// </summary>
    public class ProductsSearchViewModel : ViewModel<IProductsSearchView>, IProductsSearchViewModel
    {
        private bool isLoading;
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

        private string search;
        /// <summary>
        /// Search string
        /// </summary>
        public string Search
        {
            get { return search; }
            set
            {
                if (search == value)
                    return;

                search = value;
                Notify(() => this.Search);
                
                // Notify for the search string as we're typing in
                RaiseSearchEvent(Search);
            }
        }

        #region Private members
        
        /// <summary>
        /// Event aggregator
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        /// <summary>
        /// Unity container
        /// </summary>
        private readonly IUnityContainer container;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="view">View attached to this ViewModel</param>
        /// <param name="eventAggregator">Event aggregator</param>
        /// <param name="container">Unity container</param>
        public ProductsSearchViewModel(IProductsSearchView view,
            IEventAggregator eventAggregator,
            IUnityContainer container)
        {
            this.container = container;
            this.eventAggregator = eventAggregator;

            this.View = view;
            this.View.Model = this;
        }

        #endregion

        /// <summary>
        /// Notify that a search string has been entered
        /// </summary>
        /// <param name="search"></param>
        void RaiseSearchEvent(string search)
        {
            eventAggregator.Publish(new SearchProductsEvent(search));
        }
    }
}
