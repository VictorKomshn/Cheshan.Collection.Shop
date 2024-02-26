using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.Database.Entities;

namespace Cheshan.Collection.Shop.Core.Abstract
{
    public interface IProductsService
    {
        Task<GetByConditionResultModel> GetByConditionAsync(ProductsCondition? condition, SortingType? sortType = null);

        Task<ICollection<ProductModel>> GetSuggestedForProduct(Guid productGuid);

        Task<IEnumerable<ProductModel>> GetFrontPageProductsAsync();

        Task<ProductModel> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<ProductModel> UpdateAsync(Guid id, ProductModel newModel);

        Task<Guid> CreateAsync(ProductModel newModel);
    }
}
