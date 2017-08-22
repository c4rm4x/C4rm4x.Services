#region Using

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace C4rm4x.Services.Framework.Messaging
{
    /// <summary>
    /// Interface for a message queue sender
    /// </summary>
    /// <typeparam name="TContent">Type of the messages to send</typeparam>
    public interface IMessageQueueSender<TContent> where TContent : class
    {
        /// <summary>
        /// Send a message
        /// </summary>
        /// <param name="message">The message to send</param>
        Task SendAsync(TContent message);

        /// <summary>
        /// Sends a collection of messages as a batch
        /// </summary>
        /// <param name="messages">The collection of messages to send</param>
        Task SendAsync(IEnumerable<TContent> messages);
    }
}
