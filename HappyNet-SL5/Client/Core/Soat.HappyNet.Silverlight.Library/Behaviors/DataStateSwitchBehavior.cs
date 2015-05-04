// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.

// http://expressionblend.codeplex.com/

using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Interactivity;
using Soat.HappyNet.Silverlight.Library.Helpers;
using Soat.HappyNet.Silverlight.Library.Behaviors.Actions;

namespace Soat.HappyNet.Silverlight.Library.Behaviors
{
	/// <summary>
	/// Switches between multiple visual states depending on a condition.
	/// </summary>
	[ContentProperty("States")]
	public class DataStateSwitchBehavior : Behavior<FrameworkElement> {

		/// <summary>
		/// Backing DependencyProperty for Binding.
		/// </summary>
		public static readonly DependencyProperty BindingProperty = DependencyProperty.Register("Binding", typeof(Binding), typeof(DataStateSwitchBehavior), new PropertyMetadata(null, DataStateSwitchBehavior.HandleBindingChanged));
		/// <summary>
		/// Backing DependencyProperty for States.
		/// </summary>
		public static readonly DependencyProperty StatesProperty = DependencyProperty.Register("States", typeof(List<DataStateSwitchCase>), typeof(DataStateSwitchBehavior), new PropertyMetadata(null));

		private BindingListener listener;

		/// <summary>
		/// Constructor
		/// </summary>
		public DataStateSwitchBehavior() {
			this.listener = new BindingListener(this.HandleBindingValueChanged);
			this.States = new List<DataStateSwitchCase>();
		}

		/// <summary>
		/// Binding to be evaluated.
		/// </summary>
		public Binding Binding {
			get { return (Binding)this.GetValue(DataStateSwitchBehavior.BindingProperty); }
			set { this.SetValue(DataStateSwitchBehavior.BindingProperty, value); }
		}

		/// <summary>
		/// Various states on which this is active or not.
		/// </summary>
		public List<DataStateSwitchCase> States {
			get { return (List<DataStateSwitchCase>)this.GetValue(DataStateSwitchBehavior.StatesProperty); }
			set { this.SetValue(DataStateSwitchBehavior.StatesProperty, value); }
		}

		private static void HandleBindingChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
			((DataStateSwitchBehavior)sender).OnBindingChanged(e);
		}

		/// <summary>
		/// Notification that the Binding property has changed.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnBindingChanged(DependencyPropertyChangedEventArgs e) {
			this.listener.Binding = (Binding)e.NewValue;
		}

		/// <summary>
		/// Performs some initialization.
		/// </summary>
		protected override void OnAttached() {
			base.OnAttached();

			this.listener.Element = this.AssociatedObject;
		}

		/// <summary>
		/// Performs some cleanup.
		/// </summary>
		protected override void OnDetaching() {
			base.OnDetaching();

			this.listener.Element = null;
		}

		private void HandleBindingValueChanged(object sender, BindingChangedEventArgs e) {
			this.CheckState();
		}

		private void CheckState() {
			foreach (DataStateSwitchCase switchCase in this.States) {
				if (switchCase.IsValid(this.listener.Value)) {
					FrameworkElement targetElement = GoToStateBase.FindTargetElement(this.AssociatedObject);
					if (targetElement != null) {
						GoToStateBase.GoToState(targetElement, switchCase.State, true);
					}
					break;
				}
			}
		}
	}

	/// <summary>
	/// Represents a case to check if the visual state should be activated.
	/// </summary>
	public class DataStateSwitchCase {

		/// <summary>
		/// The value to be compared against
		/// </summary>
		public object Value { get; set; }

		/// <summary>
		/// The name of the state to be activated if the value is true
		/// </summary>
		public string State { get; set; }

		/// <summary>
		/// Compares this value to the target value to determine if the
		/// state should be activated.
		/// </summary>
		/// <param name="targetValue"></param>
		/// <returns></returns>
		public bool IsValid(object targetValue) {
			if (targetValue == null || this.Value == null)
				return object.Equals(targetValue, this.Value);

			object convertedValue = ConverterHelper.ConvertToType(this.Value, targetValue.GetType());
			return object.Equals(targetValue, ConverterHelper.ConvertToType(this.Value, targetValue.GetType()));
		}
	}
}
