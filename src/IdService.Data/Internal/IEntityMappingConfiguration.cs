using Microsoft.EntityFrameworkCore;

namespace IdService.Data.Internal
{
    internal interface IEntityMappingConfiguration
    {
        void Map(ModelBuilder builder);
    }
}
