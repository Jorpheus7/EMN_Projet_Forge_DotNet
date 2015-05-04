// -----------------------------------------------------------------------
// <copyright file="HandlerBase.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

// http://nroute.codeplex.com/

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Soat.HappyNet.Silverlight.Library.Components
{
    public abstract class HandlerBase<E, H>
        : IDisposable
        where E : EventArgs
    {

#region Static Per Type

        protected readonly static Func<Action<Object, E>, H> _createDelegate;
        //protected readonly static Action<H> _removeDelegate;

        static HandlerBase()
        {

#if SILVERLIGHT
            _createDelegate = (a) => (H)(Object)(Delegate.CreateDelegate(typeof(H), a.Target, a.Method));
            //_removeDelegate = (h) => Delegate.Remove(h as Delegate, h as Delegate);
#else
            Expression<Func<Action<Object, E>, H>> _createExpr
                = (a) => (H)(Object)Delegate.CreateDelegate(typeof(H), a.Target, a.Method);
            _createDelegate = _createExpr.Compile();

            //Expression<Action<H>> _removeExpr = (h) => Delegate.Remove((Delegate)(object)h, (Delegate)(object)h);
            //_removeDelegate = _removeExpr.Compile();
#endif

        }

#endregion

        Func<Action<Object, E>, H> _createAction;
        Action<H> _removeAction;
        H _eventHandler;
        bool _isDisposed;

        public HandlerBase(Action<H> removeAction)
            : this((a) => _createDelegate(a), removeAction) { }

        public HandlerBase(Func<Action<Object, E>, H> createAction, Action<H> removeAction)
        {
            if (createAction == null) throw new ArgumentNullException("createAction");
            _createAction = createAction;
            _removeAction = removeAction;
        }

#region Properties

        public Func<Event<E>, bool> Predicate { get; set; }

        public Action<HandlerBase<E, H>, Event<E>> PreHandle { get; set; }

        public Action<HandlerBase<E, H>, Event<E>> PostHandle { get; set; }

        protected Func<Action<Object, E>, H>  CreateAction
        {
            get { return _createAction; }
            set { _createAction = value; }
        }

        protected Action<H> RemoveAction
        {
            get { return _removeAction; }
            set { _removeAction = value; }
        }

        protected H EventHandler
        {
            get { return _eventHandler; }
            set { _eventHandler = value; }
        }

#endregion

        [SuppressMessage("Microsoft.Security", "CA2109:ReviewVisibleEventHandlers")]
        protected abstract void Handle(Object sender, E eventArgs);

#region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_removeAction != null) _removeAction(_eventHandler);
                Predicate = null;
                PreHandle = null;
                PostHandle = null;
                _createAction = null;
                _removeAction = null;
                _eventHandler = default(H);
                _isDisposed = true;
            }
        }

#endregion

#region Operator

        public static implicit operator H(HandlerBase<E, H> handler)
        {
            handler.EventHandler = handler.CreateAction((s, e) =>
            {
                Debug.Assert(handler != null, "Handler should have been detached, as handler is null.");
                if (handler == null) return;
                var _event = new Event<E>(s, e);

                //if (handler.Predicate != null && !handler.Predicate(_event)) return;
                if (!handler._isDisposed && handler.PreHandle != null) 
                    handler.PreHandle(handler, _event);
                if (!handler._isDisposed && (handler.Predicate == null || handler.Predicate(_event)))
                    handler.Handle(s, e);
                if (!handler._isDisposed && (handler.PostHandle != null))
                    handler.PostHandle(handler, _event);
                if (handler._isDisposed) handler = null;        // this should remove the lifting

            });

            // we return
            return handler.EventHandler;
        }

#endregion

    }
}

