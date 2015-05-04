// -----------------------------------------------------------------------
// <copyright file="ViewModel.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Threading;
using Soat.HappyNet.Silverlight.Library.Extensions;

namespace Soat.HappyNet.Silverlight.Library
{
    public class ViewModel<TView> : ViewModel
    {
        public TView View { get; protected set; }

        public override void Clean()
        {
            base.Clean();

            if (View is FrameworkElement)
            {
                FrameworkElement element = View as FrameworkElement;
                element.RemoveFromParentPanel();
            }
        }
    }

    public class ViewModel : INotifyPropertyChanged, IDisposable, ICleanable
    {
        public ViewModel() { }

        public void Notify<TValue>(Expression<Func<TValue>> propertySelector)
        {
            if (PropertyChanged != null)
            {
                var memberExpression = propertySelector.Body as MemberExpression;
                if (memberExpression != null)
                {
                    NotifyPropertyChanged(memberExpression.Member.Name);
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Invoqué lorsque l'objet doit être effacé
        /// </summary>
        public void Dispose()
        {
            this.Clean();
        }

        /// <summary>
        /// Méthode surchargeable pour y inclure la logique de nettoyage (supprimer les event handlers par ex.)
        /// </summary>
        public virtual void Clean()
        {
        }

#if DEBUG
        /// <summary>
        /// Utile lorsqu'on veut s'assurer que les objets du ViewModel sont bien garbage collectés
        /// </summary>
        ~ViewModel()
        {
            string msg = string.Format("{0} ({1}) Finalized", this.GetType().Name, this.GetHashCode());
            System.Diagnostics.Debug.WriteLine(msg);
        }
#endif

        #endregion // IDisposable Members
    }
}
