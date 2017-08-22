#region Using

using C4rm4x.Services.Framework.Messaging;
using C4rm4x.Tools.ServiceBus;
using C4rm4x.Tools.Utilities;
using Microsoft.ServiceBus.Messaging;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace C4rm4x.Services.Messaging.ServiceBus
{
    /// <summary>
    /// Implementation of IMessageQueueSender using ServiceBus
    /// </summary>
    /// <typeparam name="TContent">Type of the messages to send</typeparam>
    public class MessageQueueSender<TContent> :
        IMessageQueueSender<TContent>
        where TContent : class
    {
        private readonly TopicClient _client;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client">The topic client</param>
        public MessageQueueSender(
            TopicClient client)
        {
            client.NotNull(nameof(client));

            _client = client;
        }

        /// <summary>
        /// Sends a collection of messages as a batch
        /// </summary>
        /// <param name="messages">The messages</param>
        public Task SendAsync(IEnumerable<TContent> messages)
        {
            messages.NotNullOrEmpty(nameof(messages));

            return _client.SendBatchAsync(Transform(messages));
        }

        private static IEnumerable<BrokeredMessage> Transform(
            IEnumerable<TContent> messages)
        {
            foreach (var message in messages)
                yield return messages.BuildBrokeredMessage();
        }

        /// <summary>
        /// Sends a new message
        /// </summary>
        /// <param name="message">The message</param>
        public Task SendAsync(TContent message)
        {
            message.NotNull(nameof(message));

            return _client.SendAsync(Transform(message));
        }

        private static BrokeredMessage Transform(TContent message)
        {
            return message.BuildBrokeredMessage();
        }
    }
}
