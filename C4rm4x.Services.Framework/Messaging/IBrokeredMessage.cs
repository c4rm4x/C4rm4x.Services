namespace C4rm4x.Services.Framework.Messaging
{
    /// <summary>
    /// Interface that represents a brokered message
    /// </summary>
    public interface IBrokeredMessage
    {
        /// <summary>
        /// Marks the message as complete
        /// </summary>
        void Complete();

        /// <summary>
        /// Marks the message as failure (re-try availability)
        /// </summary>
        void Abandon();

        /// <summary>
        /// Gets the content
        /// </summary>
        object Content { get; }
    }
}
