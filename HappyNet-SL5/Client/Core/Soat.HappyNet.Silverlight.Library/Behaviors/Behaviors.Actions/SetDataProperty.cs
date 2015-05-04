// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.
// http://expressionblend.codeplex.com/

using System;
using System.Windows;
using Soat.HappyNet.Silverlight.Library.Behaviors.Interactivity;
using System.Windows.Data;
using System.ComponentModel;
using System.Windows.Markup;
using Soat.HappyNet.Silverlight.Library.Helpers;
using System.Windows.Interactivity;
using Soat.HappyNet.Silverlight.Library.Behaviors;

namespace Soat.HappyNet.Silverlight.Library.Behaviors.Actions
{
    public class SetDataProperty : TriggerAction<FrameworkElement>
    {
        /// <summary>Backing DP for Binding property</summary>
		public static readonly DependencyProperty BindingProperty = DependencyProperty.Register("Binding", typeof(Binding), typeof(SetDataProperty), new PropertyMetadata(SetDataProperty.HandleBindingChanged));
		/// <summary>Backing DP for Value property</summary>
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(SetDataProperty), new PropertyMetadata(null));
		/// <summary>Backing DP for Increment property</summary>
		public static readonly DependencyProperty IncrementProperty = DependencyProperty.Register("Increment", typeof(bool), typeof(SetDataProperty), new PropertyMetadata(false));

		private BindingListener listener;

		/// <summary>
		/// Constructor
		/// </summary>
		public SetDataProperty() {
			this.listener = new BindingListener(this.HandleBindingValueChanged);
		}

		/// <summary>
		/// Binding to the property to be set. Must be a two-way binding.
		/// </summary>
		public Binding Binding {
			get { return (Binding)this.GetValue(SetDataProperty.BindingProperty); }
			set { this.SetValue(SetDataProperty.BindingProperty, value); }
		}

		/// <summary>
		/// True if the property should be incremented rather than set.
		/// </summary>
		public bool Increment {
			get { return (bool)this.GetValue(SetDataProperty.IncrementProperty); }
			set { this.SetValue(SetDataProperty.IncrementProperty, value); }
		}

		/// <summary>
		/// The value to be set, or to be incremented by.
		/// </summary>
		public object Value {
			get { return (object)this.GetValue(SetDataProperty.ValueProperty); }
			set { this.SetValue(SetDataProperty.ValueProperty, value); }
		}

		/// <summary>
		/// Perform initialization.
		/// </summary>
		protected override void OnAttached() {
			base.OnAttached();

			this.listener.Element = this.AssociatedObject;
		}

		/// <summary>
		/// Perform cleanup.
		/// </summary>
		protected override void OnDetaching() {
			base.OnDetaching();

			this.listener.Element = null;
		}

		/// <summary>
		/// Invokes this action.
		/// </summary>
		/// <param name="parameter"></param>
		protected override void Invoke(object parameter) {
			object currentValue = this.listener.Value;
			if (currentValue != null) {

				object newValue = ConverterHelper.ConvertToType(this.Value, currentValue.GetType());
				if (this.Increment)
					newValue = FluidBindProperty.Add(currentValue, newValue);

				this.listener.Value = newValue;
			}

			else
				this.listener.Value = this.Value;
		}

		private void HandleBindingValueChanged(object sender, BindingChangedEventArgs e) {

		}

		private static void HandleBindingChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
			((SetDataProperty)sender).OnBindingChanged(e);
		}

		/// <summary>
		/// Notification that the binding has changed.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnBindingChanged(DependencyPropertyChangedEventArgs e) {
			this.listener.Binding = this.Binding;
		}

    }
}
