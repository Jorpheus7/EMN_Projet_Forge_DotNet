// -----------------------------------------------------------------------
// <copyright file="PropertyChangedExtensions.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace Soat.HappyNet.Silverlight.Library.Extensions
{
    public static class PropertyChangedExtensions
    {
        private const string SELECTOR_MUSTBEPROP = "PropertySelector must select a property type.";

        //public static void Notify<T>(this INotifyPropertyChanged target,
        //   Expression<Func<T>> propertySelector, Action<string> notifier)
        //{
        //    if (notifier != null)
        //        notifier(GetPropertyName(propertySelector));
        //}

        public static void Notify<T>(this Action<string> notifier, Expression<Func<T>> propertySelector)
        {
            if (notifier != null)
                notifier(GetPropertyName(propertySelector));
        }

        public static void Notify<T>(this PropertyChangedEventHandler handler, Expression<Func<T>> propertySelector)
        {
            if (handler != null)
            {
                var memberExpression = GetMemberExpression(propertySelector);
                if (memberExpression != null)
                {
                    var _sender = ((ConstantExpression)memberExpression.Expression).Value;
                    handler(_sender, new PropertyChangedEventArgs(memberExpression.Member.Name));
                }
                //else we raise error?
            }
        }

        public static string GetPropertyName<T>(Expression<Func<T>> propertySelector)
        {
            var _memberExpression = propertySelector.Body as MemberExpression;
            if (_memberExpression == null) 
            {
                var _unaryExpression = propertySelector.Body as UnaryExpression;
                if (_unaryExpression != null) _memberExpression = _unaryExpression.Operand as MemberExpression;
            }
            if (_memberExpression != null)
            {
                if (_memberExpression.Member.MemberType != MemberTypes.Property) throw new ArgumentException(SELECTOR_MUSTBEPROP);
                return _memberExpression.Member.Name;
            }
            return null;
        }

        public static MemberExpression GetMemberExpression<T>(Expression<Func<T>> propertySelector)
        {

#if SILVERLIGHT

            var _memberExpression = propertySelector.Body as MemberExpression;
            if (_memberExpression != null)
            {
                if (_memberExpression.Member.MemberType != MemberTypes.Property)  throw new ArgumentException(SELECTOR_MUSTBEPROP);
                return _memberExpression;
            }
#else

            var _unaryExpression = propertySelector.Body as UnaryExpression;
            if (_unaryExpression != null)
            {
                var _memberExpression = _unaryExpression.Operand as MemberExpression;
                if (_memberExpression != null)
                {
                    if (_memberExpression.Member.MemberType != MemberTypes.Property)
                        throw new ArgumentException(SELECTOR_MUSTBEPROP);
                    return _memberExpression;
                }
            }
#endif
            // all else
            return null;
        }

    }
}


//        public static string GetPropertyName(Expression<Func<Object>> propertySelector)
//        {

//#if SILVERLIGHT
//            var _memberExpression = propertySelector.Body as MemberExpression;
//            if (_memberExpression != null)
//            {
//                if (_memberExpression.Member.MemberType != MemberTypes.Property)
//                    throw new ArgumentException("PropertySelector must select a property type.");
//                return _memberExpression.Member.Name;
//            }
//            return null;
//#else
//            var _unaryExpression = propertySelector.Body as UnaryExpression;
//            if (_unaryExpression != null)
//            {
//                var _memberExpression = _unaryExpression.Operand as MemberExpression;
//                if (_memberExpression != null)
//                {
//                    if (_memberExpression.Member.MemberType != MemberTypes.Property)
//                        throw new ArgumentException("PropertySelector must select a property type.");
//                    return _memberExpression.Member.Name;
//                }
//            }
//            return null;
//#endif

//        }

//        public static string GetPropertyName<T>(Expression<Func<T>> propertySelector)
//        {

//#if SILVERLIGHT

//            var _memberExpression = propertySelector.Body as MemberExpression;
//            if (_memberExpression == null)
//            {
//                var _unaryExpression = propertySelector.Body as UnaryExpression;
//                if (_unaryExpression != null) _memberExpression = _unaryExpression.Operand as MemberExpression;
//            }
//            if (_memberExpression != null)
//            {
//                if (_memberExpression.Member.MemberType != MemberTypes.Property)
//                    throw new ArgumentException("PropertySelector must select a property type.");
//                return _memberExpression.Member.Name;
//            }
//#else
//            var _unaryExpression = propertySelector.Body as UnaryExpression;
//            if (_unaryExpression != null)
//            {
//                var _memberExpression = _unaryExpression.Operand as MemberExpression;
//                if (_memberExpression != null)
//                {
//                    if (_memberExpression.Member.MemberType != MemberTypes.Property)
//                        throw new ArgumentException("PropertySelector must select a property type.");
//                    return _memberExpression.Member.Name;
//                }
//            }
//#endif
//            // all else
//            return null;
//        }