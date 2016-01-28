
namespace SCA.Infrastructure.Data.Ef6
{
    using System;
    using System.Data.Entity;

    public static class Extensions
    {
        /// <summary>
        /// Hooks the save changes metod.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="funcDelegate">The function delegate.</param>
        public static void HookSaveChanges(this DbContext dbContext, Action<string> funcDelegate)
        {
            new EntityFrameworkHook(dbContext, funcDelegate);
        }
    }
}
