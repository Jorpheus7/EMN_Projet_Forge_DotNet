// -----------------------------------------------------------------------
// <copyright file="ListenerHandlers.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

// http://nroute.codeplex.com/

using System;
using System.Windows;
using System.ComponentModel;

namespace Soat.HappyNet.Silverlight.Library.Components
{

    // for EventHandler

    public sealed class ListenerHandler : ListenerHandler<EventArgs, EventHandler>
    {
        public ListenerHandler(IWeakEventListener listener, Action<EventHandler> removeAction)
            : base((a) => new EventHandler(a), listener, removeAction) { }
    }

    public sealed class WeakListenerHandler : WeakListenerHandler<EventArgs, EventHandler>
    {
        public WeakListenerHandler(IWeakEventListener listener, Action<EventHandler> removeAction)
            : base((a) => new EventHandler(a), listener, removeAction) { }
    }

    // for EventHandler<E>

    public sealed class ListenerHandler<E> : ListenerHandler<E, EventHandler<E>>
       where E : EventArgs
    {
        public ListenerHandler(IWeakEventListener listener, Action<EventHandler<E>> removeAction)
            : base((a) => new EventHandler<E>(a), listener, removeAction) { }
    }

    public sealed class WeakListenerHandler<E> : WeakListenerHandler<E, EventHandler<E>>
        where E : EventArgs
    {
        public WeakListenerHandler(IWeakEventListener listener, Action<EventHandler<E>> removeAction)
            : base((a) => new EventHandler<E>(a), listener, removeAction) { }
    }

    // for Routed Events

    public sealed class RoutedListenerHandler : ListenerHandler<RoutedEventArgs, RoutedEventHandler>
    {
        public RoutedListenerHandler(IWeakEventListener listener, Action<RoutedEventHandler> removeAction)
            : base((a) => new RoutedEventHandler(a), listener, removeAction) { }
    }

    public sealed class WeakRoutedListenerHandler : WeakListenerHandler<RoutedEventArgs, RoutedEventHandler>
    {
        public WeakRoutedListenerHandler(IWeakEventListener listener, Action<RoutedEventHandler> removeAction)
            : base((a) => new RoutedEventHandler(a), listener, removeAction) { }
    }

    // for Property Changed

    public sealed class PropertyChangedListenerHandler : ListenerHandler<PropertyChangedEventArgs, PropertyChangedEventHandler>
    {
        public PropertyChangedListenerHandler(IWeakEventListener listener,
            Action<PropertyChangedEventHandler> removeAction)
            : base((a) => new PropertyChangedEventHandler(a), listener, removeAction) { }
    }

    public sealed class WeakPropertyChangedListenerHandler : WeakListenerHandler<PropertyChangedEventArgs, PropertyChangedEventHandler>
    {
        public WeakPropertyChangedListenerHandler(IWeakEventListener listener,
            Action<PropertyChangedEventHandler> removeAction)
            : base((a) => new PropertyChangedEventHandler(a), listener, removeAction) { }
    }

}
