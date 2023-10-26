using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.Database.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Cheshan.Collection.Shop.Core.Mappers
{
    public static class ProductsMapper
    {
        public static ProductEntity ToEntity(this ProductModel model, Guid? id = null)
        {
            var result = new ProductEntity
            {
                Id = id.HasValue ? id.Value : model.Id,
                Name = model.Name,
                Price = model.Price,
                Brand = model.Brand,
                Description = model.Description,
                Material = model.Material,
                SalePercent = model.SalePercent,
                SalePrice = model.SalePrice,
                SKU = model.SKU,
                MainPhoto = model.MainPhoto,
                Photos = model.Photos.ToArray(),
                Colours = model.Colours.ToArray(),
                IsMen = model.IsMen,
                Category = model.Category,
                SP = model.SP,
            };

            result.SizesWithAmounts = model.SizesWithAmounts.Select(x => x.ToEntity(result)).ToArray();

            return result;
        }

        public static ProductModel ToModel(this ProductEntity entity)
        {
            var model = new ProductModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                Brand = entity.Brand,
                Description = entity.Description,
                Material = entity.Material,
                SalePercent = entity.SalePercent,
                SalePrice = entity.SalePrice,
                SKU = entity.SKU,
                MainPhoto = entity.MainPhoto,
                Photos = entity.Photos,
                Category = entity.Category,
                IsMen = entity.IsMen,
                SP = entity.SP,
                Colours = entity.Colours.ToArray(),
            };

            model.SizesWithAmounts = entity.SizesWithAmounts.Select(x => x.ToModel(model)).ToList();
            return model;
        }


        public static SizeWithAmountEntity ToEntity(this SizeWithAmountModel sizeWithAmount, ProductEntity? product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            return new SizeWithAmountEntity
            {
                Size = sizeWithAmount.Size,
                Amount = sizeWithAmount.Amount,
                Product = product
            };
        }

        public static SizeWithAmountModel ToModel(this SizeWithAmountEntity sizeWithAmount, ProductModel? product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            var size = sizeWithAmount.Size;
            if (size.StartsWith("size="))
            {
                size = size.Substring(5);
            }
            var model = new SizeWithAmountModel
            {
                Size = size,
                Amount = sizeWithAmount.Amount,
                Product = product
            };
            return model;
        }
    }
}
