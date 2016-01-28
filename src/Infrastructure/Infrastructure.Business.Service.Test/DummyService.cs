
namespace Infrastructure.Business.Service.Test
{
    using Infrastructure.Business.Service;
    using Infrastructure.Data.Repositories;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class DummyService : Service<DummyEntity>
    {
        public DummyService(IRepositoryAsync<DummyEntity> rep)
            : base(rep)
        { }

        public new IRepositoryAsync<DummyEntity> Repository { get { return base.Repository; } }
    }
}
