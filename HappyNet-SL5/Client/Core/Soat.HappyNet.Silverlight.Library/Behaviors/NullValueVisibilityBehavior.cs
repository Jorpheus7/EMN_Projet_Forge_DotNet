// -----------------------------------------------------------------------
// <copyright file="NullValueVisibilityBehavior.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

// http://nroute.codeplex.com/

using System;
using System.Windows;
using System.ComponentModel;
using Soat.HappyNet.Silverlight.Library.Helpers;
using Soat.HappyNet.Silverlight.Library.Behaviors.Interactivity;
using System.Windows.Data;

namespace Soat.HappyNet.Silverlight.Library.Behaviors
{
    public class NullValueVisibilityBehavior : BindableBehavior<FrameworkElement>
    {

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), typeof(NullValueVisibilityBehavior),
            new PropertyMetadata(null, new PropertyChangedCallback(OnValueChanged)));

#region Properties

        [TypeConverter(typeof(ConvertFromStringConverter))]
        public Object Value
        {
            get { return GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public Binding ValueBinding
        {
            get { return GetBinding(ValueProperty); }
            set { SetBinding<object>(ValueProperty, value); }
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
            ((NullValueVisibilityBehavior)d).UpdateVisibility(e.NewValue);
        }

        void UpdateVisibility(object value)
        {
            // basic check
            if (this.AssociatedObject == null) return;
            var _visible = (value != null);
            if (Negate) _visible = !_visible;
            this.AssociatedObject.Visibility = _visible ? Visibility.Visible : Visibility.Collapsed;
        }

#endregion

    }
}
