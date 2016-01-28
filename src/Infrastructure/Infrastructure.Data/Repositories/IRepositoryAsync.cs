
namespace Infrastructure.Data.Repositories
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represent a repository for the entity type in asyncronuous mode.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IRepositoryAsync<TEntity> : IRepository<TEntity> where TEntity : class, IObjectState
    {
        /// <summary>
        /// Finds the entity in asynchronous mode.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns>An instance of <see cref="Task{T}"/> with the execution.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if multiple entities exist in the context with the primary key values.</exception>
        /// <exception cref="System.InvalidOperationException">Thrown if the type of entity is not part of the data model for this context.</exception>
        /// <exception cref="System.InvalidOperationException">Thrown if the types of the key values do not match the types of the key values for the entity type to be found.</exception>
        /// <exception cref="System.InvalidOperationException">Thrown if the context has been disposed.</exception>
        Task<TEntity> FindAsync(params object[] keyValues);

        /// <summary>
        /// Finds the entity in asynchronous mode.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="keyValues">The key values.</param>
        /// <returns>An instance of <see cref="Task{T}"/> with the execution.</returns>
        Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);

        /// <summary>
        /// Deletes the entity in asynchronous mode.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns>An instance of <see cref="Task{T}"/> with the execution.</returns>
        Task<bool> DeleteAsync(params object[] keyValues);

        /// <summary>
        /// Deletes the entity in asynchronous mode.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="keyValues">The key values.</param>
        /// <returns>An instance of <see cref="Task{T}"/> with the execution.</returns>
        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);
    }
}