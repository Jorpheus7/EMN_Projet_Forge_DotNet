// -----------------------------------------------------------------------
// <copyright file="NotifyPropertyChangedExtensions.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Soat.HappyNet.Silverlight.Library.Extensions
{
    public static class NotifyPropertyChangedExtensions
    {
         /// <example>this.NotifyPropertyChanged(() => this.Name, RaisePropertyChanged);</example>
         public static void NotifyPropertyChanged<TValue>(this Object target,   
             Expression<Func<TValue>> propertySelector, Action<string> notifier)  
         {  
             if (notifier != null)  
             {  
                 var memberExpression = propertySelector.Body as MemberExpression;  
                 if (memberExpression != null)  
                 {  
                     notifier(memberExpression.Member.Name);  
                 }  
             }  
   
         }  

         /// <example>new Action<string>(RaisePropertyChanged).Notify(() => this.Name);  </example>
         public static void Notify<TValue>(this Action<string> notifier,   
             Expression<Func<TValue>> propertySelector)  
         {  
   
             if (notifier != null)  
             {  
                 var memberExpression = propertySelector.Body as MemberExpression;  
                 if (memberExpression != null)  
                 {  
                     notifier(memberExpression.Member.Name);  
                 }  
             }  
   
         }  
   
        /// <example>PropertyChanged.Raise(() => this.Name);</example>
         public static void Raise<TValue>(this PropertyChangedEventHandler handler, Expression<Func<TValue>> propertySelector)
         {

             if (handler != null)
             {
                 var memberExpression = propertySelector.Body as MemberExpression;
                 if (memberExpression != null)
                 {
                     var _sender = ((ConstantExpression)memberExpression.Expression).Value;
                     handler(_sender, new PropertyChangedEventArgs(memberExpression.Member.Name));
                 }
             }
         }
    }
}
