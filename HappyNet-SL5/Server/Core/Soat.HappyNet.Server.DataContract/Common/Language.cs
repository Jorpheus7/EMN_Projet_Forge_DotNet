using System;
using System.Net;
using System.ComponentModel;

namespace Soat.HappyNet.Server.DataContract
{
    public class Language : INotifyPropertyChanged
    {
        private string name;
        /// <summary>
        /// Nom d'affichage pour le langage
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
        /// Description du langage (ex.: "version française")
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
        /// Nom ISO du langage
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
