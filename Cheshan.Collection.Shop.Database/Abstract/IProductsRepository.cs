using Cheshan.Collection.Shop.Database.Models;
using System.Linq.Expressions;

namespace Cheshan.Collection.Shop.Database.Abstract
{
    public interface IProductsRepository
    {
        Task<IEnumerable<ProductEntity>> GetByConditionAsync(Expression<Func<ProductEntity, bool>> condition);

        Task<ProductEntity> GetAsync(Guid id);
    }
}
