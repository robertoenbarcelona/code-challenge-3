
namespace Infrastructure.Business.Service.Test
{
    using Infrastructure.Data.Repositories;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class DummyQueryFluent : IQueryFluent<DummyEntity>
    {
        public IQueryFluent<DummyEntity> OrderBy(System.Func<System.Linq.IQueryable<DummyEntity>, System.Linq.IOrderedQueryable<DummyEntity>> orderBy)
        {
            return null;
        }

        public IQueryFluent<DummyEntity> Include(System.Linq.Expressions.Expression<System.Func<DummyEntity, object>> expression)
        {
            return null;
        }

        public System.Collections.Generic.IEnumerable<DummyEntity> SelectPage(int page, int pageSize, out int totalCount)
        {
            totalCount = 0;
            return null;
        }

        public System.Collections.Generic.IEnumerable<TResult> Select<TResult>(System.Linq.Expressions.Expression<System.Func<DummyEntity, TResult>> selector = null)
        {
            return null;
        }

        public System.Collections.Generic.IEnumerable<DummyEntity> Select()
        {
            return null;
        }

        public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<DummyEntity>> SelectAsync()
        {
            return null;
        }

        public System.Linq.IQueryable<DummyEntity> SqlQuery(string query, params object[] parameters)
        {
            return null;
        }
    }
}
