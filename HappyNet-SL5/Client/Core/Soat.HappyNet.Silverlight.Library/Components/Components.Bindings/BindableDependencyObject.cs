// -----------------------------------------------------------------------
// <copyright file="BindableDependencyObject.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

// http://nroute.codeplex.com/

using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Windows.Data;
using Soat.HappyNet.Silverlight.Library.Components.Bindings;

namespace Soat.HappyNet.Silverlight.Library.Components.Bindings
{
    public abstract class BindableDependencyObject : DependencyObject
    {

        Dictionary<DependencyProperty, BindingInfo> _pendingBindings;

        protected abstract FrameworkElement AttachTarget { get; }

#region Binding Helpers

        protected void SetBinding<T>(DependencyProperty bindingProperty, Binding value)
        {
            SetBinding(bindingProperty, typeof(T), value);
        }

        protected void SetBinding(DependencyProperty bindingProperty, Type bindingType, Binding value)
        {
            if (this.AttachTarget != null)
                this.SetAttachedBinding(AttachTarget, bindingProperty, bindingType, value);

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
            if (this.AttachTarget != null)
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
                this.SetAttachedBinding(AttachTarget, _bindingInfo);
            
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

    }
}
