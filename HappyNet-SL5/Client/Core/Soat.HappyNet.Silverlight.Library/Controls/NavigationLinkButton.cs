// -----------------------------------------------------------------------
// <copyright file="NavigationLinkButton.cs" company="So@t">
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
using Soat.HappyNet.Silverlight.Library.Helpers;
using System.Windows.Browser;

namespace Soat.HappyNet.Silverlight.Library.Controls
{
    public class NavigationLinkButton : HyperlinkButton
    {
        #region NavigateUri

        public new Uri NavigateUri
        {
            get { return (Uri)GetValue(NavigateUriProperty); }
            set { SetValue(NavigateUriProperty, value); }
        }

        public new static readonly DependencyProperty NavigateUriProperty =
            DependencyProperty.Register("NavigateUri", typeof(Uri), typeof(NavigationLinkButton),
            new PropertyMetadata(new PropertyChangedCallback(OnNavigationUriChanged)));

        private static void OnNavigationUriChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((NavigationLinkButton)d).OnNavigationUriChanged(e);
        }

        protected virtual void OnNavigationUriChanged(DependencyPropertyChangedEventArgs e)
        {
        }

        #endregion

        #region NavArg0 (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public string NavArg0
        {
            get { return (string)GetValue(NavArg2Property); }
            set { SetValue(NavArg0Property, value); }
        }
        public static readonly DependencyProperty NavArg0Property =
            DependencyProperty.Register("NavArg0", typeof(string), typeof(NavigationLinkButton), null);

        #endregion

        #region NavArg1 (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public string NavArg1
        {
            get { return (string)GetValue(NavArg1Property); }
            set { SetValue(NavArg1Property, value); }
        }
        public static readonly DependencyProperty NavArg1Property =
            DependencyProperty.Register("NavArg1", typeof(string), typeof(NavigationLinkButton), null);

        #endregion

        #region NavArg2 (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public string NavArg2
        {
            get { return (string)GetValue(NavArg2Property); }
            set { SetValue(NavArg2Property, value); }
        }
        public static readonly DependencyProperty NavArg2Property =
            DependencyProperty.Register("NavArg2", typeof(string), typeof(NavigationLinkButton), null);

        #endregion

        public NavigationLinkButton()
        {
        }

        protected override void OnClick()
        {
            //base.OnClick();

            Navigate();
        }

        void Navigate()
        {
            if (Application.Current is INavigationApplication)
            {
                var app = Application.Current as INavigationApplication;

                string url = string.Format(NavigateUri.OriginalString, NavArg0, NavArg1, NavArg2);

                if (GeneralHelper.IsControlDown())
                {
                    string windowUrl = HtmlPage.Document.DocumentUri.AbsoluteUri;
                    if (!string.IsNullOrEmpty(HtmlPage.Document.DocumentUri.Fragment))
                    {
                        windowUrl = windowUrl.Replace(HtmlPage.Document.DocumentUri.Fragment, string.Empty);
                    }

                    HtmlPage.Window.Navigate(
                        new Uri(string.Format("{0}#{1}", windowUrl, url), UriKind.Absolute),
                        "_new");
                }
                else
                {
                    app.MainFrame.Navigate(new Uri(url, UriKind.RelativeOrAbsolute));
                }
            }
        }
    }
}
