// -----------------------------------------------------------------------
// <copyright file="KonamiCodeControl.cs" company="So@t">
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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Soat.HappyNet.Silverlight.Library.Controls
{
    [TemplateVisualState(Name = "Active", GroupName = "CodeEntry")]
    [TemplateVisualState(Name = "Inactive", GroupName = "CodeEntry")]
    public class KonamiCodeControl : ContentControl
    {
        private int indexSoFar;
        public KonamiCodeControl()
        {
            DefaultStyleKey = typeof(KonamiCodeControl);
            this.KeyUp += new KeyEventHandler(KonamiCodeControl_KeyUp);
            this.KeyDown += new KeyEventHandler(KonamiCodeControl_KeyDown);
            indexSoFar = 0;
        }

        private void KonamiCodeControl_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void KonamiCodeControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (KeyStrokes[indexSoFar] == e.Key)
            {
                indexSoFar++;
                if (indexSoFar >= KeyStrokes.Length)
                {
                    indexSoFar = 0;
                    OnKeyStrokesEntered();
                    Reset();
                    VisualStateManager.GoToState(this, "Active", true);
                }
            }
            else
            {
                Reset();
            }
        }

        protected void OnKeyStrokesEntered()
        {
            if (KeyStrokesEntered != null)
                KeyStrokesEntered(this, new EventArgs());
        }

        public event EventHandler KeyStrokesEntered;

        [TypeConverter(typeof(KeyListTypeConverter))]
        public Key[] KeyStrokes
        {
            get { return (Key[])GetValue(KeyStrokesProperty); }
            set { SetValue(KeyStrokesProperty, value); }
        }

        public void Reset()
        {
            VisualStateManager.GoToState(this, "Inactive", true);
            indexSoFar = 0;
        }

        // Using a DependencyProperty as the backing store for KeyStrokes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KeyStrokesProperty =
            DependencyProperty.Register("KeyStrokes", typeof(Key[]), typeof(KonamiCodeControl), new PropertyMetadata(null, KeyStrokesChanged));

        private static void KeyStrokesChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((KonamiCodeControl)obj).Reset();
        }
    }
}
