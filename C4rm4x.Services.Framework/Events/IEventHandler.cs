#region Using

using System;
using System.Threading.Tasks;

#endregion

namespace C4rm4x.Services.Framework.Events
{
    /// <summary>
    /// Interface that handles events of a given type
    /// </summary>
    public interface IEventHandler
    {
        /// <summary>
        /// Handles event with given data
        /// </summary>
        /// <param name="eventData">Event data</param>
        Task OnEventHandlerAsync(object eventData);

        /// <summary>
        /// Checks to see whether the event handler can 
        /// </summary>
        /// <param name="type"></param>
        /// <returns>True when this handler can handle an event of given type; false, otherwise</returns>
        bool CanHandleInstanceOf(Type type);
    }

    /// <summary>
    /// Interface that handles all the events of type TEvent
    /// </summary>
    /// <typeparam name="TEvent">Type of the event</typeparam>
    public interface IEventHandler<TEvent> : IEventHandler
        where TEvent : EventData
    {
        /// <summary>
        /// Handles event with given data
        /// </summary>
        /// <param name="eventData">Event data</param>
        Task OnEventHandlerAsync(TEvent eventData);
    }
}
