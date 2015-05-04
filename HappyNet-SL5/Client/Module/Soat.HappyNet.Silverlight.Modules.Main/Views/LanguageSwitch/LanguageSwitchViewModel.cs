// -----------------------------------------------------------------------
// <copyright file="LanguageSwitchViewModel.cs" company="So@t">
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
using Soat.HappyNet.Silverlight.Infrastructure;
using Soat.HappyNet.Silverlight.Infrastructure.Localization;
using System.Threading;
using System.Globalization;
using System.Collections.ObjectModel;
using Soat.HappyNet.Server.DataContract;
using Soat.HappyNet.Silverlight.Library;
using Soat.HappyNet.Silverlight.Library.Extensions;
using Soat.HappyNet.Silverlight.Infrastructure.Events;
using Soat.HappyNet.Silverlight.Infrastructure.Models;

namespace Soat.HappyNet.Silverlight.Modules.Main.Views
{
    /// <summary>
    /// Class defining the ViewModel for the LanguageSwitch View
    /// </summary>
    public class LanguageSwitchViewModel : ViewModel<ILanguageSwitchView>, ILanguageSwitchViewModel
    {
        private Language currentLanguage;
        /// <summary>
        /// Selected language
        /// </summary>
        public Language CurrentLanguage
        {
            get { return currentLanguage; }
            set
            {
                if (currentLanguage != value)
                {
                    currentLanguage = value;
                    Notify(() => this.CurrentLanguage);

                    SwitchLanguage(value);
                }
            }
        }

        /// <summary>
        /// List of available languages
        /// </summary>
        public ObservableCollection<Language> Languages { get; private set; }

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
        public LanguageSwitchViewModel(ILanguageSwitchView view, 
            IEventAggregator eventAggregator,
            IUnityContainer container)
        {
            this.container = container;
            this.eventAggregator = eventAggregator;

            this.InitLanguages();
            
            this.View = view;
            this.View.Model = this;
        } 

        #endregion

        #region Initialize Methods

        /// <summary>
        /// Initializes the list of languages
        /// </summary>
        void InitLanguages()
        {
            var fr = new Language { CultureID = "fr", Name = "FR", Description = "Version française", CountryCode = "FR" };
            var eng = new Language { CultureID = "en", Name = "EN", Description = "English version", CountryCode = "GB" };
            var us = new Language { CultureID = "en", Name = "US", Description = "US version", CountryCode = "US" };
            Languages = new ObservableCollection<Language>()
            {
                fr, 
                eng,
                us
            };

            string lang = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            CurrentLanguage = Languages.Where(l => l.CultureID == lang).FirstOrDefault();
            if (CurrentLanguage == null)
                CurrentLanguage = fr;
        }

        #endregion

        #region Changement language

        /// <summary>
        /// Changes the current language
        /// </summary>
        /// <param name="lang">New language to apply</param>
        void SwitchLanguage(Language lang)
        {
            // Retrieves the language instance
            container.RegisterInstance<Language>(lang);

            CurrentLanguage = lang;

            // Sets the new culture
            Thread currentthread = Thread.CurrentThread;
            currentthread.CurrentCulture = new CultureInfo(lang.CultureID);
            currentthread.CurrentUICulture = new CultureInfo(lang.CultureID);

            // Gets the xaml instance within the app resources and fires property changed
            LocalizedStrings strings = Application.Current.Resources["LocalizedStrings"] as LocalizedStrings;
            strings.FirePropertyChanged();

            // Notifies everyone that the localization has changed
            eventAggregator.Publish(new LocalizationChangedEvent(lang));
        }

        #endregion
    }
}
