#region Using

using C4rm4x.Services.Framework.Messaging;
using C4rm4x.Tools.ServiceBus;
using C4rm4x.Tools.Utilities;
using Microsoft.ServiceBus.Messaging;

#endregion

namespace C4rm4x.Services.Messaging.ServiceBus.Internals
{
    internal class ServiceBusBrokeredMessage :
        IBrokeredMessage
    {
        public BrokeredMessage Message { get; private set; }

        public object Content { get; private set; }

        private ServiceBusBrokeredMessage()
        {

        }

        public static ServiceBusBrokeredMessage New(BrokeredMessage message)
        {
            message.NotNull(nameof(message));

            return new ServiceBusBrokeredMessage
            {
                Message = message,
                Content = message.ExtractContent(),
            };
        }

        public static ServiceBusBrokeredMessage New<TContent>(TContent content)
            where TContent : class
        {
            content.NotNull(nameof(content));

            return new ServiceBusBrokeredMessage
            {
                Content = content,
                Message = content.BuildBrokeredMessage()
            };
        }

        public void Abandon()
        {
            Message.Abandon();
        }

        public void Complete()
        {
            Message.Complete();
        }
    }
}
