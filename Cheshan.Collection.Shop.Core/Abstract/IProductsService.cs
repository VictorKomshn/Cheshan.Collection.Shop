using Cheshan.Collection.Shop.Core.Models;

namespace Cheshan.Collection.Shop.Core.Abstract
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductModel>> GetByConditionAsync(ProductsCondition condition);

        Task<ProductModel> GetAsync(Guid id);
    }
}
