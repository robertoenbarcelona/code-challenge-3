
namespace Infrastructure.Data.Fakes
{
    using System.Data.Entity;

    /// <summary>
    /// A fake DB Context for testing
    /// </summary>
    public interface IFakeDbContext
    {
        /// <summary>
        /// Get the DB set for the entity type.
        /// </summary>
        /// <typeparam name="T">The entity type.</typeparam>
        /// <returns>The <see cref="DbSet{T}"/></returns>
        DbSet<T> Set<T>() where T : class;

        /// <summary>
        /// Adds the fake DbSet.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TFakeDbSet">The type of the fake DbSet.</typeparam>
        void AddFakeDbSet<TEntity, TFakeDbSet>()
            where TEntity : class, IObjectState, new()
            where TFakeDbSet : FakeDbSet<TEntity>, IDbSet<TEntity>, new();
    }
}
