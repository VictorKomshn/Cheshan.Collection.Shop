using Cheshan.Collection.Shop.Database.Abstract;
using Cheshan.Collection.Shop.Database.Database;
using Cheshan.Collection.Shop.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public async Task<ProductEntity> GetAsync(Guid id)
        {
            var result = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                throw new Exception("Product was not found");
            }

            return result;
        }

        public async Task<IEnumerable<ProductEntity>> GetByConditionAsync(Expression<Func<ProductEntity, bool>> condition)
        {
            var query = _dataContext.Products.AsQueryable();

            var result = await query.Where(condition).ToListAsync();

            return result;
        }

        public async Task<ProductEntity> UpdateAsync(Guid id, ProductEntity newEntity)
        {
            try
            {
                //var oldEntity = await GetAsync(id);
                //oldEntity = newEntity;
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
