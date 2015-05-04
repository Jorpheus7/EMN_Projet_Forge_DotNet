// -----------------------------------------------------------------------
// <copyright file="ProductPhotoGalleryViewModel.cs" company="So@t">
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
using System.Collections.ObjectModel;

namespace Soat.HappyNet.Silverlight.Modules.Production.Views
{
    /// <summary>
    /// Class defining the ViewModel for the ProductPhotoGallery View
    /// </summary>
    public class ProductPhotoGalleryViewModel : ViewModel<IProductPhotoGalleryView>, IProductPhotoGalleryViewModel
    {
        #region Properties

        /// <summary>
        /// Photo gallery
        /// </summary>
        public ObservableCollection<ImageTools.Image> Photos { get; private set; }

        private string title;
        /// <summary>
        /// Popup title
        /// </summary>
        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    Notify(() => this.Title);
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
        public ProductPhotoGalleryViewModel(IProductPhotoGalleryView view,
              IEventAggregator eventAggregator,
              IUnityContainer container)
        {
            Photos = new ObservableCollection<ImageTools.Image>();

            this.container = container;
            this.eventAggregator = eventAggregator;

            this.View = view;
            this.View.Model = this;
        }

        #endregion

        /// <summary>
        /// Displays the popup
        /// </summary>
        public void Show()
        {
            this.View.Show();
        }
    }
}
