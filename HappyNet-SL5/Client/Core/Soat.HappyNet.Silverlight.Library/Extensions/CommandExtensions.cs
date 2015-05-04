// -----------------------------------------------------------------------
// <copyright file="CommandExtensions.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

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
using Microsoft.Practices.Composite.Presentation.Commands;
using System.Linq.Expressions;
using System.ComponentModel;
using Soat.HappyNet.Silverlight.Library.Components;
using System.Collections.Specialized;

namespace Soat.HappyNet.Silverlight.Library.Extensions
{
    public static class CommandExtensions
    {
        public static T RequeryOnPropertyChanged<T>(this T command, INotifyPropertyChanged notifiable)
            where T : ICommand, IWeakEventListener
        {
            if (command == null) throw new ArgumentNullException("command");
            if (notifiable == null) throw new ArgumentNullException("notifiable");

            // we attach a weak listner to hear for any changes in the property
            notifiable.PropertyChanged += new WeakListenerHandler<PropertyChangedEventArgs, PropertyChangedEventHandler>
                (command, (h) => notifiable.PropertyChanged -= h);
            return command;
        }

        public static T RequeryOnPropertyChanged<T>(this T command,
            INotifyPropertyChanged notifiable, Expression<Func<Object>> propertySelector)
            where T : ICommand, IWeakEventListener
        {
            if (command == null) throw new ArgumentNullException("command");
            if (notifiable == null) throw new ArgumentNullException("notifiable");
            if (propertySelector == null) throw new ArgumentNullException("propertySelector");

            // we attach a weak listner to hear for any changes in the property
            notifiable.PropertyChanged += new WeakListenerHandler<PropertyChangedEventArgs, PropertyChangedEventHandler>
                (command, (h) => notifiable.PropertyChanged -= h).HandleFor(propertySelector);
            return command;
        }

        public static T RequeryOnCommandCanExecuteChanged<T>(this T command, ICommand relatedCommand)
            where T : ICommand, IWeakEventListener
        {
            if (command == null) throw new ArgumentNullException("command");
            if (relatedCommand == null) throw new ArgumentNullException("relatedCommand");

            // we attach a weak listner to hear for any changes in the other command
            relatedCommand.CanExecuteChanged += new WeakListenerHandler(command,
                (h) => relatedCommand.CanExecuteChanged -= h);
            return command;
        }

        public static T RequeryOnCollectionChanged<T>(this T command,
            INotifyCollectionChanged collection)
            where T : ICommand, IWeakEventListener
        {
            if (command == null) throw new ArgumentNullException("command");
            if (collection == null) throw new ArgumentNullException("collection");

            // we attach a weak listner to hear for any changes in the collection
            collection.CollectionChanged += new WeakListenerHandler<NotifyCollectionChangedEventArgs, NotifyCollectionChangedEventHandler>
                (command, (h) => collection.CollectionChanged -= h);
            return command;
        }
    }
}
