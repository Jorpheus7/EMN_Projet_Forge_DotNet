// -----------------------------------------------------------------------
// <copyright file="ExecuteCommandAction.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

// http://nroute.codeplex.com/

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Data;
using Soat.HappyNet.Silverlight.Library.Behaviors.Interactivity;
using System.ComponentModel;
using Soat.HappyNet.Silverlight.Library.Helpers;
using System.Windows.Interactivity;

namespace Soat.HappyNet.Silverlight.Library.Behaviors.Actions
{
    public class ExecuteCommandAction : BindableTriggerAction<FrameworkElement>
    {
        const double INTERACTIVITY_ENABLED = 1d;
        const double INTERACTIVITY_DISABLED = 0.5d;

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(ExecuteCommandAction),
            new PropertyMetadata(null, ExecuteCommandAction.HandleCommandChanged));

        public static readonly DependencyProperty ParameterProperty = 
            DependencyProperty.Register("Parameter", typeof(object), typeof(ExecuteCommandAction),
            new PropertyMetadata(null, new PropertyChangedCallback(OnParameterChanged)));

        bool _manageEnableState;

        #region Properties

        public ICommand CommandValue
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public Binding Command
        {
            get { return GetBinding(CommandProperty); }
            set { SetBinding<ICommand>(CommandProperty, value); }
        }

        [TypeConverter(typeof(ConvertFromStringConverter))]
        public object ParameterValue
        {
            get { return GetValue(ParameterProperty); }
            set { SetValue(ParameterProperty, value); }
        }

        public Binding Parameter
        {
            get { return GetBinding(ParameterProperty); }
            set { SetBinding<object>(ParameterProperty, value); }
        }

        public bool ManageEnableState
        {
            get { return _manageEnableState; }
            set { _manageEnableState = value; }
        }

        #endregion

        #region Trigger Related

        protected override void OnAttached()
        {
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            DisposeEnableState();
        }

        protected override void Invoke(object parameter)
        {
            if (CommandValue != null && CommandValue.CanExecute(this.ParameterValue))
            {
                CommandValue.Execute(this.ParameterValue);
            }
        }

        #endregion
                 
        #region Handlers

        private static void HandleCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ExecuteCommandAction)d).SetupEnableState(e.NewValue as ICommand, e.OldValue as ICommand);
        }

        private static void OnParameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ExecuteCommandAction)d).UpdateEnabledState();
        }

        void Command_CanExecuteChanged(object sender, EventArgs e)
        {
            this.UpdateEnabledState();
        }

        #endregion

        #region Helpers

        void SetupEnableState(ICommand newCommand, ICommand oldCommand)
        {
            // basic checks
            if (!ManageEnableState || AssociatedObject == null ) return;

            // we detach or attach
            if (oldCommand != null)
                oldCommand.CanExecuteChanged -= new EventHandler(Command_CanExecuteChanged);
            if (newCommand != null)
                newCommand.CanExecuteChanged += new EventHandler(Command_CanExecuteChanged);

            // and update
            UpdateEnabledState();
        }

        void UpdateEnabledState()
        {
            // basic checks
            if (!ManageEnableState || AssociatedObject == null || Command == null) return;

            // we get if it is enabled or not
            var _canExecute = CommandValue.CanExecute(this.ParameterValue);

            // we check if it is a control            
            if (typeof(Control).IsAssignableFrom(AssociatedObject.GetType()))
            {
                // we check if 
                var _control = AssociatedObject as Control;
                _control.IsEnabled = _canExecute;
            }
            else
            {
                AssociatedObject.IsHitTestVisible = _canExecute;
                AssociatedObject.Opacity = _canExecute ? INTERACTIVITY_ENABLED : INTERACTIVITY_DISABLED;
            }
        }

        void DisposeEnableState()
        {
            if (!ManageEnableState || AssociatedObject == null || Command == null) return;

            if (AssociatedObject as Control != null)
                CommandValue.CanExecuteChanged -= new EventHandler(Command_CanExecuteChanged);
        }

        #endregion

    }
}
