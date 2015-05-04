// -----------------------------------------------------------------------
// <copyright file="IProductItemView.cs" company="So@t">
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

namespace Soat.HappyNet.Silverlight.Modules.Production.Views
{
    /// <summary>
    /// Interface defining the ProductItem View
    /// </summary>
    public interface IProductItemView
    {
        /// <summary>
        /// ViewModel attached to the View
        /// </summary>
        IProductItemViewModel Model { get; set; }

        /// <summary>
        /// Sets the photo to the view
        /// </summary>
        /// <param name="img">Photo</param>
        void SetPhoto(ImageTools.Image img);
    }
}
