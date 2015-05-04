// -----------------------------------------------------------------------
// <copyright file="MouseWheelTrigger.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

// http://nroute.codeplex.com/

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.ComponentModel;
using Soat.HappyNet.Silverlight.Library.Components;

namespace Soat.HappyNet.Silverlight.Library.Behaviors.Triggers
{

    public class MouseWheelTrigger : TriggerBase<UIElement>
    {
        private const double DEFAULT_DELTAFACTOR = 120d;

        public static readonly DependencyProperty DeltaFactorProperty =
            DependencyProperty.Register("DeltaFactor", typeof(object), typeof(MouseWheelTrigger),
            new PropertyMetadata(DEFAULT_DELTAFACTOR, new PropertyChangedCallback(OnDeltaFactorChanged)));

        Duration _throttle;
        Handler<MouseWheelEventArgs, MouseWheelEventHandler> _mouseWheelHandler;
        
#region Properties

        /// <summary>
        /// Factors the delta with the specified value.
        /// </summary>
        public double DeltaFactor
        {
            get { return Convert.ToDouble((DeltaFactorProperty)); }
            set { SetValue(DeltaFactorProperty, value); }
        }

        [TypeConverter(typeof(DurationConverter))]
        public Duration Throttle
        {
            get { return _throttle; }
            set { _throttle = value; }
        }

#endregion

#region Overrides

        protected override void OnAttached()
        {
            base.OnAttached();

            _mouseWheelHandler = new Handler<MouseWheelEventArgs, MouseWheelEventHandler>(OnMouseWheel,
                    (h) => AssociatedObject.MouseWheel -= h);
                AssociatedObject.MouseWheel += _mouseWheelHandler;

            // if throttling is required
            if (Throttle.HasTimeSpan && Throttle != TimeSpan.MaxValue && Throttle > TimeSpan.Zero)
                _mouseWheelHandler.Throttle(Throttle.TimeSpan);

        }

        protected override void OnDetaching()
        {
           base.OnDetaching();
           _mouseWheelHandler.Dispose();
        }

#endregion

#region Handlers

        void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Double _delta = e.Delta / DeltaFactor;
            base.InvokeActions(_delta);
        }

        static void OnDeltaFactorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var _newValue = Convert.ToDouble(e.NewValue);
            if (_newValue < 0) throw new ArgumentException("DeltaFactor cannot be less than 0");
        }

#endregion

    }

}
