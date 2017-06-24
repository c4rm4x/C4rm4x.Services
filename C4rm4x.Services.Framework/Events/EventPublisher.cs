#region Using

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace C4rm4x.Services.Framework.Events
{
    #region Interface

    /// <summary>
    /// Service responsible to publish events to be processed later on
    /// </summary>
    public interface IEventPublisher
    {
        /// <summary>
        /// Publishes all events pending to be processed
        /// </summary>
        /// <param name="events">The events pending to be processed</param>
        Task PublishAllAsync(IEnumerable<EventData> events);
    }

    #endregion

    /// <summary>
    /// Service that implements IEventPublisher
    /// </summary>
    public class EventPublisher : IEventPublisher
    {
        private readonly IEnumerable<IEventHandler> _eventHandlers;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="eventHandlers">The event handlers</param>
        public EventPublisher(IEnumerable<IEventHandler> eventHandlers)
        {
            _eventHandlers = eventHandlers;
        }

        /// <summary>
        /// Publishes all events pending to be processed
        /// </summary>
        /// <param name="events">The events pending to be processed</param>
        public Task PublishAllAsync(IEnumerable<EventData> events)
        {
            return Task.WhenAll(events.SelectMany(@event =>
                _eventHandlers.Where(handler =>
                    handler.CanHandleInstanceOf(@event.GetType()))
                        .Select(handler => handler.OnEventHandlerAsync(@event))));
        }
    }
}
