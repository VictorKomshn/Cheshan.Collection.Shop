using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.Database.Entities;

namespace Cheshan.Collection.Shop.Core.Mappers
{
    public static class CartsMapper
    {
        public static CartEntity ToEntity(this CartModel model)
        {
            return new CartEntity
            {
                Id = model.Id,
                Products = model.Products.Select(x => x.ToEntity()).ToArray(),
                UserId = model.UserId,
            };
        }

        public static CartModel ToModel(this CartEntity entity)
        {
            return new CartModel
            {
                Id = entity.Id,
                Products = entity.Products.Select(x => x.ToModel()).ToArray(),
                UserId = entity.UserId,
            };
        }
    }
}
