

namespace Infrastructure.Data.Ef6
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Infrastructure.Data.Repositories;

    /// <summary>
    /// An Entity Framewrok proyection with fluent query API for an entity
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public sealed class QueryFluent<TEntity> : IQueryFluent<TEntity> where TEntity : class, IObjectState
    {

        private readonly Expression<Func<TEntity, bool>> expression;
        private readonly List<Expression<Func<TEntity, object>>> includes;
        private readonly Repository<TEntity> repository;
        private Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryFluent{TEntity}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public QueryFluent(Repository<TEntity> repository)
        {
            this.repository = repository;
            this.includes = new List<Expression<Func<TEntity, object>>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryFluent{TEntity}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="queryObject">The query object.</param>
        public QueryFluent(Repository<TEntity> repository, IQueryObject<TEntity> queryObject) : this(repository) 
        {
            this.expression = queryObject.Query();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryFluent{TEntity}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="expression">The expression.</param>
        public QueryFluent(Repository<TEntity> repository, Expression<Func<TEntity, bool>> expression) : this(repository) 
        { 
            this.expression = expression; 
        }

        /// <summary>
        /// Set the order by expression.
        /// </summary>
        /// <param name="orderBy">The order by.</param>
        /// <returns>This instance of <see cref="IQueryFluent{T}"/> class.</returns>
        public IQueryFluent<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            this.orderBy = orderBy;
            return this;
        }

        /// <summary>
        /// Includes the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>This instance of <see cref="IQueryFluent{T}"/> class.</returns>
        public IQueryFluent<TEntity> Include(Expression<Func<TEntity, object>> expression)
        {
            this.includes.Add(expression);
            return this;
        }

        /// <summary>
        /// Selects the result in the page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns>An instance of <see cref="IEnumerable{T}"/> class with the results.</returns>
        public IEnumerable<TEntity> SelectPage(int page, int pageSize, out int totalCount)
        {
            totalCount = this.repository.Select(this.expression).Count();
            return this.repository.Select(this.expression, this.orderBy, this.includes, page, pageSize);
        }

        /// <summary>
        /// Selects the specified selector.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="selector">The selector.</param>
        /// <returns>An instance of <see cref="IEnumerable{T}"/> class with the results.</returns>
        public IEnumerable<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selector) 
        {
            return this.repository.Select(this.expression, this.orderBy, this.includes).Select(selector); 
        }

        /// <summary>
        /// Selects all results.
        /// </summary>
        /// <returns>An instance of <see cref="IEnumerable{T}"/> class with the results.</returns>
        public IEnumerable<TEntity> Select() 
        {
            return this.repository.Select(this.expression, this.orderBy, this.includes); 
        }

        /// <summary>
        /// Selects the result in asynchronous mode.
        /// </summary>
        /// <returns>An instance of <see cref="Task{T}"/> for the computation.</returns>
        public async Task<IEnumerable<TEntity>> SelectAsync() 
        {
            return await this.repository.SelectAsync(this.expression, this.orderBy, this.includes); 
        }

        /// <summary>
        /// Execute the sql query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>An instance of <see cref="IQueryable{T}"/> with the result tree.</returns>
        public IQueryable<TEntity> SqlQuery(string query, params object[] parameters) 
        {
            return this.repository.SelectQuery(query, parameters).AsQueryable(); 
        }
    }
}