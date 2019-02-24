﻿using System;
using System.Collections.Generic;
using Intellias.CQRS.Core.Events;

namespace Intellias.CQRS.Core.Domain
{
    /// <summary>
    /// Aggregate State
    /// </summary>
    public abstract class AggregateState
    {
        private readonly Dictionary<Type, Action<IEvent>> handlers = new Dictionary<Type, Action<IEvent>>();

        /// <summary>
        /// Version of aggregate state
        /// </summary>
        public int Version { get; private set; }

        /// <summary>
        /// Configures a handler method for an event.
        /// </summary>
        /// <param name="handler">todo: describe handler parameter on Handles</param>
        protected void Handles<TEvent>(Action<TEvent> handler)
            where TEvent : IEvent
        {
            handlers.Add(typeof(TEvent), @event => handler?.Invoke((TEvent)@event));
        }

        /// <summary>
        /// Apply an event
        /// </summary>
        /// <param name="event">Event</param>
        public void ApplyEvent(IEvent @event)
        {
            @event.Version = ++Version;
            handlers[@event.GetType()].Invoke(@event);
        }
    }
}
