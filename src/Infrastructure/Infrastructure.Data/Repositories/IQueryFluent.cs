
namespace Infrastructure.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;


    /// <summary>
    /// Represent a proyection with fluent query API for an entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IQueryFluent<TEntity> where TEntity : IObjectState
    {
        /// <summary>
        /// Set the order by expression.
        /// </summary>
        /// <param name="orderBy">The order by.</param>
        /// <returns>This instance of <see cref="IQueryFluent{T}"/> class.</returns>
        IQueryFluent<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);

        /// <summary>
        /// Includes the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>This instance of <see cref="IQueryFluent{T}"/> class.</returns>
        IQueryFluent<TEntity> Include(Expression<Func<TEntity, object>> expression);

        /// <summary>
        /// Selects the result in the page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns>An instance of <see cref="IEnumerable{T}"/> class with the results.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#")]
        IEnumerable<TEntity> SelectPage(int page, int pageSize, out int totalCount);

        /// <summary>
        /// Selects the specified selector.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="selector">The selector.</param>
        /// <returns>An instance of <see cref="IEnumerable{T}"/> class with the results.</returns>
        IEnumerable<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selector = null);

        /// <summary>
        /// Selects all results.
        /// </summary>
        /// <returns>An instance of <see cref="IEnumerable{T}"/> class with the results.</returns>
        IEnumerable<TEntity> Select();

        /// <summary>
        /// Selects the result in asynchronous mode.
        /// </summary>
        /// <returns>An instance of <see cref="Task{T}"/> for the computation.</returns>
        Task<IEnumerable<TEntity>> SelectAsync();

        /// <summary>
        /// Execute the sql query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>An instance of <see cref="IQueryable{T}"/> with the result tree.</returns>
        IQueryable<TEntity> SqlQuery(string query, params object[] parameters);
    }
}