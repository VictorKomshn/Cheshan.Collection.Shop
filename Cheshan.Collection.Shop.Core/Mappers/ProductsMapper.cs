using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.Database.Entities;

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
                IsMan = model.IsMan,
                Category = model.Category,
                SP = model.SP,
                SEO = model.SEO,
                Details = model.Details?.ToArray(),
                ModelParameters = model.ModelParameters
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
                CategoryType = entity.CategoryType,
                IsMan = entity.IsMan,
                SP = entity.SP,
                SEO = entity.SEO,
                Details = entity.Details,
                ModelParameters = entity.ModelParameters
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
            if (size.Split(',').Length >= 2)
            {
                size = string.Join('-', size.Split(','));
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
