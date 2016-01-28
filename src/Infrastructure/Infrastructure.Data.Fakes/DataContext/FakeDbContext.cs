
namespace Infrastructure.Data.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading;
    using System.Threading.Tasks;
    using Infrastructure.Data;

    /// <summary>
    /// Base class for fake DbContext
    /// </summary>
    public abstract class FakeDbContext : IFakeDbContext
    {
        private readonly Dictionary<Type, object> fakeDbSets;

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeDbContext"/> class.
        /// </summary>
        protected FakeDbContext()
        {
            fakeDbSets = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns>A fake number.</returns>
        public int SaveChanges() 
        { 
            return default(int); 
        }

        /// <summary>
        /// Synchronizes the state of the object.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState
        {
            // no implentation needed, unit tests which uses FakeDbContext since there is no actual database for unit tests, 
            // there is no actual DbContext to sync with, please look at the Integration Tests for test that will run against an actual database.
        }

        /// <summary>
        /// Synchronizes the objects state post commit.
        /// </summary>
        public void SyncObjectsStatePostCommit() 
        { }

        /// <summary>
        /// Get the DB set for the entity type.
        /// </summary>
        /// <typeparam name="T">The entity type.</typeparam>
        /// <returns>The <see cref="DbSet{T}"/></returns>
        public DbSet<T> Set<T>() where T : class
        { 
            return (DbSet<T>)fakeDbSets[typeof(T)]; 
        }

        /// <summary>
        /// Adds the fake DbSet.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TFakeDbSet">The type of the fake DbSet.</typeparam>
        public void AddFakeDbSet<TEntity, TFakeDbSet>()
            where TEntity : class, IObjectState, new()
            where TFakeDbSet : FakeDbSet<TEntity>, IDbSet<TEntity>, new()
        {
            var fakeDbSet = Activator.CreateInstance<TFakeDbSet>();
            fakeDbSets.Add(typeof(TEntity), fakeDbSet);
        }

        /// <summary>
        /// Saves the changes in asynchronous mode.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An instance of <see cref="Task{T}"/> with a fake execution.</returns>
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken) 
        {
            return new Task<int>(() => default(int)); 
        }

        /// <summary>
        /// Saves the changes in asynchronous mode.
        /// </summary>
        /// <returns>An instance of <see cref="Task{T}"/> with a fake execution.</returns>
        public Task<int> SaveChangesAsync() 
        { 
            return new Task<int>(() => default(int)); 
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose() 
        { }
    }
}