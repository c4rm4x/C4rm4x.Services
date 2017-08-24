#region Using

using System.Threading.Tasks;

#endregion

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
        Task CompleteAsync();

        /// <summary>
        /// Marks the message as failure (re-try availability)
        /// </summary>
        Task AbandonAsync();

        /// <summary>
        /// Gets the content
        /// </summary>
        object Content { get; }
    }
}
