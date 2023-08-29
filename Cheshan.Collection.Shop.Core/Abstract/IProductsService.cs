using Cheshan.Collection.Shop.Core.Models;

namespace Cheshan.Collection.Shop.Core.Abstract
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductModel>> GetByConditionAsync(ProductsCondition condition);

        Task<ProductModel> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<ProductModel> UpdateAsync(Guid id, ProductModel newModel);

        Task<Guid> CreateAsync(ProductModel newModel);
    }
}
