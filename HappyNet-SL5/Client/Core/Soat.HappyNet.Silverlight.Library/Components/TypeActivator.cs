// -----------------------------------------------------------------------
// <copyright file="TypeActivator.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

// http://nroute.codeplex.com/

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Linq.Expressions;
using System.Reflection;

namespace Soat.HappyNet.Silverlight.Library.Components
{
    /// <summary>
    /// A type-instance activator, that initializes types using only public and parameter-less constructors.
    /// </summary>
    /// <remarks>
    /// - This is much more faster than the system CreateType, as it uses dynamic methods.
    /// - Secondly, it also caches the activator so next set of instances don't need to create the instances.
    /// </remarks>
    public static class TypeActivator
    {
        private const string DELEGATE_NAME_FORMAT = "__DelegateInvoker{0}";

        readonly static Type[] _emptyParameters = new Type[] { };
        readonly static Dictionary<Type, Func<Object>> _activators;
        readonly static Object _lock = new Object();

        static TypeActivator()
        {
            _activators = new Dictionary<Type, Func<object>>();
        }

        /// <summary>
        /// Creates an instance of <see cref="T">Type T</see>.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of object to create</typeparam>
        /// <returns>An instance of <see cref="T"/></returns>
        public static T CreateInstance<T>()
        {
            return (T)CreateInstance(typeof(T));
        }

        /// <summary>
        /// Creates an instance of <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> of object to create</typeparam>
        /// <returns>An instance of <paramref name="type"/>.</returns>
        public static object CreateInstance(Type type)
        {
            // basic check
            if (type == null) throw new ArgumentNullException("type");

            // we try and get the activator else we need to 
            Func<Object> _activator = null;
            if (!_activators.TryGetValue(type, out _activator))
            {
                _activator = CreateParameterLessActivator(type);
                lock (_lock)
                {
                    // double check and add
                    if (!_activators.ContainsKey(type)) 
                        _activators.Add(type, _activator);
                }
            }

            // and return
            return _activator();
        }

#region Helper

        delegate Object DelegateActivator();

        static Func<Object> CreateParameterLessActivator(Type type)
        {

            // adopted from http://beaucrawford.net/post/Constructor-invocation-with-DynamicMethod.aspx
            
#if SILVERLIGHT
            var _dynamicMethod = new DynamicMethod(string.Format(DELEGATE_NAME_FORMAT, type.Name), type, _emptyParameters);
#else
            var _dynamicMethod = new DynamicMethod(string.Format(DELEGATE_NAME_FORMAT, type.Name), type, _constParms, type);
#endif
            var _ilGenerator = _dynamicMethod.GetILGenerator();

            var constructor = type.GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, _emptyParameters, null);
            //_ilGenerator.Emit(OpCodes.Nop);
            //_ilGenerator.Emit(OpCodes.Ldarg_0);
            _ilGenerator.Emit(OpCodes.Newobj, constructor);
            _ilGenerator.Emit(OpCodes.Ret);

            // note we actually create DelegateInvoker<T, E> but get it back as DelegateInvoker<T>
            var _delegate = _dynamicMethod.CreateDelegate(typeof(DelegateActivator)) as DelegateActivator;
            Expression<Func<Object>> _exp = () => _delegate();
            return _exp.Compile();

        }

#endregion

    }
}
