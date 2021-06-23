using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdService.Data.Internal
{
    internal abstract class EntityMappingConfiguration<T> : IEntityMappingConfiguration<T>
        where T : class
    {
        public abstract void Map(EntityTypeBuilder<T> b);

        public void Map(ModelBuilder b)
        {
            Map(b.Entity<T>());
        }
    }
}
