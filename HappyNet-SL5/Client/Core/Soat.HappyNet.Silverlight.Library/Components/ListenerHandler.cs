﻿// -----------------------------------------------------------------------
// <copyright file="ListenerHandler.cs" company="So@t">
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

    public class ListenerHandler<E, H> : HandlerBase<E, H>
        where E : EventArgs
    {

        public ListenerHandler(IWeakEventListener listener, Action<H> removeAction)
            : this((a) => _createDelegate(a), listener, removeAction) { }

        public ListenerHandler(Func<Action<Object, E>, H> createAction, IWeakEventListener listener, Action<H> removeAction)
            : base(createAction, removeAction)
        {
            if (listener == null) throw new ArgumentNullException("listener");
            Listener = listener;
        }

        protected IWeakEventListener Listener { get; set; }

#region Overrides

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                base.Dispose(true);
                if (Listener != null) Listener = null;
            }
        }

        protected override void Handle(object sender, E eventArgs)
        {
            // we use a flag coz if the listener returns false we need to remove it
            bool _removeHandlerFlag = false;

            if (Listener != null)
                // if it returns false then it doesn't wanna recieve anymore
                _removeHandlerFlag = !(Listener.ReceiveWeakEvent(typeof(H), sender, eventArgs));
            else
                // since we don't have a listener, we ensure we unregister
                _removeHandlerFlag = true;
            
            // we remove
            if (_removeHandlerFlag) Dispose();
        }

#endregion

#if DEBUG // && WRITETOCONSOLE
        ~ListenerHandler()
        {
            Debug.WriteLine("Releasing Listener Handler<E,H> " + this.GetHashCode().ToString());
        }
#endif

    }

    public class WeakListenerHandler<E, H> : HandlerBase<E, H>
        where E : EventArgs
    {

        WeakReference _listener;

        public WeakListenerHandler(IWeakEventListener listener, Action<H> removeAction)
            : this((a) => _createDelegate(a), listener, removeAction) { }

        public WeakListenerHandler(Func<Action<Object, E>, H> createAction, IWeakEventListener listener, 
            Action<H> removeAction) : base(createAction, removeAction)
        {
            if (listener == null) throw new ArgumentNullException("listener");
            Listener = listener;
        }

        protected IWeakEventListener Listener
        {
            get
            {
                if (_listener == null || !_listener.IsAlive) return null;
                var _targetObj = _listener.Target;
                return (IWeakEventListener)_targetObj;
            }
            set
            {
                if (value == null)
                {
                    if (_listener != null) _listener.Target = null;
                    _listener = null;
                }
                else
                    _listener = new WeakReference(value);
            }
        }

#region Overrides

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                base.Dispose(true);
                if (Listener != null) Listener = null;
            }
        }

        protected override void Handle(object sender, E eventArgs)
        {
            // we use a flag coz if the listener returns false we need to remove it
            bool _removeHandlerFlag = true;
            IWeakEventListener _listenerObj = Listener;

            // if we recieve a false signal then
            if (_listenerObj != null)
                _removeHandlerFlag = !_listenerObj.ReceiveWeakEvent(typeof(H), sender, eventArgs);

            // we remove the handler
            if (_removeHandlerFlag) Dispose();
        }

#endregion

#if DEBUG // && WRITETOCONSOLE
        ~WeakListenerHandler()
        {
            Debug.WriteLine("Releasing Weak Listener Handler<E,H> " + this.GetHashCode().ToString());
        }
#endif

    }

}
