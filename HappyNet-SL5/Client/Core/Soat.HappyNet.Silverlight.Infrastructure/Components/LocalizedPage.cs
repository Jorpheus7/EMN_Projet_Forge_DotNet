// -----------------------------------------------------------------------
// <copyright file="LocalizedPage.cs" company="So@t">
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
using Soat.HappyNet.Silverlight.Infrastructure.Localization;
using System.ComponentModel;
using System.Windows.Navigation;

namespace Soat.HappyNet.Silverlight.Infrastructure.Components
{
    public abstract class LocalizedPage : Page
    {
        LocalizedStrings localization;

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public LocalizedPage()
        {
            InitializeLocalization();
        }

        /// <summary>
        /// Initializes localization
        /// </summary>
        void InitializeLocalization()
        {
            SetTitle();

            localization = Application.Current.Resources["LocalizedStrings"] as LocalizedStrings;
            localization.PropertyChanged += new PropertyChangedEventHandler(OnLocalizationChanged);
        }

        /// <summary>
        /// Handles a localization change
        /// </summary>
        void OnLocalizationChanged(object sender, PropertyChangedEventArgs e)
        {
            SetTitle();
        }

        /// <summary>
        /// Sets the current title of the page
        /// </summary>
        protected abstract void SetTitle();

        #endregion

        /// <summary>
        /// Cleans the page when leaving the page
        /// </summary>
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            localization.PropertyChanged -= new PropertyChangedEventHandler(OnLocalizationChanged);
        }
    }
}
