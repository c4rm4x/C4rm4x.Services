#region Using

using System;
using System.Threading.Tasks;

#endregion

namespace C4rm4x.Services.Framework.Events
{
    /// <summary>
    /// Abstract class that handles all the events of type TEvent
    /// </summary>
    /// <typeparam name="TEvent">Type of the event</typeparam>
    public abstract class AbstractEventHandler<TEvent> : 
        IEventHandler<TEvent> where TEvent : EventData
    {
        /// <summary>
        /// Checks to see whether the event handler can 
        /// </summary>
        /// <param name="type"></param>
        /// <returns>True when this handler can handle an event of given type; false, otherwise</returns>
        public bool CanHandleInstanceOf(Type type)
        {
            return typeof(TEvent).IsAssignableFrom(type);
        }

        /// <summary>
        /// Handles event with given data
        /// </summary>
        /// <param name="eventData">Event data</param>
        public Task OnEventHandlerAsync(object eventData)
        {
            if (!CanHandleInstanceOf(eventData.GetType()))
                throw new ArgumentException(string.Format(
                    "An object of type {0} cannot be handled against this handler",
                    eventData.GetType().FullName));

            return OnEventHandlerAsync((TEvent)eventData);
        }

        /// <summary>
        /// Handles event with given data
        /// </summary>
        /// <param name="eventData">Event data</param>
        public abstract Task OnEventHandlerAsync(TEvent eventData);
    }
}
