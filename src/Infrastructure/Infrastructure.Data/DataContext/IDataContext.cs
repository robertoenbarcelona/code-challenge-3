
namespace Infrastructure.Data.DataContext
{
    using System;
    using System.Data.Entity;
        /// <summary>
    /// Represents a data context technology agnostic
    /// </summary>
    public interface IDataContext : IDisposable
    {
        /// <summary>
        /// Get the dataset for the entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        DbSet<T> Set<T>() where T : class;

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns>The number of objects written to the underlying database.</returns>
        int SaveChanges();

        /// <summary>
        /// Synchronizes the state of the object.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState;

        /// <summary>
        /// Synchronizes the objects state post commit.
        /// </summary>
        void SyncObjectsStatePostCommit();
    }
}