﻿// -----------------------------------------------------------------------
// <copyright file="Handler.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

// http://nroute.codeplex.com/

using System;
using System.Diagnostics;

namespace Soat.HappyNet.Silverlight.Library.Components
{

    public class Handler<E, H> 
        : HandlerBase<E, H>
        where E : EventArgs
    {
        
        public Handler(Action<Object, E> action, Action<H> removeAction)
            : this((a) => _createDelegate(a), action, removeAction) { }

        public Handler(Func<Action<Object, E>, H> createAction, Action<Object, E> action, Action<H> removeAction)
            : base(createAction, removeAction)
        {
            if (action == null) throw new ArgumentNullException("action");
            Action = action;
        }

        protected Action<Object, E> Action { get; set; }

#region Overrides

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                base.Dispose(true);
                Action = null;
            }
        }

        protected override void Handle(object sender, E eventArgs)
        {
            if (Action != null)
                Action(sender, eventArgs);
            else
                Dispose();      // if we don't have a action thing            
        }

#endregion

#if DEBUG // && WRITETOCONSOLE
        ~Handler()
        {
            Debug.WriteLine("Releasing Handler<E, H> " + this.GetHashCode().ToString() + " " + typeof(E).Name);
        }
#endif

    }

    public class WeakHandler<E, H> 
        : HandlerBase<E, H>
        where E : EventArgs
    {

        WeakReference _target;
        EventDelegateInvoker<E> _invoker;
        readonly bool _methodIsStatic;

        public WeakHandler(Action<Object, E> action, Action<H> removeAction)
            : this((a) => _createDelegate(a), action, removeAction) { }

        public WeakHandler(Func<Action<Object, E>, H> createAction, Action<Object, E> action, Action<H> removeAction)
            : base(createAction, removeAction)
        {
            if (action == null) throw new ArgumentNullException("action");
            Target = action.Target;
            
            var _methodInfo = action.Method;
            _methodIsStatic = _methodInfo.IsStatic;
            _invoker = new EventDelegateInvoker<E>(action.Target != null ? action.Target.GetType() : null, _methodInfo);
        }

        protected Object Target
        {
            get
            {
                if (_target == null || !_target.IsAlive) return null;
                var _targetObj = _target.Target;
                return _targetObj;
            }
            set
            {
                if (value == null)
                {
                    if (_target != null) _target.Target = null;
                    _target = null;
                }
                else
                    _target = new WeakReference(value);
            }
        }

#region Overrides

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                base.Dispose(true);
                if (Target != null) Target = null;
                _invoker = null;
            }
        }

        protected override void Handle(object sender, E eventArgs)
        {

            Object _targetObj = Target;

            if (_targetObj != null && _invoker != null)
            {
                _invoker.RaiseEvent(_targetObj, sender, eventArgs);
            }
            else if (_invoker != null && _methodIsStatic)
            {
                _invoker.RaiseEvent(null, sender, eventArgs);
            }
            else
            {
                Dispose();
            }
        }

#endregion

#if DEBUG // && WRITETOCONSOLE
        ~WeakHandler()
        {
            Debug.WriteLine("Releasing Weak Handler<E, H> " + this.GetHashCode().ToString() + " " + typeof(E).Name);
        }
#endif

    }

}
