// -----------------------------------------------------------------------
// <copyright file="BubblingEvent.cs" company="So@t">
// (c) Copyright So@t (http://www.soat.fr). All rights reserved.
// This source is subject to the GNU Lesser General Public License.
// See http://happynet.codeplex.com
// </copyright>
// -----------------------------------------------------------------------

// Par Peter Blois

using System;
using System.Collections.Generic;
using System.Windows;

namespace Soat.HappyNet.Silverlight.Library.Components.Mouse {

    /// <summary>
    /// Enum RoutingStrategy
    /// </summary>
	public enum RoutingStrategy {
        /// <summary>
        /// Bulle : passe par toutes les couches en interagissant avec toutes
        /// </summary>
		Bubble,
        /// <summary>
        /// Direct : passe par toutes les couches sans interagir avec elles
        /// </summary>
		Direct,
	}

	/// <summary>
	/// Half-implemented extensible routed event system.
	/// Declare a routed event with the syntax such as:
	/// <example><![CDATA[
	///		public static readonly BubblingEvent<ContextMenuEventArgs> ContextMenuEvent = new BubblingEvent<ContextMenuEventArgs>("ContextMenuEventArgs", RoutingStrategy.Bubble);
	///	]]></example>
	///	
	/// Register a type handler for the event:
	/// <example>
	///		static Page() {
	///			ContextMenuGenerator.ContextMenuEvent.RegisterClassHandler(typeof(Page), Page.HandleContextMenuEvent, false);
	///		}
	/// </example>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class BubblingEvent<T> where T : BubblingEventArgs {

		Dictionary<Type, BubblingEventRegistration<T>> registeredTypes = new Dictionary<Type, BubblingEventRegistration<T>>();

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="name">Nom de l'événement</param>
        /// <param name="routingStrategy">Stratégie de routing</param>
		public BubblingEvent(string name, RoutingStrategy routingStrategy) {
			this.Name = name;
			this.RoutingStrategy = routingStrategy;
		}

        /// <summary>
        /// Nom de l'événement
        /// </summary>
		public string Name { get; private set; }
        /// <summary>
        /// Stratégie de routage
        /// </summary>
		public RoutingStrategy RoutingStrategy { get; private set; }

        /// <summary>
        /// Abonne un handler à l'événement
        /// </summary>
        /// <param name="classType">Type de classe à observer</param>
        /// <param name="handler">Handler</param>
		public void RegisterClassHandler(Type classType, EventHandler<T> handler) {
			this.RegisterClassHandler(classType, handler, false);
		}

        /// <summary>
        /// Abonne un handler à l'événement
        /// </summary>
        /// <param name="classType">Type de classe à observer</param>
        /// <param name="handler">Handler</param>
        /// <param name="handledEventsToo">Gestion événements</param>
		public void RegisterClassHandler(Type classType, EventHandler<T> handler, bool handledEventsToo) {

			if (!registeredTypes.ContainsKey(classType)) {
				registeredTypes[classType] = new BubblingEventRegistration<T>(classType, handler, handledEventsToo);
			}
		}

        /// <summary>
        /// Lève un événement
        /// </summary>
        /// <param name="evt">Evénement</param>
        /// <param name="element">Element source</param>
		public void RaiseEvent(T evt, FrameworkElement element) {
			switch (this.RoutingStrategy) {
				case RoutingStrategy.Bubble:
					this.RaiseBubblingEvent(evt, element);
					break;
				case RoutingStrategy.Direct:
					this.RaiseDirectEvent(evt, element);
					break;
			}

		}

		private void RaiseBubblingEvent(T args, FrameworkElement element) {

			IList<BubblingEventDelegate<T>> delegates = this.GetDelegates(element, true);

			for (int i = 0; i < delegates.Count; ++i)
				delegates[i].Invoke(args);
		}

		private void RaiseDirectEvent(T args, FrameworkElement element) {
			IList<BubblingEventDelegate<T>> delegates = this.GetDelegates(element, false);

			for (int i = delegates.Count - 1; i >= 0; --i)
				delegates[i].Invoke(args);
		}

		private IList<BubblingEventDelegate<T>> GetDelegates(FrameworkElement element, bool walkTree) {
			List<BubblingEventDelegate<T>> delegates = new List<BubblingEventDelegate<T>>();

			while (element != null) {

				Type classType = element.GetType();
				while (classType != typeof(object) && classType != null) {

					BubblingEventRegistration<T> registration;
					if (this.registeredTypes.TryGetValue(classType, out registration))
						delegates.Add(new BubblingEventDelegate<T>(element, registration));

					classType = classType.BaseType;
				}

				element = element.Parent as FrameworkElement;

				if (!walkTree)
					break;
			}

			return delegates;
		}
	}

    /// <summary>
    /// Bubbling event
    /// </summary>
    /// <typeparam name="T">Type à traiter</typeparam>
    /// <param name="sender">Origine</param>
    /// <param name="args">Arguments</param>
	public delegate void BubblingEventHandler<T>(object sender, T args) where T : BubblingEventArgs;

    /// <summary>
    /// Classe d'argument du BubblingEvent
    /// </summary>
	public class BubblingEventArgs : EventArgs {

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="source">Source de l'événement</param>
		public BubblingEventArgs(object source) {
			this.Source = source;
		}

        /// <summary>
        /// Source de l'événement
        /// </summary>
		public object Source { get; private set; }
        /// <summary>
        /// Spécifie si l'événement a été géré ou non
        /// </summary>
		public bool Handled { get; set; }
	}

    /// <summary>
    /// Abonnement au Bubbling Event
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public class BubblingEventRegistration<T> where T : BubblingEventArgs {

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="classType">Type de classe à observer</param>
        /// <param name="handler">Handler</param>
        /// <param name="handledEvents">Evénements gérés</param>
		public BubblingEventRegistration(Type classType, EventHandler<T> handler, bool handledEvents) {
			this.ClassType = classType;
			this.Handler = handler;
			this.HandledEvents = handledEvents;
		}

        /// <summary>
        /// Type de classe
        /// </summary>
		public Type ClassType { get; set; }
        /// <summary>
        /// Handler
        /// </summary>
		public EventHandler<T> Handler { get; set; }
        /// <summary>
        /// Evénements gérés
        /// </summary>
		public bool HandledEvents { get; set; }
	}

    /// <summary>
    /// Delegate du Bubbling Event
    /// </summary>
    /// <typeparam name="T">Type à gérer</typeparam>
	public class BubblingEventDelegate<T> where T : BubblingEventArgs {

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="source">Source de l'événement</param>
        /// <param name="registration">Abonnement</param>
		public BubblingEventDelegate(object source, BubblingEventRegistration<T> registration) {
			this.Source = source;
			this.EventRegistration = registration;
		}

        /// <summary>
        /// Abonnement
        /// </summary>
		public BubblingEventRegistration<T> EventRegistration { get; set; }
        /// <summary>
        /// Source
        /// </summary>
		public object Source { get; set; }

        /// <summary>
        /// Invocation de l'handler
        /// </summary>
        /// <param name="args">Arguments de l'handler</param>
		public void Invoke(T args) {
			if (!args.Handled || this.EventRegistration.HandledEvents) {
				this.EventRegistration.Handler(this.Source, args);
			}
		}
	}
}
