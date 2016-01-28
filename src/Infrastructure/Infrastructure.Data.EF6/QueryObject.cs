

namespace Infrastructure.Data.Ef6
{
    using System;
    using System.Linq.Expressions;
    using Infrastructure.Data.Repositories;

    /// <summary>
    /// Represent a query for an entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public abstract class QueryObject<TEntity> : IQueryObject<TEntity>
    {
        private Expression<Func<TEntity, bool>> predicate;

        /// <summary>
        /// Queries this instance.
        /// </summary>
        /// <returns>An instance of  the expression tree of the instance.</returns>
        public virtual Expression<Func<TEntity, bool>> Query()
        {
            return this.predicate;
        }

        /// <summary>
        /// Build a tree for the And expression with the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>An instance of  the expression tree of the instance.</returns>
        ////[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "query")]
        public Expression<Func<TEntity, bool>> And(Expression<Func<TEntity, bool>> query)
        {
            return this.predicate = this.predicate == null ? PredicateBuilder.True<TEntity>().And(query): this.predicate.And(query);
        }

        /// <summary>
        /// Build a tree for the Or expression with the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>An instance of  the expression tree of the instance.</returns>
        ////[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "query")]
        public Expression<Func<TEntity, bool>> Or(Expression<Func<TEntity, bool>> query)
        {
            return this.predicate = this.predicate == null ? PredicateBuilder.False<TEntity>().Or(query) : this.predicate.Or(query);
        }

        /// <summary>
        /// Ands the specified query object.
        /// </summary>
        /// <param name="queryObject">The query object.</param>
        /// <returns>An instance of  the expression tree of the instance.</returns>
        ////[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public Expression<Func<TEntity, bool>> And(IQueryObject<TEntity> queryObject)
        {
            return this.And(queryObject.Query());
        }

        /// <summary>
        /// Ors the specified query object.
        /// </summary>
        /// <param name="queryObject">The query object.</param>
        /// <returns>An instance of  the expression tree of the instance.</returns>
        ////[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public Expression<Func<TEntity, bool>> Or(IQueryObject<TEntity> queryObject)
        {
            return this.Or(queryObject.Query());
        }
    }
}