using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.Database.Entities;

namespace Cheshan.Collection.Shop.Core.Mappers
{
    public static class ProductsMapper
    {
        public static ProductEntity ToEntity(this ProductModel model, Guid? id = null)
        {
            return new ProductEntity
            {
                Id = id.HasValue ? id.Value : model.Id,
                Name = model.Name,
                Price = model.Price,
                Size = model.Size,
                AmountLeft = model.AmountLeft,
                Brand = model.Brand,
                Description = model.Description,
                Material = model.Material,
                SalePercent = model.SalePercent,
                SalePrice = model.SalePrice,
                SKU = model.SKU,
                MainPhoto = model.MainPhoto,
                Photos = model.Photos.ToArray(),
            };
        }

        public static ProductModel ToModel(this ProductEntity entity)
        {
            return new ProductModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                Size = entity.Size,
                AmountLeft = entity.AmountLeft,
                Brand = entity.Brand,
                Description = entity.Description,
                Material = entity.Material,
                SalePercent = entity.SalePercent,
                SalePrice = entity.SalePrice,
                SKU = entity.SKU,
                MainPhoto = entity.MainPhoto,
                Photos = entity.Photos,
            };
        }
    }
}
