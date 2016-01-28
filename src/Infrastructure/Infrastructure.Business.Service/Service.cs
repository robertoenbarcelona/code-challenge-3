
namespace Infrastructure.Business.Service
{
    using Infrastructure.Core.Exceptions;
    using Infrastructure.Data;
    using Infrastructure.Data.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;


    /// <summary>
    /// Represent a date service for an entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public abstract class Service<TEntity> : IService<TEntity> where TEntity : class, IObjectState
    {
        private readonly IRepositoryAsync<TEntity> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="Service{TEntity}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        protected Service(IRepositoryAsync<TEntity> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        protected IRepositoryAsync<TEntity> Repository { get { return this.repository; } }

        /// <summary>
        /// Finds the specified key values.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns>
        /// The entity instance.
        /// </returns>
        /// <exception cref="NoDataFoundException">Thown when entity type is not in context, or params are not from expected type or there are more entities for thre selected key.</exception>
        /// <exception cref="DistribuitedException">Thown when underline data system fails.</exception>
        public virtual TEntity Find(params object[] keyValues)
        {
            TEntity result;
            try
            {
                result = this.repository.Find(keyValues);
            }
            catch (InvalidOperationException ex)
            {
                var parms = new StringBuilder("Params=[");
                keyValues.ToList().ForEach((x) => { parms.Append(x.ToString()).Append("|"); });
                parms.Remove(parms.Length - 1, 1);
                parms.Append("]");
                throw new NoDataFoundException(Infrastructure.Resources.Exceptions.CannotRetrieveData, ex, parms.ToString(), typeof(TEntity).ToString());
            }
            catch (Exception ex)
            {
                throw new DistribuitedException(Infrastructure.Resources.Exceptions.CannotRetrieveData, ex);
            }
            return result;
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Insert(TEntity entity)
        {
            this.repository.Insert(entity);
        }

        /// <summary>
        /// Inserts the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            this.repository.InsertRange(entities);
        }

        /// <summary>
        /// Inserts the or update graph.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void InsertOrUpdateGraph(TEntity entity)
        {
            this.repository.InsertOrUpdateGraph(entity);
        }

        /// <summary>
        /// Inserts the graph range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void InsertGraphRange(IEnumerable<TEntity> entities)
        {
            this.repository.InsertGraphRange(entities);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Update(TEntity entity)
        {
            this.repository.Update(entity);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public virtual void Delete(object id)
        {
            this.repository.Delete(id);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(TEntity entity)
        {
            this.repository.Delete(entity);
        }

        /// <summary>
        /// Selects the entities satisfying the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// An instance of <see cref="IQueryable{T}" /> with the result tree.
        /// </returns>
        public virtual IQueryable<TEntity> SelectQuery(string query, params object[] parameters)
        {
            return this.repository.SelectQuery(query, parameters).AsQueryable();
        }

        /// <summary>
        /// Build the query object for this repository.
        /// </summary>
        /// <returns>
        /// An instance of the <see cref="IQueryFluent{T}" /> with the expression tree.
        /// </returns>
        public IQueryFluent<TEntity> Query()
        {
            return this.repository.Query();
        }

        /// <summary>
        /// Build the query object for this repository.
        /// </summary>
        /// <param name="queryObject"></param>
        /// <returns>
        /// An instance of the <see cref="IQueryFluent{T}" /> with the expression tree.
        /// </returns>
        public virtual IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject)
        {
            return this.repository.Query(queryObject);
        }

        /// <summary>
        /// Build the query object for this repository.
        /// </summary>
        /// <param name="query"></param>
        /// <returns>
        /// An instance of the <see cref="IQueryFluent{T}" /> with the expression tree.
        /// </returns>
        public virtual IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query)
        {
            return this.repository.Query(query);
        }

        /// <summary>
        /// Get the queriable interface of this repository.
        /// </summary>
        /// <returns>
        /// An instance of the <see cref="IQueryable{T}" />.
        /// </returns>
        public IQueryable<TEntity> Queryable()
        {
            return this.repository.Queryable();
        }

        /// <summary>
        /// Finds the entity in asynchronous mode.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns>
        /// An instance of <see cref="Task{T}" /> with the execution.
        /// </returns>
        /// <exception cref="NoDataFoundException">Thown when entity type is not in context, or params are not from expected type or there are more entities for thre selected key.</exception>
        /// <exception cref="DistribuitedException">Thown when underline data system fails.</exception>
        public virtual async Task<TEntity> FindAsync(params object[] keyValues)
        {
            Task<TEntity> result;
            try
            {
                result = this.repository.FindAsync(keyValues);
            }
            catch (InvalidOperationException ex)
            {
                var parms = new StringBuilder("Params=[");
                keyValues.ToList().ForEach((x) => { parms.Append(x.ToString()).Append("|"); });
                parms.Remove(parms.Length - 1, 1);
                parms.Append("]");
                throw new NoDataFoundException(Infrastructure.Resources.Exceptions.CannotRetrieveData, ex, parms.ToString(), typeof(TEntity).ToString());
            }
            catch (Exception ex)
            {
                throw new DistribuitedException(Infrastructure.Resources.Exceptions.CannotRetrieveData, ex);
            }
            return await result;
        }

        /// <summary>
        /// Finds the entity in asynchronous mode.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="keyValues">The key values.</param>
        /// <returns>
        /// An instance of <see cref="Task{T}" /> with the execution.
        /// </returns>
        /// <exception cref="NoDataFoundException">Thown when entity type is not in context, or params are not from expected type or there are more entities for thre selected key.</exception>
        /// <exception cref="DistribuitedException">Thown when underline data system fails.</exception>
        public virtual async Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            Task<TEntity> result;
            try
            {
                result = this.repository.FindAsync(cancellationToken, keyValues);
            }
            catch (InvalidOperationException ex)
            {
                var parms = new StringBuilder("Params=[");
                keyValues.ToList().ForEach((x) => { parms.Append(x.ToString()).Append("|"); });
                parms.Remove(parms.Length - 1, 1);
                parms.Append("]");
                throw new NoDataFoundException(Infrastructure.Resources.Exceptions.CannotRetrieveData, ex, parms.ToString(), typeof(TEntity).ToString());
            }
            catch (Exception ex)
            {
                throw new DistribuitedException(Infrastructure.Resources.Exceptions.CannotRetrieveData, ex);
            }
            return await result;
        }

        /// <summary>
        /// Deletes the entity in asynchronous mode.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns>
        /// An instance of <see cref="Task{T}" /> with the execution.
        /// </returns>
        public virtual async Task<bool> DeleteAsync(params object[] keyValues)
        {
            return await DeleteAsync(CancellationToken.None, keyValues);
        }

        /// <summary>
        /// Deletes the entity in asynchronous mode.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="keyValues">The key values.</param>
        /// <returns>
        /// An instance of <see cref="Task{T}" /> with the execution.
        /// </returns>
        public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return await this.repository.DeleteAsync(cancellationToken, keyValues);
        }
    }
}