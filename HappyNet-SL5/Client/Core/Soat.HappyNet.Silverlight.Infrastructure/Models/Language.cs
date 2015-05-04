// -----------------------------------------------------------------------
// <copyright file="Language.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Net;
using System.ComponentModel;

namespace Soat.HappyNet.Silverlight.Infrastructure.Models
{
    /// <summary>
    /// Class representing a language
    /// </summary>
    public class Language : INotifyPropertyChanged
    {
        private string name;
        /// <summary>
        /// Display name for the language
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private string description;
        /// <summary>
        /// Language description (ex.: "english version", "version française")
        /// </summary>
        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        private string cultureID;
        /// <summary>
        /// ISO name of the language
        /// </summary>
        public string CultureID
        {
            get { return cultureID; }
            set
            {
                if (cultureID != value)
                {
                    cultureID = value;
                    OnPropertyChanged("CultureID");
                }
            }
        }

        private string countryCode;
        /// <summary>
        /// Country code for this language
        /// </summary>
        public string CountryCode
        {
            get { return countryCode; }
            set
            {
                if (countryCode != value)
                {
                    countryCode = value;
                    OnPropertyChanged("CountryCode");
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
