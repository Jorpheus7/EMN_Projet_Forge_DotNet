// -----------------------------------------------------------------------
// <copyright file="EventDelegatesInvoker.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

// http://nroute.codeplex.com/

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq.Expressions;

namespace Soat.HappyNet.Silverlight.Library.Components
{
    public sealed class EventDelegateInvoker<E>
        where E : EventArgs
    {
        private const string DELEGATE_NAME_FORMAT = "_____DelegateInvoker{0}";

        static readonly Dictionary<Type, Func<MethodInfo, DelegateInvoker<E>>> _activators;
        static readonly Type _invokerGenericType = typeof(DelegateInvoker<Object, E>).GetGenericTypeDefinition();
        private static readonly Object _lock = new Object();

        readonly Type _targetType;
        readonly MethodInfo _method;
        DelegateInvoker<E> _invoker;

        static EventDelegateInvoker()
        {
            _activators = new Dictionary<Type, Func<MethodInfo, DelegateInvoker<E>>>();
        }
        
        public EventDelegateInvoker(Type targetType, MethodInfo method)
        {
            if (method == null) throw new ArgumentNullException("method");
            _targetType = targetType;
            _method = method;
        }

#region Main Method

        public void RaiseEvent(Object target, Object sender, E args)
        {
            if (_invoker == null) CreateInvoker();
            _invoker.Invoke(target, sender, args);
        }

#endregion

#region Helpers

        void CreateInvoker()
        {
            // lazy load in a way
            if (_targetType != null)
            {
                lock (_lock)
                {
                    if (_activators.ContainsKey(_targetType))
                        _invoker = _activators[_targetType](_method);
                    else 
                    {
                        var _func = CreateActivator();
                        _activators.Add(_targetType, _func);
                        _invoker = _func(_method);
                    }
                }
            }
            else
            {
                _invoker = new DelegateInvoker<E>(_method);
            }
        }

        Func<MethodInfo, DelegateInvoker<E>> CreateActivator()
        {
            
            // adopted from http://beaucrawford.net/post/Constructor-invocation-with-DynamicMethod.aspx
            var type = _invokerGenericType.MakeGenericType(typeof(E), _targetType, typeof(E));
            var _constParms = new Type[] { typeof(MethodInfo) };
#if SILVERLIGHT
            var _dynamicMethod = new DynamicMethod(string.Format(DELEGATE_NAME_FORMAT, type.Name), type, _constParms);
#else
            var _dynamicMethod = new DynamicMethod(string.Format(DELEGATE_NAME_FORMAT, type.Name), type, _constParms, type);
#endif
            var _ilGenerator = _dynamicMethod.GetILGenerator();
            
            var constructor = type.GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, _constParms, null);
            _ilGenerator.Emit(OpCodes.Nop);
            _ilGenerator.Emit(OpCodes.Ldarg_0);
            _ilGenerator.Emit(OpCodes.Newobj, constructor);
            _ilGenerator.Emit(OpCodes.Ret);

            // note we actually create DelegateInvoker<T, E> but get it back as DelegateInvoker<T>
            var _delegate = _dynamicMethod.CreateDelegate(typeof(DelegateActivator<DelegateInvoker<E>, MethodInfo>)) 
                as DelegateActivator<DelegateInvoker<E>, MethodInfo>;
            Expression<Func<MethodInfo, DelegateInvoker<E>>> _exp = (m) => _delegate(m);
            return _exp.Compile();

        }

#endregion

#region Internal Classes

        delegate T DelegateActivator<T, A>(A arg1) 
            where T : class;

        class DelegateInvoker<F>
            where F : EventArgs
        {
            readonly EventDelegate<F> _delegate;

            protected DelegateInvoker() { }

            public DelegateInvoker(MethodInfo method)
            {
                if (method == null) throw new ArgumentNullException("method");
#if SILVERLIGHT
                _delegate = (EventDelegate<F>)Delegate.CreateDelegate(typeof(EventDelegate<F>), method);
#else
                _delegate = (EventDelegate<E>)Delegate.CreateDelegate(typeof(EventDelegate<E>), method, true);
#endif
            }

            public virtual void Invoke(Object target, Object sender, F args)
            {
                _delegate(sender, args);
            }

        }

        class DelegateInvoker<T, G>
            : DelegateInvoker<G>
            where G : EventArgs
        {
            readonly EventDelegate<T, G> _delegate;

            public DelegateInvoker(MethodInfo method)
                : base()
            {
                if (method == null) throw new ArgumentNullException("method");
#if SILVERLIGHT
                _delegate = (EventDelegate<T, G>)Delegate.CreateDelegate(typeof(EventDelegate<T, G>), method);
#else
                _delegate = (EventDelegate<T, E>)Delegate.CreateDelegate(typeof(EventDelegate<T, E>), method, true);
#endif
            }

            public override void Invoke(object target, object sender, G args)
            {
                this.Invoke((T)target, sender, args);
            }

            public void Invoke(T target, object sender, G args)
            {
                _delegate(target, sender, args);
            }

        }

#endregion

    }
}
