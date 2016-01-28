
namespace Infrastructure.Data.Repositories
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Represent a query for an entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IQueryObject<TEntity>
    {
        /// <summary>
        /// Queries this instance.
        /// </summary>
        /// <returns>An instance of  the expression tree of the instance.</returns>
        Expression<Func<TEntity, bool>> Query();


        /// <summary>
        /// Build a tree for the And expression with the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>An instance of  the expression tree of the instance.</returns>
        Expression<Func<TEntity, bool>> And(Expression<Func<TEntity, bool>> query);

        /// <summary>
        /// Build a tree for the Or expression with the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>An instance of  the expression tree of the instance.</returns>
        Expression<Func<TEntity, bool>> Or(Expression<Func<TEntity, bool>> query);

        /// <summary>
        ///Build a tree for the And expression with the specified query object.
        /// </summary>
        /// <param name="queryObject">The query object.</param>
        /// <returns>An instance of  the expression tree of the instance.</returns>
        Expression<Func<TEntity, bool>> And(IQueryObject<TEntity> queryObject);

        /// <summary>
        /// Build a tree for the Or expression with the specified query object.
        /// </summary>
        /// <param name="queryObject">The query object.</param>
        /// <returns>An instance of  the expression tree of the instance.</returns>
        Expression<Func<TEntity, bool>> Or(IQueryObject<TEntity> queryObject);
    }
}