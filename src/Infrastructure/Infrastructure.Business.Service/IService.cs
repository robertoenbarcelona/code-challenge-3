

namespace Infrastructure.Business.Service
{
    using Infrastructure.Data;
    using Infrastructure.Data.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represent a date service for an entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IService<TEntity> where TEntity : class, IObjectState
    {
        /// <summary>
        /// Finds the specified key values.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns>The entity instance.</returns>
        TEntity Find(params object[] keyValues);

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Inserts the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void InsertRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Inserts the graph range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void InsertGraphRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Inserts the or update graph.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void InsertOrUpdateGraph(TEntity entity);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Delete(object id);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Selects the entities satisfying the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>An instance of <see cref="IQueryable{T}"/> with the result tree.</returns>
        IQueryable<TEntity> SelectQuery(string query, params object[] parameters);

        /// <summary>
        /// Build the query object for this repository.
        /// </summary>
        /// <returns>An instance of the <see cref="IQueryFluent{T}"/> with the expression tree.</returns>
        IQueryFluent<TEntity> Query();

        /// <summary>
        /// Build the query object for this repository.
        /// </summary>
        /// <param name="queryObject">The query object.</param>
        /// <returns>An instance of the <see cref="IQueryFluent{T}"/> with the expression tree.</returns>
        IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject);

        /// <summary>
        /// Build the query object for this repository.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>An instance of the <see cref="IQueryFluent{T}"/> with the expression tree.</returns>
        IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query);

        /// <summary>
        /// Get the queriable interface of this repository.
        /// </summary>
        /// <returns>An instance of the <see cref="IQueryable{T}"/>.</returns>
        IQueryable<TEntity> Queryable();

        /// <summary>
        /// Finds the entity in asynchronous mode.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns>An instance of <see cref="Task{T}"/> with the execution.</returns>
        Task<TEntity> FindAsync(params object[] keyValues);

        /// <summary>
        /// Finds the entity in asynchronous mode.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="keyValues">The key values.</param>
        /// <returns>An instance of <see cref="Task{T}"/> with the execution.</returns>
        Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);

        /// <summary>
        /// Deletes the entity in asynchronous mode.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns>An instance of <see cref="Task{T}"/> with the execution.</returns>
        Task<bool> DeleteAsync(params object[] keyValues);

        /// <summary>
        /// Deletes the entity in asynchronous mode.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="keyValues">The key values.</param>
        /// <returns>An instance of <see cref="Task{T}"/> with the execution.</returns>
        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);
    }
}