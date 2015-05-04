// -----------------------------------------------------------------------
// <copyright file="BusyPointer.cs" company="So@t">
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
    public class BusyPointer : Control
    {
        #region Dependency Properties

        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set
            {
                SetValue(IsBusyProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for IsBusy.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register("IsBusy", typeof(bool), typeof(BusyPointer), new PropertyMetadata(new PropertyChangedCallback(OnBusyChanged)));

        private static void OnBusyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((BusyPointer)sender).OnBusyChanged(e);
        }

        protected virtual void OnBusyChanged(DependencyPropertyChangedEventArgs e)
        {
            ChangeState();
        }

        #endregion

        #region Initialization

        public BusyPointer()
        {
            this.DefaultStyleKey = typeof(BusyPointer);
        }

        #endregion

        #region Methods

        private void ChangeState()
        {
            if (IsBusy) VisualStateManager.GoToState(this, "Busied", true);
            else VisualStateManager.GoToState(this, "Normal", true);
        }

        #endregion
    }
}
