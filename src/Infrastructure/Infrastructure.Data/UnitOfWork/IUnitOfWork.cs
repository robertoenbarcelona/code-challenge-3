
namespace Infrastructure.Data.UnitOfWork
{
    using System;
    using System.Data;
    using Infrastructure.Data.Repositories;

    /// <summary>
    /// Represent a business transaction.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns>The number of entity written to the storage.</returns>
        int SaveChanges();

        /// <summary>
        /// Get the repository active in this transaction.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>An instance of <see cref="IRepository{T}"/>.</returns>
        IRepository<TEntity> Repository<TEntity>() where TEntity : class, IObjectState;

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        /// <param name="isolationLevel">The isolation level.</param>
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);

        /// <summary>
        /// Commits this instance.
        /// </summary>
        /// <returns></returns>
        bool Commit();

        /// <summary>
        /// Rollbacks this instance.
        /// </summary>
        void Rollback();

        /// <summary>
        /// Hooks the specified write.
        /// </summary>
        /// <param name="write">The write.</param>
        void Hook(Action<string> write);
    }
}