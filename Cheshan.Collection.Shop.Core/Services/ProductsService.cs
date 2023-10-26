using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.Core.Mappers;
using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.Database.Abstract;

namespace Cheshan.Collection.Shop.Core.Services
{
    public class ProductsService : IProductsService
    {

        private readonly IProductsRepository _repository;

        public ProductsService(IProductsRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Guid> CreateAsync(ProductModel newModel)
        {
            try
            {
                newModel.Id = Guid.NewGuid();
                await _repository.CreateAsync(newModel.ToEntity());
                return newModel.Id;
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                await _repository.DeleteAsync(id);
            }
            catch
            {
                throw;
            }
        }

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

        public async Task<GetByConditionResultModel> GetByConditionAsync(ProductsCondition? condition, SortingType? sortType = null, bool getSugested = false)
        {
            try
            {
                var productEntities = await _repository.GetByConditionAsync(condition.StartIndex, condition.IsMan, condition?.Brand?.Split(','), condition?.Category?.Split(','), condition?.LowestPrice, condition?.HighestPrice, condition.Sizes?.Split(','), null, condition.Take, getSugested, sortType);

                var result = new GetByConditionResultModel
                {
                    Products = productEntities.Products.Select(x => x.ToModel()),
                    MaxAmount = productEntities.MaxAmount

                };
                return result;
            }
            catch
            {
                throw;
            }


        }

        public async Task<ProductModel> UpdateAsync(Guid id, ProductModel newModel)
        {
            try
            {
                var updatedEntity = await _repository.UpdateAsync(id, newModel.ToEntity(id));
                return updatedEntity.ToModel();
            }
            catch
            {
                throw;
            }
        }
    }
}
