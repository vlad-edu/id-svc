using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdService.Data.Internal
{
    internal interface IEntityMappingConfiguration<T> : IEntityMappingConfiguration
        where T : class
    {
        void Map(EntityTypeBuilder<T> builder);
    }
}
