// -----------------------------------------------------------------------
// <copyright file="ValueNullTrigger.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

// http://nroute.codeplex.com/

using System.Windows;
using System.Windows.Data;
using Soat.HappyNet.Silverlight.Library.Behaviors.Interactivity;

namespace Soat.HappyNet.Silverlight.Library.Behaviors.Triggers
{
    public class ValueNullTrigger: BindableTriggerBase<FrameworkElement>
    {

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(object), typeof(ValueNullTrigger), 
            new PropertyMetadata(null, new PropertyChangedCallback(OnSourceChanged)));

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

        public bool Negate { get; set; }

#endregion

#region Handlers

        static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ValueNullTrigger)d).CompareValue(e.NewValue);
        }

        void CompareValue(object value)
        {
            var _result = (value == null);
            if (Negate) _result = !_result;
            if (_result)
                base.InvokeActions(value);
        }

#endregion

    }
}
