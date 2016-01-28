
namespace Infrastructure.Data.UnitOfWork
{
    using System.Threading;
    using System.Threading.Tasks;
    using Infrastructure.Data.Repositories;

    /// <summary>
    /// Rrepresent a business transaction in asyncronous mode.
    /// </summary>
    public interface IUnitOfWorkAsync : IUnitOfWork
    {
        /// <summary>
        /// Saves the changes in asynchronous mode.
        /// </summary>
        /// <returns>An instance of <see cref="Task{T}"/> with the execution.</returns>
        Task<int> SaveChangesAsync();


        /// <summary>
        /// Saves the changes in asynchronous mode.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An instance of <see cref="Task{T}"/> with the execution.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Get the async repository for this transaction.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <remarks>The repository can be registered in Ioc and obteined or directly created as singleton by the uow.</remarks>
        /// <returns>An instance of <see cref="IRepository{T}"/>.</returns>
        IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class, IObjectState;
    }
}