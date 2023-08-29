using Cheshan.Collection.Shop.Database.Abstract;
using Cheshan.Collection.Shop.Database.Database;
using Cheshan.Collection.Shop.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cheshan.Collection.Shop.Database.Repositories
{
    public class CartsRepository : ICartsRepository
    {
        private readonly DataContext _dataContext;

        public CartsRepository(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task AddToCartAsync(Guid productId, Guid cartId)
        {
            var cart = await _dataContext.Carts.FirstOrDefaultAsync(x => x.Id == cartId);

            if (cart == null)
            {
                throw new Exception("Cart was not found");
            }

            var product = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == productId);

            if(product == null)
            {
                throw new Exception("Product was not found");
            }
            if(cart.Products == null)
            {
                cart.Products = new List<ProductEntity>();
            }
            cart?.Products.Add(product);

            await _dataContext.SaveChangesAsync();
        }

        public async Task<Guid> CreateCartAsync(Guid userId)
        {
            var cart = new CartEntity
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Products = new List<ProductEntity>()
            };

            await _dataContext.Carts.AddAsync(cart);
            await _dataContext.SaveChangesAsync();  
            return cart.Id;
        }

        public async Task<CartEntity> GetAsync(Guid userId)
        {
            var cart = _dataContext.Carts.Include(x=> x.Products).FirstOrDefault(x => x.UserId == userId);

            if(cart == null)
            {
                throw new Exception("Cart was not found");
            }

            return cart;
        }

        public async Task RemoveFromCartAsync(Guid productId, Guid cartId)
        {
            var cart = await _dataContext.Carts.FirstOrDefaultAsync(x => x.Id == cartId);

            if (cart == null)
            {
                throw new Exception("Failed to find cart");
            }

            var product = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == productId);

            if (product == null)
            {
                throw new Exception("Product was not found");
            }

            if (!cart.Products.Contains(product))
            {
                throw new Exception("Cart does not contain this product");
            }

            cart?.Products.Remove(product);

            await _dataContext.SaveChangesAsync();
        }
    }
}
