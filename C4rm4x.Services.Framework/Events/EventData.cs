#region Using

using System;

#endregion

namespace C4rm4x.Services.Framework
{
    /// <summary>
    /// Base class for all EventData
    /// </summary>
    public class EventData
    {
        /// <summary>
        /// Gets the ID of the related aggregate root
        /// </summary>
        public object ID { get; internal set; }

        /// <summary>
        /// Gets the timestamp
        /// </summary>
        public DateTime TimeStamp { get; private set; }

        /// <summary>
        /// Base constructor
        /// </summary>
        protected EventData()
        {
            TimeStamp = DateTime.UtcNow;
        }
    }
}
