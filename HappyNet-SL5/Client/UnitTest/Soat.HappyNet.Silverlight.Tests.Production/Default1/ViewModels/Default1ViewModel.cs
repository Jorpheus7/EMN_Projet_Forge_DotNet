using System.ComponentModel;
using Soat.HappyNet.Silverlight.Tests.Production.Default1.Interfaces;

namespace Soat.HappyNet.Silverlight.Tests.Production.Default1.ViewModels
{
    /// <summary>
    /// Cette classe représente le Vue Model pour le Default1
    /// </summary>
    public class Default1ViewModel : INotifyPropertyChanged, IDefault1ViewModel
    {
        #region INotifyPropertyChanged Members

        /// <summary>
        /// Evènement lorsque qu'une propriété du ModelView est changée
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Evènement lorsque qu'une propriété du ModelView est changée
        /// </summary>
        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region View Property

        /// <summary>
        /// Vue à utliser 
        /// </summary>
        public IDefault1View View { get; private set; }

        #endregion
        
        #region Test Property

        /// <summary>
        /// Membre privée Test
        /// </summary>
        private string test;

        /// <summary>
        /// Propriété publique Test
        /// </summary>
        public string Test
        {
            get
            {
                return test;
            }
            set
            {
                if (this.test != value)
                {
                    this.test = value;
                    this.OnPropertyChanged("Test");
                }
            }
        }

        #endregion
        
        #region Constructor

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="view">Vue sur laquelle est implémentée le ViewModel</param>
        /// <param name="container">Container Unity pour instanciation des type d'objet</param>
        /// <param name="eventAggregator">Gestionnaire d'Evènements</param>
        public Default1ViewModel(IDefault1View view)
        {
            this.InitializeCommands();

            this.View = view;
            this.View.Model = this;

            this.Test = "this is a test!";
        }

        #endregion

        #region InitializeCommands Method

        /// <summary>
        /// Cette méthode permet l'initialisation des es
        /// </summary>
        public void InitializeCommands()
        {

        }

        #endregion
    }
}
