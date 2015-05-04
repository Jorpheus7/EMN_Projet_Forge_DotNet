﻿// -----------------------------------------------------------------------
// <copyright file="BindableTargetedTriggerAction.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

// http://nroute.codeplex.com/

using System;
using System.Windows;
using System.Windows.Interactivity;
using System.Collections.Generic;
using System.Windows.Data;
using Soat.HappyNet.Silverlight.Library.Components.Bindings;

namespace Soat.HappyNet.Silverlight.Library.Behaviors.Interactivity
{
    public abstract class BindableTargetedTriggerAction<T>
        : TargetedTriggerAction<T>
        where T : FrameworkElement
    {
        
        Dictionary<DependencyProperty, BindingInfo> _pendingBindings;

#region Properties

        public Binding IsEnabledBinding
        {
            get { return GetBinding(IsEnabledProperty); }
            set { SetBinding<bool>(IsEnabledProperty, value); }
        }

#endregion

#region Binding Helpers

        protected void SetBinding<P>(DependencyProperty bindingProperty, Binding value)
        {
            SetBinding(bindingProperty, typeof(P), value);
        }

        protected void SetBinding(DependencyProperty bindingProperty, Type bindingType, Binding value)
        {
            if (this.Target != null)
                this.SetAttachedBinding(Target, bindingProperty, bindingType, value);

            else
            {
                if (_pendingBindings == null) 
                    _pendingBindings = new Dictionary<DependencyProperty, BindingInfo>();
                _pendingBindings.Add(bindingProperty, 
                    new BindingInfo() { BindingProperty = bindingProperty, BindingType = bindingType, Binding = value });
            }
        }

        protected Binding GetBinding(DependencyProperty bindingProperty)
        {
            if (this.Target != null)
                return this.GetAttachedBinding(bindingProperty);

            else if (_pendingBindings != null && _pendingBindings.ContainsKey(bindingProperty))
                return _pendingBindings[bindingProperty].Binding;

            else
                return null;
        }

        protected void ApplyAllPendingBindings()
        {
            if (_pendingBindings == null || _pendingBindings.Count == 0) return;
         
            // set all bindings
            foreach (var _bindingInfo in _pendingBindings.Values)
                this.SetAttachedBinding(Target, _bindingInfo);
            
            // clear the pending
            _pendingBindings.Clear();
            _pendingBindings = null;

        }

        protected void ClearAllBindings()
        {
            if (_pendingBindings != null) _pendingBindings = null;
            this.ClearAttachedBindings();
        }

#endregion

#region Trigger Related

        protected override void OnAttached()
        {
            base.OnAttached();
            //ApplyAllPendingBindings();
        }

        protected override void OnTargetChanged(T oldTarget, T newTarget)
        {
            if (oldTarget != null) ClearAllBindings();
            base.OnTargetChanged(oldTarget, newTarget);
            if (newTarget != null) ApplyAllPendingBindings();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            ClearAllBindings();
        }

#endregion

    }
}