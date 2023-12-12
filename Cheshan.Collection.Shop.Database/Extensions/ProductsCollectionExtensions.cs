using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.Database.Entities;

namespace Cheshan.Collection.Shop.Database.Extensions
{
    public static class ProductsCollectionExtensions
    {

        public static IQueryable<ProductEntity> Sort(this IQueryable<ProductEntity> products, SortingType sortingType)
        {

            if (sortingType != null)
            {
                switch (sortingType)
                {
                    case SortingType.ByDate:
                        products = products.OrderBy(x => x.DateAdded);
                        break;
                    case SortingType.ByPriceAscending:
                        products = products.OrderBy(x => x.SalePrice != null ? x.SalePrice : x.Price);
                        break;
                    case SortingType.ByPriceDescending:
                        products = products.OrderByDescending(x => x.SalePrice != null ? x.SalePrice : x.Price);
                        break;
                    case SortingType.BySale:
                        products = products.OrderByDescending(x => x.SalePercent);
                        break;
                }
            }

            return products;
        }
    }
}
