using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.Database.Entities;
using Cheshan.Collection.Shop.Database.Entities.Enums;
using Cheshan.Collection.Shop.Database.Models;

namespace Cheshan.Collection.Shop.Database.Abstract
{
    public interface IProductsRepository
    {
        Task<GetByConditionResult> GetByConditionAsync(int startIndex, bool? isMan, string[]? brandNames, string[]? categories, CategoryType? categoryType, int? lowestPrice, int? highestPrice, string[]? sizes, string? color, string? searchString = null, SortingType? sortType = null);

        Task<IEnumerable<ProductEntity>> GetProductsSuggested(Guid productId);

        Task<ProductEntity> GetAsync(Guid id, bool noTracking = false);

        Task<ProductEntity> UpdateAsync(Guid id, ProductEntity newEntity);

        Task CreateAsync(ProductEntity newEntity);

        Task DeleteAsync(Guid id);

    }
}
