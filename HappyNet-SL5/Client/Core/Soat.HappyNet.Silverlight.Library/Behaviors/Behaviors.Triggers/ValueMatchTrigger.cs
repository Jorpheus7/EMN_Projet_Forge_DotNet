// -----------------------------------------------------------------------
// <copyright file="ValueMatchTrigger.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

// http://nroute.codeplex.com/

using System;
using System.Windows;
using Soat.HappyNet.Silverlight.Library.Behaviors.Interactivity;
using System.ComponentModel;
using System.Windows.Data;
using Soat.HappyNet.Silverlight.Library.Helpers;

namespace Soat.HappyNet.Silverlight.Library.Behaviors.Triggers
{

    /// <summary>
    /// Represents a trigger that can match a value being equal to the given value.
    /// </summary>
    public class ValueMatchTrigger : BindableTriggerBase<FrameworkElement>
    {

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(object), typeof(ValueMatchTrigger), 
            new PropertyMetadata(null, new PropertyChangedCallback(OnSourceChanged)));

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), typeof(ValueMatchTrigger),
            new PropertyMetadata(null));

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

#region Handlers

        static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ValueMatchTrigger)d).CompareValue(e.NewValue);
        }

        void CompareValue(object sourceValue)
        {
            // we need to convert it so that the maching can be done
            var _matchValue = sourceValue != null ? ConverterHelper.ConvertToType(Value, sourceValue.GetType()) : null;
            var _result = object.Equals(_matchValue, sourceValue);
            if (Negate) _result = !_result;
            if (_result)
                base.InvokeActions(sourceValue);
        }

#endregion

    }
}
