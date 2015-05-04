// -----------------------------------------------------------------------
// <copyright file="ObserverHandlers.cs" company="So@t">
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

    public sealed class ObserverHandler : ObserverHandler<EventArgs, EventHandler>
    {
        public ObserverHandler(IObserver<Event<EventArgs>> observer, Action<EventHandler> removeAction)
            : base((a) => new EventHandler(a), observer, removeAction) { }
    }

    public sealed class WeakObserverHandler : WeakObserverHandler<EventArgs, EventHandler>
    {
        public WeakObserverHandler(IObserver<Event<EventArgs>> observer, Action<EventHandler> removeAction)
            : base((a) => new EventHandler(a), observer, removeAction) { }
    }

    // for EventHandler<E>

    public sealed class ObserverHandler<E> : ObserverHandler<E, EventHandler<E>>
       where E : EventArgs
    {
        public ObserverHandler(IObserver<Event<E>> observer, Action<EventHandler<E>> removeAction)
            : base((a) => new EventHandler<E>(a), observer, removeAction) { }
    }

    public sealed class WeakObserverHandler<E> : WeakObserverHandler<E, EventHandler<E>>
        where E : EventArgs
    {
        public WeakObserverHandler(IObserver<Event<E>> observer, Action<EventHandler<E>> removeAction)
            : base((a) => new EventHandler<E>(a), observer, removeAction) { }
    }

    // for Routed Events

    public sealed class RoutedObserverHandler : ObserverHandler<RoutedEventArgs, RoutedEventHandler>
    {
        public RoutedObserverHandler(IObserver<Event<RoutedEventArgs>> observer, Action<RoutedEventHandler> removeAction)
            : base((a) => new RoutedEventHandler(a), observer, removeAction) { }
    }

    public sealed class WeakRoutedObserverHandler : WeakObserverHandler<RoutedEventArgs, RoutedEventHandler>
    {
        public WeakRoutedObserverHandler(IObserver<Event<RoutedEventArgs>> observer, Action<RoutedEventHandler> removeAction)
            : base((a) => new RoutedEventHandler(a), observer, removeAction) { }
    }

    // for Property Changed

    public sealed class PropertyChangedObserverHandler : ObserverHandler<PropertyChangedEventArgs, PropertyChangedEventHandler>
    {
        public PropertyChangedObserverHandler(IObserver<Event<PropertyChangedEventArgs>> observer, 
            Action<PropertyChangedEventHandler> removeAction)
            : base((a) => new PropertyChangedEventHandler(a), observer, removeAction) { }
    }

    public sealed class WeakPropertyChangedObserverHandler : WeakObserverHandler<PropertyChangedEventArgs, PropertyChangedEventHandler>
    {
        public WeakPropertyChangedObserverHandler(IObserver<Event<PropertyChangedEventArgs>> observer, 
            Action<PropertyChangedEventHandler> removeAction)
            : base((a) => new PropertyChangedEventHandler(a), observer, removeAction) { }
    }

}
