// -----------------------------------------------------------------------
// <copyright file="ValueCompareTrigger.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

// http://nroute.codeplex.com/

using System;
using System.Windows;
using Soat.HappyNet.Silverlight.Library.Behaviors.Interactivity;
using System.Windows.Data;
using Soat.HappyNet.Silverlight.Library.Helpers;
using System.ComponentModel;
using System.Collections;

namespace Soat.HappyNet.Silverlight.Library.Behaviors.Triggers
{
    public class ValueCompareTrigger : BindableTriggerBase<FrameworkElement>
    {

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(object), typeof(ValueCompareTrigger), 
            new PropertyMetadata(null, new PropertyChangedCallback(OnSourceChanged)));

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), typeof(ValueCompareTrigger),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ComparerProperty =
            DependencyProperty.Register("Comparer", typeof(IComparer), typeof(ValueCompareTrigger),
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
        public object Value
        {
            get { return (object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public Binding ValueBinding
        {
            get { return GetBinding(ValueProperty); }
            set { SetBinding<object>(ValueProperty, value); }
        }

        [TypeConverter(typeof(ConvertFromStringConverter))]
        public IComparer Comparer
        {
            get { return (IComparer)GetValue(ComparerProperty); }
            set { SetValue(ComparerProperty, value); }
        }

        public Binding ComparerBinding
        {
            get { return GetBinding(ComparerProperty); }
            set { SetBinding<IComparer>(ComparerProperty, value); }
        }

        public EqualityType Equality { get; set; }

        public bool Negate { get; set; }

#endregion

#region Handlers

        static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ValueCompareTrigger)d).CompareValue(e.NewValue);
        }

        void CompareValue(object sourceValue)
        {

            // note, we need to match them per the source type hence the conversion
            bool _result = false;
            
            // if null, we can only match equality
            if (Value == null)
            {
                if (sourceValue == null) 
                    _result = (Equality == EqualityType.Equals || Equality == EqualityType.GreaterThanOrEquals || 
                        Equality == EqualityType.LessThanOrEquals);
            }
            else
            {
                // we compare
                int _compare = 0;
                var _matchValue = ConverterHelper.ConvertToType(Value, sourceValue.GetType());
                if (Comparer != null)
                {
                    _compare = Comparer.Compare(_matchValue, sourceValue);
                }
                else if (_matchValue is IComparable)
                {
                    _compare = (_matchValue as IComparable).CompareTo(sourceValue);
                }
                else 
                {
                    throw new InvalidOperationException(
                        "Cannot compare, please compare IComparable types or specify an IComparer.");
                }

                // per the result we match the equality
                if (_compare == 0)
                {
                    _result = (Equality == EqualityType.Equals ||
                        Equality == EqualityType.GreaterThanOrEquals || Equality == EqualityType.LessThanOrEquals);
                }
                else if (_compare > 0)      // note we do opposite, as we check agains the source
                {
                    _result = (Equality == EqualityType.LessThan || Equality == EqualityType.LessThanOrEquals);
                }
                else    // _compare < 0 - note we do opposite, as we check agains the source
                {
                    _result = (Equality == EqualityType.GreaterThan || Equality == EqualityType.GreaterThanOrEquals);
                }
            }

            // we match with negate and invoke
            if (Negate) _result = !_result;
            if (_result)
                base.InvokeActions(sourceValue);
        }

#endregion

    }
}
