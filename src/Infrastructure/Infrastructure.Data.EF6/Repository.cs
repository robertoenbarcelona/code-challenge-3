

namespace Infrastructure.Data.Ef6
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Infrastructure.Data.DataContext;
    using Infrastructure.Data.Repositories;
    using Infrastructure.Data.UnitOfWork;

    /// <summary>
    /// Represent a repository for the entity type.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class Repository<TEntity> : IRepositoryAsync<TEntity> where TEntity : class, IObjectState
    {
        private readonly IDataContextAsync context;
        private readonly IUnitOfWorkAsync unitOfWork;
        private HashSet<object> entitesChecked; // tracking of all process entities in the object graph when calling SyncObjectGraph

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public Repository(IDataContextAsync context, IUnitOfWorkAsync unitOfWork)
        {
            this.context = context;
            this.unitOfWork = unitOfWork;
        }

        private DbSet<TEntity> DbSet { get { return this.context.Set<TEntity>(); } }

        /// <summary>
        /// Finds the specified key values.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns>The entity instance.</returns>
        /// <exception cref="InvalidOperationException">Thrown if multiple entities exist in the context with the primary key values.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the type of entity is not part of the data model for this context.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the types of the key values do not match the types of the key values for the entity type to be found.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the context has been disposed.</exception>
        public virtual TEntity Find(params object[] keyValues)
        {
            return this.DbSet.Find(keyValues);
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Insert(TEntity entity)
        {
            entity.ObjectState = ObjectState.Added;
            this.DbSet.Attach(entity);
            this.context.SyncObjectState(entity);
        }

        /// <summary>
        /// Inserts the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            if (entities == null) { throw new ArgumentNullException("entities"); }
            foreach (var entity in entities)
            {
                this.Insert(entity);
            }
        }

        /// <summary>
        /// Inserts the graph range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void InsertGraphRange(IEnumerable<TEntity> entities)
        {
            this.DbSet.AddRange(entities);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Update(TEntity entity)
        {
            entity.ObjectState = ObjectState.Modified;
            this.DbSet.Attach(entity);
            this.context.SyncObjectState(entity);
        }

        /// <summary>
        /// Inserts the or update graph.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void InsertOrUpdateGraph(TEntity entity)
        {
            SyncObjectGraph(entity);
            entitesChecked = null;
            this.DbSet.Attach(entity);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public virtual void Delete(object id)
        {
            var entity = this.DbSet.Find(id);
            this.Delete(entity);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Delete(TEntity entity)
        {
            entity.ObjectState = ObjectState.Deleted;
            this.DbSet.Attach(entity);
            this.context.SyncObjectState(entity);
        }

        /// <summary>
        /// Selects the entities satisfying the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>An instance of <see cref="IQueryable{T}"/> with the result tree.</returns>
        public virtual IQueryable<TEntity> SelectQuery(string query, params object[] parameters)
        {
            return this.DbSet.SqlQuery(query, parameters).AsQueryable();
        }

        /// <summary>
        /// Build the query object for this repository.
        /// </summary>
        /// <returns>An instance of the <see cref="IQueryFluent{T}"/> with the expression tree.</returns>
        public IQueryFluent<TEntity> Query()
        {
            return new QueryFluent<TEntity>(this);
        }

        /// <summary>
        /// Build the query object for this repository.
        /// </summary>
        /// <param name="queryObject">The query object.</param>
        /// <returns>An instance of the <see cref="IQueryFluent{T}"/> with the expression tree.</returns>
        public virtual IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject)
        {
            return new QueryFluent<TEntity>(this, queryObject);
        }

        /// <summary>
        /// Build the query object for this repository.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>An instance of the <see cref="IQueryFluent{T}"/> with the expression tree.</returns>
        public virtual IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query)
        {
            return new QueryFluent<TEntity>(this, query);
        }

        /// <summary>
        /// Get the queriable interface of this repository.
        /// </summary>
        /// <returns>An instance of the <see cref="IQueryable{T}"/>.</returns>
        public IQueryable<TEntity> Queryable()
        {
            return this.DbSet;
        }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <typeparam name="T">the entity type.</typeparam>
        /// <returns>The instance of <see cref="IRepository{T}"/> in the UoW.</returns>
        public IRepository<T> GetRepository<T>() where T : class, IObjectState
        {
            return this.unitOfWork.Repository<T>();
        }

        /// <summary>
        /// Finds the entity in asynchronous mode.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns>An instance of <see cref="Task{T}"/> with the execution.</returns>
        public virtual async Task<TEntity> FindAsync(params object[] keyValues)
        {
            return await this.DbSet.FindAsync(keyValues);
        }

        /// <summary>
        /// Finds the entity in asynchronous mode.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="keyValues">The key values.</param>
        /// <returns>An instance of <see cref="Task{T}"/> with the execution.</returns>
        public virtual async Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return await this.DbSet.FindAsync(cancellationToken, keyValues);
        }

        /// <summary>
        /// Deletes the entity in asynchronous mode.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns>An instance of <see cref="Task{T}"/> with the execution.</returns>
        public virtual async Task<bool> DeleteAsync(params object[] keyValues)
        {
            return await DeleteAsync(CancellationToken.None, keyValues);
        }

        /// <summary>
        /// Deletes the entity in asynchronous mode.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="keyValues">The key values.</param>
        /// <returns>An instance of <see cref="Task{T}"/> with the execution.</returns>
        public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            var entity = await FindAsync(cancellationToken, keyValues);

            if (entity == null)
            {
                return false;
            }

            entity.ObjectState = ObjectState.Deleted;
            this.DbSet.Attach(entity);

            return true;
        }

        /// <summary>
        /// Selects the entities satisfaying the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includes">The includes.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>An instance of the <see cref="IQueryable{T}"/> expression.</returns>
        internal IQueryable<TEntity> Select(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = this.DbSet;
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return query;
        }

        /// <summary>
        /// Selects the entities satisfaying the specified filter in asynchronous mode.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includes">The includes.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>An instance of the <see cref="IQueryable{T}"/> expression.</returns>
        internal async Task<IEnumerable<TEntity>> SelectAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? page = null,
            int? pageSize = null)
        {
            return await Select(filter, orderBy, includes, page, pageSize).ToListAsync();
        }


        private void SyncObjectGraph(object entity)
        {
            if (entitesChecked == null) { entitesChecked = new HashSet<object>(); }
            if (entitesChecked.Contains(entity)) { return; }

            entitesChecked.Add(entity);
            var objectStateEntity = entity as IObjectState;
            if (objectStateEntity != null && objectStateEntity.ObjectState == ObjectState.Added) { this.context.SyncObjectState(objectStateEntity); }

            // Set tracking state for child collections
            foreach (var prop in entity.GetType().GetProperties())
            {
                // Apply changes to 1-1 and M-1 properties
                var trackableRef = prop.GetValue(entity, null) as IObjectState;
                if (trackableRef != null)
                {
                    if (trackableRef.ObjectState == ObjectState.Added) { this.context.SyncObjectState(trackableRef); }
                    SyncObjectGraph(prop.GetValue(entity, null));
                }

                // Apply changes to 1-M properties
                var items = prop.GetValue(entity, null) as IEnumerable<IObjectState>;
                if (items == null) { continue; }

                Debug.WriteLine("Checking collection: " + prop.Name);
                foreach (var item in items) { SyncObjectGraph(item); }
            }
        }
    }
}