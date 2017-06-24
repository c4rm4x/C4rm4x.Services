#region Using

using C4rm4x.Services.Framework.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

#endregion

namespace C4rm4x.Services.Persistence.EF
{
    /// <summary>
    /// Base class that implements IRetriever
    /// </summary>
    /// <typeparam name="TEntity">Type of the entities</typeparam>
    /// <typeparam name="TKey">Type of entity keys</typeparam>
    /// <typeparam name="TContext">DBContext</typeparam>
    public abstract class BaseRetriever<TEntity, TKey, TContext> :
        IRetriever<TEntity, TKey>
        where TEntity : class
        where TContext : DbContext
    {
        private readonly DbSet<TEntity> _set;
        private readonly DbContext _entities;
   
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="entities">The DB context</param>
        public BaseRetriever(TContext entities)
        {
            _entities = entities;
            _set = entities.Set<TEntity>();
        }

        /// <summary>
        /// Retrieves all the instances of type TEntity
        /// </summary>
        /// <returns>All the occurrences of type TEntity</returns>
        public Task<IQueryable<TEntity>> RetrieveAllAsync()
        {
            return Task.FromResult(_set.AsNoTracking().AsQueryable());
        }

        /// <summary>
        /// Retrieves all the instances of type TEntity based on predicate
        /// </summary>
        /// <param name="predicate">The predicate</param>
        /// <returns>All the occurrences of type TEntity that fulfill the predicate</returns>
        public Task<IQueryable<TEntity>> RetrieveAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(_set.AsNoTracking().Where(predicate).AsQueryable());
        }

        /// <summary>
        /// Retrieves the first occurrence of the entity based on predicate
        /// </summary>
        /// <param name="predicate">The predicate</param>
        /// <returns>The first occurence when at leas one instance fulfills the predicate; null, otherwise</returns>
        public Task<TEntity> RetrieveAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _set.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Executes the SP that returns a collection of entities of given type
        /// </summary>
        /// <param name="queryName">SQL command or store procedure name</param>
        /// <param name="parameters">The parameters</param>
        /// <returns>A collection of TEntity returned by the SQL command</returns>
        public Task<List<TEntity>> ExecuteQueryAsync(
            string queryName, params SqlParameter[] parameters)
        {
            return _set.SqlQuery(BuildQuery(queryName, parameters)).AsNoTracking().ToListAsync();
        }

        private static string BuildQuery(string queryName, SqlParameter[] parameters)
        {
            return string.Format("exec {0} {1}", queryName,
                string.Join(",", parameters.Select(p =>
                    string.Format("@{0} {1}", p.ParameterName, GetDirection(p))))).Trim();
        }

        private static string GetDirection(SqlParameter parameter)
        {
            return parameter.Direction == ParameterDirection.Output
                ? "out"
                : string.Empty;
        }
    }

    /// <summary>
    /// Base class that implements IRetriever
    /// </summary>
    /// <typeparam name="TEntity">Type of the entities</typeparam>
    /// <typeparam name="TContext">DBContext</typeparam>
    public abstract class BaseRetriever<TEntity, TContext> : 
        BaseRetriever<TEntity, int, TContext>
        where TEntity : class
        where TContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="entities">The DB context</param>
        public BaseRetriever(TContext entities) : base(entities)
        {

        }
    }
}
