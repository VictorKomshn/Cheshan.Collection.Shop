using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.Database.Abstract;
using Cheshan.Collection.Shop.Database.Comparers;
using Cheshan.Collection.Shop.Database.Database;
using Cheshan.Collection.Shop.Database.Entities;
using Cheshan.Collection.Shop.Database.Entities.Enums;
using Cheshan.Collection.Shop.Database.Extensions;
using Cheshan.Collection.Shop.Database.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Cheshan.Collection.Shop.Database.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly DataContext _dataContext;

        private readonly string[] existingCategories = { "футболк", "топ", "жакет", "толстовк", "свитшот", "свитер", "джемпер", "плать", "юбк", "рубашк", "блуз", "брюки", "шорты", "джинсы", "спортивн", "пляжная", "верхняя", "кроссовки", "ботинки", "туфли", "головные", "украшения", "аксессуары", "сумк", "пиджак" };

        private readonly string[] existingBrands = { "Aquazzura", "Altea", "Premiata", "Peserico", "Palm Angels", "Bottega Veneta", "BY FAR", "Karl Lagerfeld", "Kiton", "Ermanno Scervino", "Etro", "Fabiana Filippi", "Dirk Bikkembergs", "Jil Sander", "Jacquemus", "Giuseppe di Morabito", "Giuseppe Zanotti", "Сult Gaia", "Panicale", "Sara Roka", "Balossa", "Khrisjoy", "Doucal's", "John Hatter&Co", "FENDI", "Moorer", "DOMREBEL" };
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

        private IQueryable<ProductEntity> GetBySearchString(IQueryable<ProductEntity> query, string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                string? category = null;
                string? brand = null;

                var splitString = searchString.Split(' ');
                if (splitString.Length > 1)
                {
                    category = splitString.FirstOrDefault(x => existingCategories.Contains(x, new SearchComparer()));
                    if (category == null)
                    {
                        category = existingCategories.FirstOrDefault(x => splitString.Any(y => x.Contains(y, StringComparison.InvariantCultureIgnoreCase)));
                    }
                    brand = splitString.FirstOrDefault(x => existingBrands.Contains(x, new SearchComparer()));

                    if (brand == null)
                    {
                        brand = existingBrands.FirstOrDefault(x => splitString.Any(y => x.Contains(y, StringComparison.InvariantCultureIgnoreCase)));
                    }
                }
                else
                {
                    category = existingCategories.FirstOrDefault(x => x.Contains(searchString, StringComparison.InvariantCultureIgnoreCase) || searchString.Contains(x, StringComparison.InvariantCultureIgnoreCase));

                    brand = existingBrands.FirstOrDefault(x => x.Contains(searchString, StringComparison.InvariantCultureIgnoreCase) || searchString.Contains(x, StringComparison.InvariantCultureIgnoreCase));
                }


                if (category != null)
                {
                    query = query.Where(x => x.Category.Contains(category));
                }
                if (brand != null)
                {
                    query = query.Where(x => x.Brand.Contains(brand));
                }

                if (brand == null && category == null)
                {
                    query = query.Where(x => x.Name.Contains(searchString));
                }

                return query;
            }

            return query;

        }

        public async Task<ProductEntity> GetAsync(Guid id, bool noTracking = false)
        {
            ProductEntity? result = null;
            if (noTracking)
            {
                result = await _dataContext.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            }
            else
            {
                result = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            }

            if (result == null)
            {
                throw new ArgumentException("Product was not found");
            }

            return result;
        }


        public async Task<IEnumerable<ProductEntity>> GetProductsSuggestedAsync(Guid productId)
        {
            var take = 4;

            var product = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == productId);
            var products = new List<ProductEntity>();
            if (product != null)
            {
                var query = _dataContext.Products.AsQueryable();

                query = query.Where(x => x.IsMan == product.IsMan);
                query = query.Where(x => x.SizesWithAmounts
                                            .Select(x => x.Amount)
                                            .Any(x => x > 0));

                var suggestedElementsAmount = await query.Where(x => x.Category == product.Category).CountAsync();
                if (suggestedElementsAmount < 4)
                {
                    query = query.Where(x => x.CategoryType == product.CategoryType);
                    suggestedElementsAmount = await query.CountAsync();
                }
                else
                {
                    query = query.Where(x => x.Category == product.Category);
                }


                Random rand = new Random();
                int skipper = 0;
                if (4 > suggestedElementsAmount)
                {
                    skipper = rand.Next(0, suggestedElementsAmount);
                }
                else
                {
                    skipper = rand.Next(0, suggestedElementsAmount - take);
                }
                var sortingGuid = Guid.NewGuid();
                products = await query.OrderBy(product => sortingGuid)
                                  .Skip(skipper)
                                  .Take(take)
                                  .ToListAsync();

                var douplicateProduct = products.FirstOrDefault(x => x.Id == product.Id);
                if (douplicateProduct != null)
                {
                    products.Remove(douplicateProduct);
                }
            }
            return products;
        }

        public async Task<IEnumerable<ProductEntity>> GetFrontPageProductsAsync()
        {
            var latestProductsAmount = 20;
            var take = 4;
            var query = _dataContext.Products.AsQueryable();
            query = query.OrderBy(x => x.DateAdded)
                 .Where(x => x.CategoryType == CategoryType.Clothes)
                 .Take(20);

            int skipper = new Random().Next(0, latestProductsAmount);

            var sortingGuid = Guid.NewGuid();
            var products = await query.OrderBy(product => sortingGuid)
                              .Skip(skipper)
                              .Take(take)
                              .ToArrayAsync();
            return products;
        }

        public async Task<GetByConditionResult> GetByConditionAsync(int startIndex,
                                                                    bool? isMan,
                                                                    string[]? brandNames,
                                                                    string[]? categories,
                                                                    CategoryType? categoryType,
                                                                    int? lowestPrice,
                                                                    int? highestPrice,
                                                                    string[]? sizes,
                                                                    string? color,
                                                                    string? searchString = null,
                                                                    SortingType? sortType = null)
        {
            var valuesToReplace = new[] { ',', '-', '/' };
            brandNames = brandNames?.Select(x => x.Replace(valuesToReplace, ' ')).ToArray();

            var take = 16;
            var query = _dataContext.Products.AsQueryable();

            query = query.Where(x => x.SizesWithAmounts
                                            .Select(x => x.Amount)
                                            .Any(x => x > 0));

            if (searchString != null)
            {
                query = GetBySearchString(query, searchString);
            }
            if (brandNames != null && brandNames.Any())
            {
                query = query.Where(x => brandNames.Contains(x.Brand.ToLower()));
            }
            if (categories != null && categories.Any())
            {
                query = query.Where(x => categories.Contains(x.Category.ToLower()) ||
                                         string.Join(',', categories).Contains(x.Category.ToLower()) ||
                                         x.Category.ToLower().Contains(string.Join(',', categories)));
            }
            else if (categoryType != null && categoryType != CategoryType.Default)
            {
                query = query.Where(x => x.CategoryType == categoryType);
            }
            if (lowestPrice != null && lowestPrice != double.MinValue)
            {
                query = query.Where(x => x.Price >= lowestPrice);
            }
            if (highestPrice != null && highestPrice != double.MaxValue)
            {
                query = query.Where(x => x.Price <= highestPrice || x.SalePrice <= highestPrice);
            }
            if (isMan != null)
            {
                query = query.Where(x => x.IsMan == isMan);
            }

            var maxElementsAmount = await query.CountAsync();

            if (sortType.HasValue)
            {
                query = query.Sort(sortType.Value);
            }

            var products = await query.Skip(startIndex).Take(take).ToListAsync();

            if (sizes != null)
            {
                products = products.Where(x =>
                {
                    var sizeWithAmount = x.SizesWithAmounts.Where(x => x.Size != null &&
                                                                       (sizes.Contains(x.Size) || x.Size.Split(',').Any(y => sizes.Contains(y))) &&
                                                                       x.Amount > 0);
                    return sizeWithAmount != null && sizeWithAmount.Any();
                }).ToList();
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
