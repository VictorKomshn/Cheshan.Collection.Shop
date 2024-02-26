using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.Core.Mappers;
using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.Database.Abstract;
using Cheshan.Collection.Shop.Database.Entities;
using Cheshan.Collection.Shop.Database.Entities.Enums;

namespace Cheshan.Collection.Shop.Core.Services
{
    public class ProductsService : IProductsService
    {

        private readonly IProductsRepository _repository;

        private readonly Dictionary<string, CategoryType> categoryGroups = new Dictionary<string, CategoryType>()
        {
            {"футболка",CategoryType.Clothes },
            {"топ",CategoryType.Clothes },
            {"жакет",CategoryType.Clothes },
            {"толстовка",CategoryType.Clothes },
            {"свитшот",CategoryType.Clothes },
            {"свитер",CategoryType.Clothes },
            {"джемпер",CategoryType.Clothes },
            {"платье",CategoryType.Clothes },
            {"юбка",CategoryType.Clothes },
            {"рубашка",CategoryType.Clothes },
            {"блуза",CategoryType.Clothes },
            {"брюки",CategoryType.Clothes },
            {"шорты",CategoryType.Clothes },
            {"джинсы",CategoryType.Clothes },
            {"sport",CategoryType.Clothes },
            {"пляжная одежда",CategoryType.Clothes },
            {"верхняя одежда",CategoryType.Clothes },
            {"пиджак",CategoryType.Clothes },

            {"кроссовки",CategoryType.Footwear },
            {"кеды",CategoryType.Footwear },
            {"ботинки",CategoryType.Footwear },
            {"туфли",CategoryType.Footwear },
            {"сандали",CategoryType.Footwear },


            {"головной убор",CategoryType.Accessories },
            {"сумка",CategoryType.Accessories},
            {"украшение",CategoryType.Accessories },
            {"аксессуар",CategoryType.Accessories },
        };

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

        public async Task<ICollection<ProductModel>> GetSuggestedForProduct(Guid productGuid)
        {
            try
            {
                var productSuggested = await _repository.GetProductsSuggestedAsync(productGuid);

                return productSuggested.Select(x => x.ToModel()).ToList();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProductModel>> GetFrontPageProductsAsync()
        {
            try
            {
                var result = await _repository.GetFrontPageProductsAsync();
                return result.Select(x => x.ToModel());
            }
            catch
            {
                throw;
            }
        }

        public async Task<GetByConditionResultModel> GetByConditionAsync(ProductsCondition? condition, SortingType? sortType = null)
        {
            try
            {
                var productEntities = await _repository.GetByConditionAsync(condition.StartIndex, condition.IsMan, condition?.Brand?.Split(','), condition?.Category?.Split(','), condition.CategoryType, condition?.LowestPrice, condition?.HighestPrice, condition.Sizes?.Split(','), null, condition.SearchString, sortType);

                ICollection<ProductEntity> products = productEntities.Products.ToList();
                var category = condition.Category?.Split(",").First();
                CategoryType categoryType = condition.CategoryType ?? CategoryType.Default;

                var result = new GetByConditionResultModel
                {
                    Products = products.Select(x => x.ToModel()).ToList(),
                    MaxAmount = productEntities.MaxAmount,
                    CategoryType = categoryType

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
