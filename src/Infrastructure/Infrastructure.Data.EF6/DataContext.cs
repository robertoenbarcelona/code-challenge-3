
namespace Infrastructure.Data.Ef6
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Infrastructure.Data.DataContext;
    using Infrastructure.Core.Exceptions;

    /// <summary>
    /// An Entity Framewrok data context wrapper
    /// </summary>
    public class DataContext : DbContext, IDataContextAsync
    {
        private readonly Guid instanceId;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext"/> class.
        /// </summary>
        /// <param name="nameOrConnectionString">Either the database name or a connection string.</param>
        public DataContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            instanceId = Guid.NewGuid();
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            //Configuration.AutoDetectChangesEnabled = false;
        }

        /// <summary>
        /// Gets the instance identifier.
        /// </summary>
        public Guid InstanceId { get { return instanceId; } }

        /// <summary>
        ///     Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <exception cref="System.Data.Entity.Infrastructure.DbUpdateException">
        ///     An error occurred sending updates to the database.</exception>
        /// <exception cref="System.Data.Entity.Infrastructure.DbUpdateConcurrencyException">
        ///     A database command did not affect the expected number of rows. This usually
        ///     indicates an optimistic concurrency violation; that is, a row has been changed
        ///     in the database since it was queried.</exception>
        /// <exception cref="System.Data.Entity.Validation.DbEntityValidationException">
        ///     The save was aborted because validation of entity property values failed.</exception>
        /// <exception cref="System.NotSupportedException">
        ///     An attempt was made to use unsupported behavior such as executing multiple
        ///     asynchronous commands concurrently on the same context instance.</exception>
        /// <exception cref="System.ObjectDisposedException">
        ///     The context or connection have been disposed.</exception>
        /// <exception cref="System.InvalidOperationException">
        ///     Some error occurred attempting to process entities in the context either
        ///     before or after sending commands to the database.</exception>
        /// <seealso cref="DbContext.SaveChanges"/>
        /// <returns>The number of objects written to the underlying database.</returns>
        public override int SaveChanges()
        {
            try
            {
                SyncObjectsStatePreCommit();
                var changes = base.SaveChanges();
                SyncObjectsStatePostCommit();
                return changes;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException ex)
            {
                throw new DataUpdateException(Infrastructure.Resources.Exceptions.UpdateFail, ex, DataUpdateException.FailType.Concurrency);
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                throw new DataUpdateException(Infrastructure.Resources.Exceptions.UpdateFail, ex, DataUpdateException.FailType.Update);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("-", errorMessages);
                var exceptionMessage = string.Concat(Infrastructure.Resources.Exceptions.UpdateFail, ": [", fullErrorMessage, "]");
                throw new DataUpdateException(exceptionMessage, ex, DataUpdateException.FailType.Validation);
                throw new DataUpdateException(Infrastructure.Resources.Exceptions.UpdateFail, ex, DataUpdateException.FailType.Validation);
            }
            catch (System.ObjectDisposedException ex)
            {
                throw new DataUpdateException(Infrastructure.Resources.Exceptions.UpdateFail, ex, DataUpdateException.FailType.ObjectDisposed);
            }
            catch (System.NotSupportedException ex)
            {
                throw new DataUpdateException(Infrastructure.Resources.Exceptions.UpdateFail, ex, DataUpdateException.FailType.InvalidOperation);
            }
        }

        /// <summary>
        ///     Asynchronously saves all changes made in this context to the underlying database.
        /// </summary>
        /// <exception cref="System.Data.Entity.Infrastructure.DbUpdateException">
        ///     An error occurred sending updates to the database.</exception>
        /// <exception cref="System.Data.Entity.Infrastructure.DbUpdateConcurrencyException">
        ///     A database command did not affect the expected number of rows. This usually
        ///     indicates an optimistic concurrency violation; that is, a row has been changed
        ///     in the database since it was queried.</exception>
        /// <exception cref="System.Data.Entity.Validation.DbEntityValidationException">
        ///     The save was aborted because validation of entity property values failed.</exception>
        /// <exception cref="System.NotSupportedException">
        ///     An attempt was made to use unsupported behavior such as executing multiple
        ///     asynchronous commands concurrently on the same context instance.</exception>
        /// <exception cref="System.ObjectDisposedException">
        ///     The context or connection have been disposed.</exception>
        /// <exception cref="System.InvalidOperationException">
        ///     Some error occurred attempting to process entities in the context either
        ///     before or after sending commands to the database.</exception>
        /// <seealso cref="System.Data.Entity.DbContext.SaveChangesAsync()"/>
        /// <returns>A task that represents the asynchronous save operation.  The 
        ///     <see cref="Task{T}">Task.Result</see> contains the number of 
        ///     objects written to the underlying database.</returns>
        public override async Task<int> SaveChangesAsync()
        {
            return await this.SaveChangesAsync(CancellationToken.None);
        }

        /// <summary>
        ///     Asynchronously saves all changes made in this context to the underlying database.
        /// </summary>
        /// <exception cref="System.Data.Entity.Infrastructure.DbUpdateException">
        ///     An error occurred sending updates to the database.</exception>
        /// <exception cref="System.Data.Entity.Infrastructure.DbUpdateConcurrencyException">
        ///     A database command did not affect the expected number of rows. This usually
        ///     indicates an optimistic concurrency violation; that is, a row has been changed
        ///     in the database since it was queried.</exception>
        /// <exception cref="System.Data.Entity.Validation.DbEntityValidationException">
        ///     The save was aborted because validation of entity property values failed.</exception>
        /// <exception cref="System.NotSupportedException">
        ///     An attempt was made to use unsupported behavior such as executing multiple
        ///     asynchronous commands concurrently on the same context instance.</exception>
        /// <exception cref="System.ObjectDisposedException">
        ///     The context or connection have been disposed.</exception>
        /// <exception cref="System.InvalidOperationException">
        ///     Some error occurred attempting to process entities in the context either
        ///     before or after sending commands to the database.</exception>
        /// <seealso cref="System.Data.Entity.DbContext.SaveChangesAsync()"/>
        /// <returns>A task that represents the asynchronous save operation.  The 
        ///     <see cref="Task{T}">Task.Result</see> contains the number of 
        ///     objects written to the underlying database.</returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                SyncObjectsStatePreCommit();
                var changesAsync = await base.SaveChangesAsync(cancellationToken);
                SyncObjectsStatePostCommit();
                return changesAsync;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException ex)
            {
                throw new DataUpdateException(Infrastructure.Resources.Exceptions.UpdateFail, ex, DataUpdateException.FailType.Concurrency);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("-", errorMessages);
                var exceptionMessage = string.Concat(Infrastructure.Resources.Exceptions.UpdateFail, ": [", fullErrorMessage, "]");
                throw new DataUpdateException(exceptionMessage, ex, DataUpdateException.FailType.Validation);
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                throw new DataUpdateException(Infrastructure.Resources.Exceptions.UpdateFail, ex, DataUpdateException.FailType.Update);
            }
            catch (System.ObjectDisposedException ex)
            {
                throw new DataUpdateException(Infrastructure.Resources.Exceptions.UpdateFail, ex, DataUpdateException.FailType.ObjectDisposed);
            }
            catch (System.NotSupportedException ex)
            {
                throw new DataUpdateException(Infrastructure.Resources.Exceptions.UpdateFail, ex, DataUpdateException.FailType.InvalidOperation);
            }
        }

        /// <summary>
        /// Synchronizes the state of the object.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState
        {
            Entry(entity).State = StateHelper.ConvertState(entity.ObjectState);
        }

        /// <summary>
        /// Synchronizes the objects state post commit.
        /// </summary>
        public void SyncObjectsStatePostCommit()
        {
            foreach (var dbEntityEntry in ChangeTracker.Entries())
            {
                ((IObjectState)dbEntityEntry.Entity).ObjectState = StateHelper.ConvertState(dbEntityEntry.State);
            }
        }

        /// <summary>
        /// Disposes the context. The underlying <see cref="T:System.Data.Entity.Core.Objects.ObjectContext" /> is also disposed if it was created
        /// is by this context or ownership was passed to this context when this context was created.
        /// The connection to the database (<see cref="T:System.Data.Common.DbConnection" /> object) is also disposed if it was created
        /// is by this context or ownership was passed to this context when this context was created.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void SyncObjectsStatePreCommit()
        {
            foreach (var dbEntityEntry in ChangeTracker.Entries())
            {
                dbEntityEntry.State = StateHelper.ConvertState(((IObjectState)dbEntityEntry.Entity).ObjectState);
            }
        }
    }
}