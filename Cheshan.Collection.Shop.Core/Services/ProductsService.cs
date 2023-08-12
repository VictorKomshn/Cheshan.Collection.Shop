using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.Core.Mappers;
using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.Database.Abstract;
using Cheshan.Collection.Shop.Database.Models;
using System.Linq.Expressions;

namespace Cheshan.Collection.Shop.Core.Services
{
    public class ProductsService : IProductsService
    {

        private readonly IProductsRepository _repository;

        public async Task<ProductModel> GetAsync(Guid id)
        {
            try
            {
                var productEntity = await _repository.GetAsync(id);
                return productEntity.ToModel();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProductModel>> GetByConditionAsync(ProductsCondition condition)
        {
            Expression<Func<ProductEntity, bool>> filter = product =>
                    product.Size == condition.Size &&
                    product.Price >= condition.LowestPrice &&
                    product.Price <= condition.HighestPrice;
            try
            {
                var productEntities = await _repository.GetByConditionAsync(filter);

                return productEntities.Select(x => x.ToModel());
            }
            catch
            {
                throw;
            }


        }
    }
}
