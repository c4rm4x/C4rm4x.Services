#region Using

using System;
using System.Threading.Tasks;

#endregion

namespace C4rm4x.Services.Framework.Caching
{
    /// <summary>
    /// Service responsible to cache information 
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// Retrieves an object previously stored by key
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>The previously stored object (if any); null, otherwise</returns>
        Task<object> RetrieveAsync(string key);

        /// <summary>
        /// Retrieves an object of type T previously stored by key
        /// </summary>
        /// <typeparam name="T">Type of tye object to retrieve</typeparam>
        /// <param name="key">The key</param>
        /// <returns>The previously stored object (if any); null, otherwise</returns>
        /// <exception cref="InvalidCastException">If object cannot be cast as required type</exception>
        Task<T> RetrieveAsync<T>(string key);

        /// <summary>
        /// Stores an object into the cache with the given key as identifier
        /// </summary>
        /// <typeparam name="T">Type of tye object to store</typeparam>
        /// <param name="key">The key</param>
        /// <param name="objectToStore">The object to store</param>
        /// <param name="expirationTime">How long the object will be stored (-1 if you do not want the object to expire)</param>
        Task StoreAsync<T>(string key, T objectToStore, int expirationTime = 60);
    }
}
