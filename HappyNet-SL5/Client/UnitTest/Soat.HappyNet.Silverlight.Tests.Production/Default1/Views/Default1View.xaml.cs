using System.Windows.Controls;
using Soat.HappyNet.Silverlight.Tests.Production.Default1.Interfaces;

namespace Soat.HappyNet.Silverlight.Tests.Production.Default1.Views
{
    /// <summary>
    /// Cette classe représente l'affichage du Default1
    /// </summary>
    public partial class Default1View : UserControl, IDefault1View
    {
        #region Model property

        /// <summary>
        /// Model presentation attaché à la vue
        /// </summary>
        public IDefault1ViewModel Model
        {
            get { return this.DataContext as IDefault1ViewModel; }
            set { this.DataContext = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public Default1View()
        {
            InitializeComponent();
        }

        #endregion
    }
}
