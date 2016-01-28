
namespace Infrastructure.Data.Fakes
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public abstract class FakeDbSet<TEntity> : DbSet<TEntity>, IDbSet<TEntity> where TEntity : class, IObjectState, new()
    {
        private readonly ObservableCollection<TEntity> items;
        private readonly IQueryable query;

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeDbSet{TEntity}"/> class.
        /// </summary>
        protected FakeDbSet()
        {
            items = new ObservableCollection<TEntity>();
            query = items.AsQueryable();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() { return items.GetEnumerator(); }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<TEntity> GetEnumerator() { return items.GetEnumerator(); }

        /// <summary>
        /// Gets the expression tree that is associated with the instance of <see cref="T:System.Linq.IQueryable" />.
        /// </summary>
        public Expression Expression { get { return query.Expression; } }

        /// <summary>
        /// Gets the type of the element(s) that are returned when the expression tree associated with this instance of <see cref="T:System.Linq.IQueryable" /> is executed.
        /// </summary>
        public Type ElementType { get { return query.ElementType; } }

        /// <summary>
        /// Gets the query provider that is associated with this data source.
        /// </summary>
        public IQueryProvider Provider { get { return query.Provider; } }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public override TEntity Add(TEntity entity)
        {
            items.Add(entity);
            return entity;
        }

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public override TEntity Remove(TEntity entity)
        {
            items.Remove(entity);
            return entity;
        }

        /// <summary>
        /// Attaches the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">entity</exception>
        public override TEntity Attach(TEntity entity)
        {
            switch (entity.ObjectState)
            {
                case ObjectState.Modified:
                    items.Remove(entity);
                    items.Add(entity);
                    break;
                
                case ObjectState.Deleted:
                    items.Remove(entity);
                    break;
                
                case ObjectState.Unchanged:
                case ObjectState.Added:
                    items.Add(entity);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException("entity");
            }
            return entity;
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public override TEntity Create() { return new TEntity(); }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <typeparam name="TDerivedEntity">The type of the derived entity.</typeparam>
        /// <returns></returns>
        public override TDerivedEntity Create<TDerivedEntity>() { return Activator.CreateInstance<TDerivedEntity>(); }

        /// <summary>
        /// Gets the local.
        /// </summary>
        /// <value>
        /// The local.
        /// </value>
        public override ObservableCollection<TEntity> Local { get { return items; } }
    }
}