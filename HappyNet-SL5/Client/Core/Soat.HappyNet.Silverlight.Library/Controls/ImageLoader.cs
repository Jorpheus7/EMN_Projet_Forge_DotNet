// -----------------------------------------------------------------------
// <copyright file="ImageLoader.cs" company="So@t">
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
using System.Windows.Media.Imaging;

namespace Soat.HappyNet.Silverlight.Library.Controls
{
    public class ImageLoader : Control
    {
        #region Source (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(ImageLoader),
            new PropertyMetadata(new PropertyChangedCallback(OnSourceChanged)));

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ImageLoader)d).OnSourceChanged();
        }

        protected virtual void OnSourceChanged()
        {
            if (Source is BitmapImage 
                && ImageControl != null)
            {
                var bitmap = Source as BitmapImage;
                bitmap.DownloadProgress += new EventHandler<DownloadProgressEventArgs>(value_DownloadProgress);
                bitmap.ImageFailed += new EventHandler<ExceptionRoutedEventArgs>(OnBitmapImageFailed);

                this.BeginAnimation();
                this.ImageControl.Source = Source;
            }
        }

        #endregion

        #region IsLoading (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }
        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register("IsLoading", typeof(bool), typeof(ImageLoader),
            new PropertyMetadata(false, new PropertyChangedCallback(OnIsLoadingChanged)));

        private static void OnIsLoadingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ImageLoader)d).OnIsLoadingChanged(e);
        }

        protected virtual void OnIsLoadingChanged(DependencyPropertyChangedEventArgs e)
        {
            if (IsLoading)
            {
                VisualStateManager.GoToState(this, "Loading", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "Normal", true);
            }
        }

        #endregion

        Image ImageControl;

        public ImageLoader()
        {
            DefaultStyleKey = typeof(ImageLoader);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ImageControl = GetTemplateChild("ImageControl") as Image;

            OnSourceChanged();
        }

        void value_DownloadProgress(object sender, DownloadProgressEventArgs e)
        {
            if (e.Progress == 100)
            {
                StopAnimation();
            }
        }

        void OnBitmapImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            StopAnimation();
            VisualStateManager.GoToState(this, "Failed", true);
        }
 
        private void BeginAnimation()
        {
            IsLoading = true;
        }
 
        private void StopAnimation()
        {
            IsLoading = false;
        }
    }
}