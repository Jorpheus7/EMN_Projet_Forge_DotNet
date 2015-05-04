// -----------------------------------------------------------------------
// <copyright file="ValueSwitchTrigger.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

// http://nroute.codeplex.com/

using System.Windows.Data;
using System.Collections.Specialized;
using System.Windows;
using Soat.HappyNet.Silverlight.Library.Behaviors.Interactivity;
using Soat.HappyNet.Silverlight.Library.Helpers;

namespace Soat.HappyNet.Silverlight.Library.Behaviors.Triggers
{

    public class ValueSwitchTrigger: BindableTriggerBase<FrameworkElement>
    {

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(object), typeof(ValueSwitchTrigger), 
            new PropertyMetadata(null, new PropertyChangedCallback(OnSourceChanged)));

        public static readonly DependencyProperty CaseValueProperty =
            DependencyProperty.RegisterAttached("CaseValue", typeof(object), typeof(ValueSwitchTrigger),
            new PropertyMetadata(null));

        public ValueSwitchTrigger()
        {
            base.Actions.CollectionChanged += new NotifyCollectionChangedEventHandler(Actions_CollectionChanged);
        }

#region Properties

        public object Source
        {
            get { return GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public Binding SourceBinding
        {
            get { return GetBinding(SourceProperty); }
            set { SetBinding<object>(SourceProperty, value); }
        }

#endregion

#region TriggersRelated

        protected override void OnAttached()
        {
            base.OnAttached();
            UpdateCases(this.Source);
        }

        protected override void OnDetaching()
        {
            base.Actions.CollectionChanged -= new NotifyCollectionChangedEventHandler(Actions_CollectionChanged);
            base.OnDetaching();
        }

#endregion

#region Handlers 

        void Actions_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && base.AssociatedObject != null)
                foreach (var _triggerAction in e.NewItems)
                    ((System.Windows.Interactivity.TriggerAction)_triggerAction).IsEnabled = false;

            if (e.OldItems != null)
                foreach (var _triggerAction in e.OldItems)
                    ((System.Windows.Interactivity.TriggerAction)_triggerAction).IsEnabled = false;
        }

#endregion

#region Handlers

        static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ValueSwitchTrigger)d).UpdateCases(e.NewValue);
        }

        void UpdateCases(object sourceValue)
        {

            // basic check, we can't do switch casing if the source value is null
            if (sourceValue == null)
            {
                // we disable all
                foreach (var _triggerAction in base.Actions)
                    _triggerAction.IsEnabled = false;
                return;
            } 

            // we check on each of the actions
            foreach (var _triggerAction in base.Actions)
            {

                // we get the value
                var _value = GetCaseValue(_triggerAction);
                if (_value == null)
                {
                    _triggerAction.IsEnabled = false;
                    continue;
                }

                // we try and match the value also we need to convert it so that the maching can be done
                var _matchValue = ConverterHelper.ConvertToType(_value, sourceValue.GetType());
                _triggerAction.IsEnabled = (object.Equals(_matchValue, sourceValue));

            }

            // and this invokes only the one that maches our value
            base.InvokeActions(sourceValue);
        }

#endregion

#region Attached Properties Related

        public static void SetCaseValue(System.Windows.Interactivity.TriggerAction triggerAction, object value)
        {
            triggerAction.SetValue(CaseValueProperty, value);
        }

        public static object GetCaseValue(System.Windows.Interactivity.TriggerAction triggerAction)
        {
            return triggerAction.GetValue(CaseValueProperty);
        }

#endregion

    }
}
