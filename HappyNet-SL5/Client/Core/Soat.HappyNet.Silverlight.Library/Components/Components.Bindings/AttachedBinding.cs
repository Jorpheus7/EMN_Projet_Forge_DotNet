// -----------------------------------------------------------------------
// <copyright file="AttachedBinding.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

// http://nroute.codeplex.com/

using System;
using System.Windows;
using System.Windows.Data;

namespace Soat.HappyNet.Silverlight.Library.Components.Bindings
{
    public class AttachedBinding
    {

        const string DP_NAME_FROMAT = "Attached_Binding_{0}";
        static int _indexer = 0;

        readonly WeakReference _target;
        readonly WeakReference _attachTarget;
        readonly DependencyProperty _bindingProperty;
        DependencyProperty _attachedProperty;
        Binding _binding;

        public AttachedBinding(DependencyObject target, FrameworkElement attachTarget, 
            DependencyProperty bindingProperty, Type bindingType)
        {
            // basic checks
            if (target == null) throw new ArgumentNullException("target");
            if (attachTarget == null) throw new ArgumentNullException("attachTarget");
            if (bindingProperty == null) throw new ArgumentNullException("bindingProperty");
            if (bindingType == null) throw new ArgumentNullException("bindingType");
            
            // we save the reference to the source
            _target = new WeakReference(target);
            _attachTarget = new WeakReference(attachTarget);
            _bindingProperty = bindingProperty;

            // we get the default value
            var _defValue = bindingProperty.GetMetadata(bindingType).DefaultValue;

            // we attach the dp
            if (attachTarget != null)
            {
                // we create the attached property
                _attachedProperty = DependencyProperty.RegisterAttached(string.Format(DP_NAME_FROMAT, _indexer++),
                    bindingType, attachTarget.GetType(),
                    new PropertyMetadata(_defValue, new PropertyChangedCallback(OnPropertyChanged)));
            }
            else
            {
                attachTarget.Loaded += (s, e) =>
                {
                    // we create the binding property
                    _attachedProperty = DependencyProperty.RegisterAttached(string.Format(DP_NAME_FROMAT, _indexer++),
                        bindingType, attachTarget.GetType(),
                        new PropertyMetadata(_defValue, new PropertyChangedCallback(OnPropertyChanged)));

                    // and we if have binding then 
                    if (_binding != null) SetBinding(_binding);
                };
            }

        }

#region Properties

        public FrameworkElement AttachTarget 
        {
            get
            {
                if (_attachTarget == null || !_attachTarget.IsAlive) return null;
                var _attachTargetObject = _attachTarget.Target;
                return _attachTargetObject as FrameworkElement;
            }
        }

        public DependencyObject Target
        {
            get
            {
                if (_target == null || !_target.IsAlive) return null;
                var _ownerObject = _target.Target;
                return _ownerObject as DependencyObject;
            }
        }

        public DependencyProperty BindingProperty
        {
            get
            {
                return _bindingProperty;
            }
        }

        public DependencyProperty AttachedProperty
        {
            get
            {
                return _attachedProperty;
            }
        }

        public Binding Binding
        {
            get
            {
                return _binding;
            }
        }

#endregion

#region Helpers

        public void SetBinding(Binding binding)
        {
            // we save the binding
            _binding = binding;

            // we set the binding
            var _attachTargetObj = this.AttachTarget;
            if (_attachTargetObj != null) _attachTargetObj.SetBinding(AttachedProperty, binding); 
        }

        void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // we get the owner
            var _ownerObj = this.Target;
            if (_ownerObj != null) _ownerObj.SetValue(BindingProperty, e.NewValue);
        }

#endregion

    }
}
