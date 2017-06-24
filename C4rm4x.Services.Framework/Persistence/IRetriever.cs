#region Using

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

#endregion

namespace C4rm4x.Services.Framework.Persistence
{
    /// <summary>
    /// Retrieves entities of type TEntity and key TKey
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity</typeparam>
    /// <typeparam name="TKey">Type of the key</typeparam>
    public interface IRetriever<TEntity, TKey>
       where TEntity : class
    {
        /// <summary>
        /// Retrieves the first occurrence of the entity based on predicate
        /// </summary>
        /// <param name="predicate">The predicate</param>
        /// <returns>The first occurence when at leas one instance fulfills the predicate; null, otherwise</returns>
        Task<TEntity> RetrieveAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Retrieves all the instances of type TEntity
        /// </summary>
        /// <returns>All the occurrences of type TEntity</returns>
        Task<IQueryable<TEntity>> RetrieveAllAsync();

        /// <summary>
        /// Retrieves all the instances of type TEntity based on predicate
        /// </summary>
        /// <param name="predicate">The predicate</param>
        /// <returns>All the occurrences of type TEntity that fulfill the predicate</returns>
        Task<IQueryable<TEntity>> RetrieveAllAsync(Expression<Func<TEntity, bool>> predicate);
    }

    /// <summary>
    /// Retrieves entities of type TEntity and key int
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity</typeparam>
    public interface IRetriever<TEntity> : IRetriever<TEntity, int>
        where TEntity : class
    { }
}
