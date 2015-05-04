// -----------------------------------------------------------------------
// <copyright file="WindowContentControl.cs" company="So@t">
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

namespace Soat.HappyNet.Silverlight.Library.Controls
{
    /// <summary>
    /// Cette classe représente un control d'affichage d'une fenetre contenant un titre
    /// </summary>
    public class WindowContentControl : ContentControl
    {
        #region Title Dependency Property

        /// <summary>
        /// Titre de la fenêtre
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// Titre de la fenêtre
        /// // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(WindowContentControl), new PropertyMetadata(""));

        #endregion


        #region HeaderContent (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public object HeaderContent
        {
            get { return (object)GetValue(HeaderContentProperty); }
            set { SetValue(HeaderContentProperty, value); }
        }
        public static readonly DependencyProperty HeaderContentProperty =
            DependencyProperty.Register("HeaderContent", typeof(object), typeof(WindowContentControl), null);

        #endregion


    }
}
