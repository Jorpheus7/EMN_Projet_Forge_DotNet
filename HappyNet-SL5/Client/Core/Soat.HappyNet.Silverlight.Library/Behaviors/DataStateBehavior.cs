// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.

// http://expressionblend.codeplex.com/

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;
using Soat.HappyNet.Silverlight.Library.Helpers;
using Soat.HappyNet.Silverlight.Library.Behaviors.Actions;

namespace Soat.HappyNet.Silverlight.Library.Behaviors
{

	/// <summary>
	/// Behavior that activates two visual states based on the condition of a binding.
	/// </summary>
	public class DataStateBehavior : Behavior<FrameworkElement> {

		/// <summary>Backing DP for the Binding property</summary>
		public static readonly DependencyProperty BindingProperty = DependencyProperty.Register("Binding", typeof(Binding), typeof(DataStateBehavior), new PropertyMetadata(null, DataStateBehavior.HandleBindingChanged));
		/// <summary>Backing DP for the Value property</summary>
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(DataStateBehavior), new PropertyMetadata(null, DataStateBehavior.HandleValueChanged));
		/// <summary>Backing DP for the TrueState property</summary>
		public static readonly DependencyProperty TrueStateProperty = DependencyProperty.Register("TrueState", typeof(string), typeof(DataStateBehavior), new PropertyMetadata(null));
		/// <summary>Backing DP for the FalseState property</summary>
		public static readonly DependencyProperty FalseStateProperty = DependencyProperty.Register("FalseState", typeof(string), typeof(DataStateBehavior), new PropertyMetadata(null));

		private BindingListener listener;

		/// <summary>
		/// Constructor
		/// </summary>
		public DataStateBehavior() {
			this.listener = new BindingListener(this.HandleBindingValueChanged);
		}

		/// <summary>
		/// The binding to be used to evaluate the state.
		/// </summary>
		public Binding Binding {
			get { return (Binding)this.GetValue(DataStateBehavior.BindingProperty); }
			set { this.SetValue(DataStateBehavior.BindingProperty, value); }
		}

		/// <summary>
		/// The value to which the binding is compared against.
		/// </summary>
		public object Value {
			get { return (object)this.GetValue(DataStateBehavior.ValueProperty); }
			set { this.SetValue(DataStateBehavior.ValueProperty, value); }
		}

		/// <summary>
		/// The name of the VisualState tobe activated when true.
		/// </summary>
		public string FalseState {
			get { return (string)this.GetValue(DataStateBehavior.FalseStateProperty); }
			set { this.SetValue(DataStateBehavior.FalseStateProperty, value); }
		}

		/// <summary>
		/// The name of the VisualState tobe activated when false.
		/// </summary>
		public string TrueState {
			get { return (string)this.GetValue(DataStateBehavior.TrueStateProperty); }
			set { this.SetValue(DataStateBehavior.TrueStateProperty, value); }
		}

		private static void HandleBindingChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
			((DataStateBehavior)sender).OnBindingChanged(e);
		}

		/// <summary>
		/// Notification that the Binding property has changed.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnBindingChanged(DependencyPropertyChangedEventArgs e) {
			this.listener.Binding = (Binding)e.NewValue;
		}

		private static void HandleValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
			((DataStateBehavior)sender).OnValueChanged(e);
		}

		/// <summary>
		/// Notification that the Value property has changed.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnValueChanged(DependencyPropertyChangedEventArgs e) {
			this.CheckState();
		}

		/// <summary>
		/// Implementation of OnAttached.
		/// </summary>
		protected override void OnAttached() {
			base.OnAttached();

			this.listener.Element = this.AssociatedObject;
		}

		/// <summary>
		/// Implementation of OnDetaching.
		/// </summary>
		protected override void OnDetaching() {
			base.OnDetaching();

			this.listener.Element = null;
		}

		private void HandleBindingValueChanged(object sender, BindingChangedEventArgs e) {
			this.CheckState();
		}

		private void CheckState() {

			if (this.Value == null || this.listener.Value == null) {
				this.IsTrue = object.Equals(this.listener.Value, this.Value);
			}
			else {
				object convertedValue = ConverterHelper.ConvertToType(this.Value, this.listener.Value.GetType());
				this.IsTrue = object.Equals(this.listener.Value, ConverterHelper.ConvertToType(this.Value, this.listener.Value.GetType()));
			}
		}

		private bool? isTrue;
		private bool? IsTrue {
			get { return this.isTrue; }
			set {
				if (this.isTrue != value) {
					this.isTrue = value;

					if (this.IsTrue.HasValue) {
						FrameworkElement targetElement = GoToStateBase.FindTargetElement(this.AssociatedObject);
						if (targetElement == null)
							this.IsTrue = null;
						else if (this.IsTrue.Value)
							GoToStateBase.GoToState(targetElement, this.TrueState, true);
						else
							GoToStateBase.GoToState(targetElement, this.FalseState, true);
					}
				}
			}
		}

		
	}
}
