

namespace Infrastructure.Data.Ef6
{
    using System;
    using System.Data.Entity;

    /// <summary>
    /// Static extensions for <see cref="ObjectState"/>
    /// </summary>
    public static class StateHelper
    {
        /// <summary>
        /// Converts the state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>The <see cref="EntityState"/> value.</returns>
        public static EntityState ConvertState(ObjectState state)
        {
            switch (state)
            {
                case ObjectState.Added:
                    return EntityState.Added;

                case ObjectState.Modified:
                    return EntityState.Modified;

                case ObjectState.Deleted:
                    return EntityState.Deleted;

                default:
                    return EntityState.Unchanged;
            }
        }

        /// <summary>
        /// Converts the state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>The <see cref="ObjectState"/> value.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">state</exception>
        public static ObjectState ConvertState(EntityState state)
        {
            switch (state)
            {
                case EntityState.Detached:
                    return ObjectState.Unchanged;

                case EntityState.Unchanged:
                    return ObjectState.Unchanged;

                case EntityState.Added:
                    return ObjectState.Added;

                case EntityState.Deleted:
                    return ObjectState.Deleted;

                case EntityState.Modified:
                    return ObjectState.Modified;

                default:
                    throw new ArgumentOutOfRangeException("state");
            }
        }
    }
}