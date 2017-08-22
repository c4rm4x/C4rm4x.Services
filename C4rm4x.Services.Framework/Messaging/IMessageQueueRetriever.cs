#region Using

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace C4rm4x.Services.Framework.Messaging
{
    /// <summary>
    /// Interface for a message queue retriever
    /// </summary>
    public interface IMessageQueueRetriever
    {
        /// <summary>
        /// Retrieve the messages
        /// </summary>
        /// <param name="numberOfMessages">Number of messages to retrieve</param>
        /// <param name="seconds">Waiting time</param>
        /// <returns>The list of messages retrieved</returns>
        Task<IEnumerable<IBrokeredMessage>> RetrieveAsync(int numberOfMessages = 1, int seconds = 1);
    }
}
