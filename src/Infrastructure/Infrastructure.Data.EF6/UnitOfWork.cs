


namespace Infrastructure.Data.Ef6
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.Data;
    using System.Data.Common;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Practices.ServiceLocation;
    using Infrastructure.Data.DataContext;
    using Infrastructure.Data.Repositories;
    using Infrastructure.Data.UnitOfWork;
    using System.Data.Entity;

    /// <summary>
    /// Rrepresent a business transaction.
    /// </summary>
    public sealed class UnitOfWork : IUnitOfWorkAsync
    {

        private IDataContextAsync dataContext;
        private bool disposed;
        private DbTransaction transaction;
        private Dictionary<string, dynamic> repositories;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public UnitOfWork(IDataContextAsync dataContext)
        {
            this.dataContext = dataContext;
            this.repositories = new Dictionary<string, dynamic>();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        ~UnitOfWork()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        private DbContext Context { get { return (DbContext)dataContext; } }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns>The number of entity written to the storage.</returns>
        public int SaveChanges()
        {
            return this.dataContext.SaveChanges();
        }

        /// <summary>
        /// Get the repository for this transaction.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <remarks>The repository can be registered in Ioc and obteined or directly created as singleton by the uow.</remarks>
        /// <returns>An instance of <see cref="IRepository{T}"/>.</returns>
        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IObjectState
        {
            if (ServiceLocator.IsLocationProviderSet)
            {
                return ServiceLocator.Current.GetInstance<IRepository<TEntity>>();
            }

            return RepositoryAsync<TEntity>();
        }

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        /// <param name="isolationLevel">The isolation level.</param>
        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            if (this.Context.Database.Connection.State != ConnectionState.Open)
            {
                this.Context.Database.Connection.Open();
            }

            this.transaction = this.Context.Database.Connection.BeginTransaction(isolationLevel);
        }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        /// <returns></returns>
        public bool Commit()
        {
            this.transaction.Commit();
            return true;
        }

        /// <summary>
        /// Rollbacks this instance.
        /// </summary>
        public void Rollback()
        {
            this.transaction.Rollback();
            this.dataContext.SyncObjectsStatePostCommit();
        }

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns></returns>
        public Task<int> SaveChangesAsync()
        {
            return this.dataContext.SaveChangesAsync();
        }

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return this.dataContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Get the async repository for this transaction.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <remarks>The repository can be registered in Ioc and obteined or directly created as singleton by the uow.</remarks>
        /// <returns>An instance of <see cref="IRepository{T}"/>.</returns>
        public IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class, IObjectState
        {
            if (ServiceLocator.IsLocationProviderSet)
            {
                return ServiceLocator.Current.GetInstance<IRepositoryAsync<TEntity>>();
            }

            if (this.repositories == null)
            {
                this.repositories = new Dictionary<string, dynamic>();
            }

            var type = typeof(TEntity).Name;
            if (this.repositories.ContainsKey(type))
            {
                return (IRepositoryAsync<TEntity>)this.repositories[type];
            }

            var repositoryType = typeof(Repository<>);
            this.repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), this.dataContext, this));
            return this.repositories[type];
        }

        /// <summary>
        /// Hooks the database underline manager with the specified log function.
        /// </summary>
        /// <param name="write">The write.</param>
        public void Hook(Action<string> write)
        {
            this.Context.Database.Log = write;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (this.disposed) { return; }

            if (disposing)
            {
                // free other managed objects that implement
                // IDisposable only
                try
                {
                    if (this.Context != null && this.Context.Database.Connection.State == ConnectionState.Open)
                    {
                        this.Context.Database.Connection.Close();
                        //this.Context.Dispose();
                    }
                    //if (this.transaction != null)
                    //{
                    //    this.transaction.Dispose();
                    //    this.transaction = null;
                    //}
                    if (this.dataContext != null)
                    {
                        this.dataContext.Dispose();
                        this.dataContext = null;
                    }
                }
                catch (ObjectDisposedException)
                {
                    // do nothing, the objectContext has already been disposed
                }
            }

            // release any unmanaged objects
            // set large object references to null
            if (this.repositories != null) { this.repositories = null; }
            this.disposed = true;
        }
    }
}