#region Using

using C4rm4x.Services.Framework.Messaging;
using C4rm4x.Services.Messaging.ServiceBus.Internals;
using C4rm4x.Tools.Utilities;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace C4rm4x.Services.Messaging.ServiceBus
{
    /// <summary>
    /// Implementation of IMessageQueueRetriever using ServiceBus
    /// </summary>
    public class MessageQueueRetriever :
        IMessageQueueRetriever
    {
        private readonly SubscriptionClient _client;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">Subscription client</param>
        public MessageQueueRetriever(
            SubscriptionClient client)
        {
            client.NotNull(nameof(client));

            _client = client;
        }

        /// <summary>
        /// Retrieves the given number of messages
        /// </summary>
        /// <param name="numberOfMessages">Number of messages to retrieve</param>
        /// <param name="seconds">Waiting time</param>
        /// <returns>The messages retrieved</returns>
        public async Task<IEnumerable<IBrokeredMessage>> RetrieveAsync(
            int numberOfMessages = 1, int seconds = 1)
        {
            return Transform(await _client.ReceiveBatchAsync(
                numberOfMessages,
                new TimeSpan(0, 0, seconds)));
        }

        private static IEnumerable<IBrokeredMessage> Transform(
            IEnumerable<BrokeredMessage> sources)
        {
            foreach (var source in sources)
                yield return ServiceBusBrokeredMessage.New(source);
        }
    }
}
