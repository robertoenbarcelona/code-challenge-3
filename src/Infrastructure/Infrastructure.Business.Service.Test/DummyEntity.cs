
namespace Infrastructure.Business.Service.Test
{
    using Infrastructure.Data;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class DummyEntity : IObjectState
    {
        public int Id { get; set; }

        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
