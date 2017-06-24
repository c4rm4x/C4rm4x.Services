#region Using

using System.Collections.Generic;
using System.Linq;

#endregion

namespace C4rm4x.Services.Framework
{
    /// <summary>
    /// Aggregate root (DDD)
    /// </summary>
    public abstract class AggregateRoot<TKey>
    {
        private readonly ICollection<EventData> _events;

        /// <summary>
        /// Gets the aggregate root ID
        /// </summary>
        public abstract TKey ID { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public AggregateRoot()
        {
            _events = new List<EventData>();
        }

        /// <summary>
        /// Apply event
        /// </summary>
        /// <param name="event">The event to be applied</param>
        protected void ApplyEvent(EventData @event)
        {
            _events.Add(@event);
        }

        /// <summary>
        /// Pop all uncommited events
        /// </summary>
        /// <returns>The collection of events pending to be processed</returns>
        public IEnumerable<EventData> FlushEvents()
        {
            var events = _events.ToArray();

            foreach (var @event in events)
            {
                @event.ID = ID;
            }

            _events.Clear();

            return events;
        }
    }
}
