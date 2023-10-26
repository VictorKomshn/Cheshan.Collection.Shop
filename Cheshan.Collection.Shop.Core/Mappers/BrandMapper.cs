using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.Database.Entities;

namespace Cheshan.Collection.Shop.Core.Mappers
{
    public static class BrandMapper
    {
        public static BrandModel? ToModel(this BrandEntity entity)
        {
            if(entity == null)
            {
                return null;
            }
            return new BrandModel
            {
                Description = entity.Description,
                Logo = entity.Logo,
                Name = entity.Name,
            };
        }
    }
}
