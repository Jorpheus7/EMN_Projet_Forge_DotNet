// -----------------------------------------------------------------------
// <copyright file="KeyTrigger.cs" company="So@t">
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
    public class KeyTrigger : TriggerBase<UIElement>
    {

        Duration _throttle;
        Handler<KeyEventArgs, KeyEventHandler> _keyHandler;

#region Properties

        public KeyEvent KeyEvent { get; set; }

        public int KeyCode { get; set; }

        public Key Key { get; set; }

        public bool WithAltModifier {get; set; }

        public bool WithControlModifier {get; set; }

        public bool WithShiftModifier { get; set; }

        public bool WithWindowsModifier { get; set; }

        public bool WithAppleModifier { get; set; }

        public bool Negate { get; set; }

        public bool SetIsHandled { get; set; }

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

            if (KeyEvent == KeyEvent.KeyUp)
            {
                _keyHandler = new Handler<KeyEventArgs, KeyEventHandler>(OnKeyEvent,
                    (h) => AssociatedObject.KeyUp -= h);
                AssociatedObject.KeyUp += _keyHandler;
            }
            else
            {
                _keyHandler = new Handler<KeyEventArgs, KeyEventHandler>(OnKeyEvent,
                    (h) => AssociatedObject.KeyDown -= h);
                AssociatedObject.KeyDown += _keyHandler;
            }

            // if throttling is required
            if (Throttle.HasTimeSpan && Throttle != TimeSpan.MaxValue && Throttle > TimeSpan.Zero)
                _keyHandler.Throttle(Throttle.TimeSpan);
        }

        protected override void OnDetaching()
        {
           base.OnDetaching();
           _keyHandler.Dispose();
        }

#endregion

#region Handler

        void OnKeyEvent(object sender, KeyEventArgs e)
        {

            // basic checks
            bool _isMatch = (KeyCode != 0) ? (KeyCode == e.PlatformKeyCode) : (e.Key == Key);

            // match the modifiers
            var _modifiers = Keyboard.Modifiers;
            if (WithAltModifier && (_modifiers & ModifierKeys.Alt) != ModifierKeys.Alt) _isMatch = false;
            if (WithControlModifier && (_modifiers & ModifierKeys.Control) != ModifierKeys.Control) _isMatch = false;
            if (WithShiftModifier && (_modifiers & ModifierKeys.Shift) != ModifierKeys.Shift) _isMatch = false;
            if (WithWindowsModifier && (_modifiers & ModifierKeys.Windows) != ModifierKeys.Windows) _isMatch = false;
            if (WithAppleModifier && (_modifiers & ModifierKeys.Apple) != ModifierKeys.Apple) _isMatch = false;

            if (Negate) _isMatch = !_isMatch;
            if (!_isMatch) return;

            // raise
            base.InvokeActions(e.Key);

            // handled
            if (SetIsHandled) e.Handled = true;
        }

#endregion

    }
}
