// -----------------------------------------------------------------------
// <copyright file="BoolValueVisibilityBehavior.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

// http://nroute.codeplex.com/

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
using System.ComponentModel;
using Soat.HappyNet.Silverlight.Library.Helpers;
using Soat.HappyNet.Silverlight.Library.Behaviors.Interactivity;
using System.Windows.Data;

namespace Soat.HappyNet.Silverlight.Library.Behaviors
{
    public class BoolValueVisibilityBehavior : BindableBehavior<FrameworkElement>
    {

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(bool), typeof(BoolValueVisibilityBehavior),
            new PropertyMetadata(false, new PropertyChangedCallback(OnValueChanged)));

#region Properties

        public bool Value
        {
            get { return Convert.ToBoolean(GetValue(ValueProperty)); }
            set { SetValue(ValueProperty, value); }
        }

        public Binding ValueBinding
        {
            get { return GetBinding(ValueProperty); }
            set { SetBinding<bool>(ValueProperty, value); }
        }

        public bool Negate { get; set; }

#endregion

#region Trigger Related

        protected override void OnAttached()
        {
            base.OnAttached();
            UpdateVisibility(Value);
        }

#endregion

#region Handlers

        static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BoolValueVisibilityBehavior)d).UpdateVisibility(Convert.ToBoolean(e.NewValue));
        }

        void UpdateVisibility(bool value)
        {
            // basic check
            if (this.AssociatedObject == null) return;
            var _visible = value;
            if (Negate) _visible = !_visible;
            this.AssociatedObject.Visibility = _visible ? Visibility.Visible : Visibility.Collapsed;
        }

#endregion

    }
}
