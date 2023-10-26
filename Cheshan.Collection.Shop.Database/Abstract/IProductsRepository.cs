using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.Database.Entities;
using Cheshan.Collection.Shop.Database.Models;

namespace Cheshan.Collection.Shop.Database.Abstract
{
    public interface IProductsRepository
    {
        Task<GetByConditionResult> GetByConditionAsync(int startIndex, bool? isMan, string[]? brandNames, string[]? categories, int? lowestPrice, int? highestPrice, string[]? sizes, string? color, int? take = 32, bool getSuggested = false, SortingType? sortType = null);

        Task<ProductEntity> GetAsync(Guid id, bool noTrackin = false);

        Task<ProductEntity> UpdateAsync(Guid id, ProductEntity newEntity);

        Task CreateAsync(ProductEntity newEntity);

        Task DeleteAsync(Guid id);

    }
}
