// -----------------------------------------------------------------------
// <copyright file="ValueRegexTrigger.cs" company="So@t">
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
using System.Text.RegularExpressions;

namespace Soat.HappyNet.Silverlight.Library.Behaviors.Triggers
{
    public class ValueRegexTrigger : BindableTriggerBase<FrameworkElement>
    {

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(string), typeof(ValueRegexTrigger), 
            new PropertyMetadata(null, new PropertyChangedCallback(OnSourceChanged)));

        public static readonly DependencyProperty PatternProperty =
            DependencyProperty.Register("Pattern", typeof(string), typeof(ValueRegexTrigger),
            new PropertyMetadata(null));

#region Properties

        [TypeConverter(typeof(ConvertFromStringConverter))]
        public string Source
        {
            get { return Convert.ToString(GetValue(SourceProperty)); }
            set { SetValue(SourceProperty, value); }
        }

        public Binding SourceBinding
        {
            get { return GetBinding(SourceProperty); }
            set { SetBinding<string>(SourceProperty, value); }
        }

        public string Pattern
        {
            get { return Convert.ToString(GetValue(PatternProperty)); }
            set { SetValue(PatternProperty, value); }
        }

        public Binding PatternBinding
        {
            get { return GetBinding(PatternProperty); }
            set { SetBinding<string>(PatternProperty, value); }
        }

        public bool Negate { get; set; }

#endregion

#region Handlers

        static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ValueRegexTrigger)d).MatchValue(e.NewValue);
        }

        void MatchValue(object sourceValue)
        {
            // basic checks
            if (string.IsNullOrEmpty(Pattern)) return;
            
            // get the value n' match
            var _matchValue = Convert.ToString(sourceValue);
            var _result = Regex.IsMatch(_matchValue, Pattern);

            // negate, n' invoke
            if (Negate) _result = !_result;
            if (_result) base.InvokeActions(sourceValue);
        }

#endregion

    }
}
