using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.Database.Abstract;
using Cheshan.Collection.Shop.Database.Database;
using Cheshan.Collection.Shop.Database.Entities;
using Cheshan.Collection.Shop.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Cheshan.Collection.Shop.Database.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly DataContext _dataContext;

        public ProductsRepository(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext)); ;
        }

        public async Task CreateAsync(ProductEntity newEntity)
        {
            try
            {
                newEntity.DateAdded = DateTime.UtcNow;
                foreach (var size in newEntity.SizesWithAmounts)
                {
                    size.Id = Guid.NewGuid();
                }
                await _dataContext.Products.AddAsync(newEntity);
                await _dataContext.SaveChangesAsync();
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
                var product = await GetAsync(id);
                _dataContext.Products.Remove(product);
                await _dataContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<ProductEntity> GetAsync(Guid id, bool noTrackin = false)
        {
            ProductEntity? result = null;
            if (noTrackin)
            {
                result = await _dataContext.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            }
            else
            {
                result = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            }

            if (result == null)
            {
                throw new Exception("Product was not found");
            }

            return result;
        }

        public async Task<GetByConditionResult> GetByConditionAsync(int startIndex,
                                                                          bool? isMan,
                                                                          string[]? brandNames,
                                                                          string[]? categories,
                                                                          int? lowestPrice,
                                                                          int? highestPrice,
                                                                          string[]? sizes,
                                                                          string? color,
                                                                          int? take = 16,
                                                                          bool getSuggested = false,
                                                                          SortingType? sortingType = null)
        {
            var query = _dataContext.Products.AsQueryable();

            if (brandNames != null && brandNames.Any())
            {
                query = query.Where(x => brandNames.Contains(x.Brand.ToLower()));
            }
            if (categories != null && categories.Any())
            {
                query = query.Where(x => categories.Contains(x.Category.ToLower()));
            }
            if (lowestPrice != null)
            {
                query = query.Where(x => x.Price >= lowestPrice);
            }
            if (highestPrice != null)
            {
                query = query.Where(x => x.Price <= highestPrice || x.SalePrice <= highestPrice);
            }
            if (color != null)
            {
                query = query.Where(x => x.Colours.Contains(color));

            }
            if (isMan != null)
            {
                query = query.Where(x => x.IsMen == isMan);
            }

            var amount = await _dataContext.Products.CountAsync();
            var maxElementsAmount = await query.CountAsync();
            List<ProductEntity> products = new List<ProductEntity>();

            if (take > amount)
            {
                products = await query.Skip(startIndex).Take(take.Value).ToListAsync();
            }
            else if (take.HasValue && take != null && getSuggested)
            {
                Random rand = new Random();
                int skipper = rand.Next(0, maxElementsAmount - take.Value);
                var sortingGuid = Guid.NewGuid();
                products = await query.OrderBy(product => sortingGuid)
                                  .Skip(skipper)
                                  .Take(take.Value)
                                  .ToListAsync();
            }
            else
            {
                products = await query.Skip(startIndex).Take(16).ToListAsync();
            }

            if (sizes != null)
            {
                products = products.Where(x =>
                {
                    var sizeWithAmount = x.SizesWithAmounts.Where(x => x.Size != null && sizes.Contains(x.Size) && x.Amount > 0);
                    return sizeWithAmount != null && sizeWithAmount.Any();
                }).ToList();
            }

            if (sortingType != null)
            {
                switch (sortingType)
                {
                    case SortingType.ByDate:
                        products = products.OrderBy(x => x.DateAdded).ToList();
                        break;
                    case SortingType.ByPriceAscending:
                        products = products.OrderBy(x => x.Price).ToList();
                        break;
                    case SortingType.ByPriceDescending:
                        products = products.OrderByDescending(x => x.Price).ToList();
                        break;
                    case SortingType.BySale:
                        products = products.OrderBy(x => x.SalePercent).ToList();
                        break;
                }
            }


            var result = new GetByConditionResult
            {
                Products = products,
                MaxAmount = maxElementsAmount
            };
            return result;
        }

        public async Task<ProductEntity> UpdateAsync(Guid id, ProductEntity newEntity)
        {
            try
            {
                var oldEntity = await GetAsync(id, true);
                var sizes = oldEntity.SizesWithAmounts;
                foreach (var newSize in newEntity.SizesWithAmounts)
                {
                    var oldSize = sizes.FirstOrDefault(x => x.Size == newSize.Size);
                    if (oldSize == null)
                    {
                        newSize.Id = Guid.NewGuid();
                    }
                    else
                    {
                        newSize.Id = oldSize.Id;
                    }
                }
                _dataContext.Products.Update(newEntity);
                await _dataContext.SaveChangesAsync();
                return newEntity;
            }
            catch
            {
                throw;
            }
        }
    }
}
