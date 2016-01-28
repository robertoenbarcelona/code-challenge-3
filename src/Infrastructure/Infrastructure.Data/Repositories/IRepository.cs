
namespace Infrastructure.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Represent a repository for the entity type.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IRepository<TEntity> where TEntity : class, IObjectState
    {
        /// <summary>
        /// Finds the specified key values.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns>The entity instance.</returns>TEntity Find(params object[] keyValues);
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
        /// 
        IQueryFluent<TEntity> Query();

        /// <summary>
        /// Build the query object for this repository.
        /// </summary>
        /// <param name="queryObject">The query object.</param>
        /// <returns>
        /// An instance of the <see cref="IQueryFluent{T}" /> with the expression tree.
        /// </returns>
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
        /// Gets the repository.
        /// </summary>
        /// <typeparam name="T">the entity type.</typeparam>
        /// <returns>The instance of <see cref="IRepository{T}"/> in the UoW.</returns>
        IRepository<T> GetRepository<T>() where T : class, IObjectState;
    }
}