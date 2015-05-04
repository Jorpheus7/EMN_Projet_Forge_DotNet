// -----------------------------------------------------------------------
// <copyright file="Handlers.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

// http://nroute.codeplex.com/

using System;
using System.ComponentModel;
using System.Windows;

namespace Soat.HappyNet.Silverlight.Library.Components
{
    // for EventHandler

    public sealed class Handler : Handler<EventArgs, EventHandler>
    {
        public Handler(Action<Object, EventArgs> action, Action<EventHandler> removeAction)
            : base((a) => new EventHandler(a), action, removeAction) { }
    }

    public sealed class WeakHandler : WeakHandler<EventArgs, EventHandler>
    {
        public WeakHandler(Action<Object, EventArgs> action, Action<EventHandler> removeAction)
            : base((a) => new EventHandler(a), action, removeAction) { }
    }

    // for EventHandler<E>

    public sealed class Handler<E> : Handler<E, EventHandler<E>>
       where E : EventArgs
    {
        public Handler(Action<Object, E> action, Action<EventHandler<E>> removeAction)
            : base((a) => new EventHandler<E>(a), action, removeAction) { }
    }

    public sealed class WeakHandler<E> : WeakHandler<E, EventHandler<E>>
        where E : EventArgs
    {
        public WeakHandler(Action<Object, E> action, Action<EventHandler<E>> removeAction)
            : base((a) => new EventHandler<E>(a), action, removeAction) { }
    }

    // for Routed Events

    public sealed class RoutedHandler : Handler<RoutedEventArgs, RoutedEventHandler>
    {
        public RoutedHandler(Action<Object, RoutedEventArgs> action, Action<RoutedEventHandler> removeAction)
            : base((a) => new RoutedEventHandler(a), action, removeAction) { }
    }

    public sealed class WeakRoutedHandler : WeakHandler<RoutedEventArgs, RoutedEventHandler>
    {
        public WeakRoutedHandler(Action<Object, RoutedEventArgs> action, Action<RoutedEventHandler> removeAction)
            : base((a) => new RoutedEventHandler(a), action, removeAction) { }
    }

    // for Property Changed

    public sealed class PropertyChangedHandler : Handler<PropertyChangedEventArgs, PropertyChangedEventHandler>
    {
        public PropertyChangedHandler(Action<Object, PropertyChangedEventArgs> action, 
            Action<PropertyChangedEventHandler> removeAction)
            : base((a) => new PropertyChangedEventHandler(a), action, removeAction) { }
    }

    public sealed class WeakPropertyChangedHandler : WeakHandler<PropertyChangedEventArgs, PropertyChangedEventHandler>
    {
        public WeakPropertyChangedHandler(Action<Object, PropertyChangedEventArgs> action, 
            Action<PropertyChangedEventHandler> removeAction)
            : base((a) => new PropertyChangedEventHandler(a), action, removeAction) { }
    }

}
