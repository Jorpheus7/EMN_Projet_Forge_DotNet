// -----------------------------------------------------------------------
// <copyright file="ProductDetailView.xaml.cs" company="So@t">
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using ImageTools.IO;
using ImageTools.IO.Png;
using ImageTools.IO.Gif;

namespace Soat.HappyNet.Silverlight.Modules.Production.Views
{
    /// <summary>
    /// Class defining the ProductDetail View
    /// </summary>
    public partial class ProductDetailView : UserControl, IProductDetailView
    {
        #region Model property

        /// <summary>
        /// ViewModel attached to the View
        /// </summary>
        public IProductDetailViewModel Model
        {
            get { return this.DataContext as IProductDetailViewModel; }
            set { this.DataContext = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ProductDetailView()
        {
            InitializeComponent();

            Encoders.AddEncoder<PngEncoder>();
            Decoders.AddDecoder<GifDecoder>();
            Decoders.AddDecoder<PngDecoder>();
        }

        #endregion

        /// <summary>
        /// Sets the photo to the view
        /// </summary>
        /// <param name="img"></param>
        public void SetPhoto(ImageTools.Image img)
        {
            Dispatcher.BeginInvoke(() => {
                this.ProductImage.Source = img;
            });
        }
    }
}
