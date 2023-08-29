using Cheshan.Collection.Shop.Database.Entities;
using System.Linq.Expressions;

namespace Cheshan.Collection.Shop.Database.Abstract
{
    public interface IProductsRepository
    {
        Task<IEnumerable<ProductEntity>> GetByConditionAsync(Expression<Func<ProductEntity, bool>> condition);

        Task<ProductEntity> GetAsync(Guid id);

        Task<ProductEntity> UpdateAsync(Guid id, ProductEntity newEntity);

        Task CreateAsync(ProductEntity newEntity);

        Task DeleteAsync(Guid id);

    }
}
