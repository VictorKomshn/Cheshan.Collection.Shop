using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.Database.Models;

namespace Cheshan.Collection.Shop.Core.Mappers
{
    public static class ProductsMapper
    {
        public static ProductEntity ToEntity(this ProductModel model)
        {
            return new ProductEntity
            {
                Id = model.Id,
                Amount = model.Amount,
                Name = model.Name,
                Price = model.Price,
                Size = model.Size,
            };
        }

        public static ProductModel ToModel(this ProductEntity entity)
        {
            return new ProductModel
            {
                Id = entity.Id,
                Amount = entity.Amount,
                Name = entity.Name,
                Price = entity.Price,
                Size = entity.Size,
            };
        }
    }
}
