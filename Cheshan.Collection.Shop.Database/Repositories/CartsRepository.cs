using Cheshan.Collection.Shop.Database.Abstract;
using Cheshan.Collection.Shop.Database.Database;
using Cheshan.Collection.Shop.Database.Entities;
using Cheshan.Collection.Shop.Database.Enums;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Cheshan.Collection.Shop.Database.Repositories
{
    public class CartsRepository : ICartsRepository
    {
        private readonly DataContext _dataContext;

        public CartsRepository(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task<int> AddToCartAsync(Guid productId, string size, Guid userId)
        {
            var cart = await GetAsync(userId);

            if (cart == null)
            {
                throw new Exception("Cart was not found");
            }

            var product = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == productId);

            if (product == null)
            {
                throw new Exception("Product was not found");
            }
            if (cart.Products == null)
            {
                cart.Products = new List<SizeWithAmountEntity>();
            }

            var existingProductInCart = cart.Products.FirstOrDefault(x => x.Product.Id == productId && string.Equals(x.Size, size, StringComparison.OrdinalIgnoreCase));
            if (existingProductInCart != null)
            {
                existingProductInCart.Amount++;

                _dataContext.Update(cart);
            }
            else
            {
                var productInCart = new SizeWithAmountEntity
                {
                    Id = Guid.NewGuid(),
                    Product = product,
                    Size = size,
                    Amount = 1
                };

                cart?.Products.Add(productInCart);
            }

            await _dataContext.SaveChangesAsync();

            int productsAmount = 0;

            foreach (var productAmount in cart.Products.Select(x => x.Amount))
            {
                productsAmount += productAmount;
            }

            return productsAmount;
        }

        public async Task<Guid> CreateCartAsync(Guid userId)
        {
            var cart = new CartEntity
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Products = new List<SizeWithAmountEntity>()
            };

            await _dataContext.Carts.AddAsync(cart);
            await _dataContext.SaveChangesAsync();
            return cart.Id;
        }

        public async Task<CartEntity> GetAsync(Guid userId)
        {
            var cart = _dataContext.Carts.Include(x => x.Products).ThenInclude(x => x.Product).FirstOrDefault(x => x.UserId == userId);

            if (cart == null)
            {
                throw new Exception("Cart was not found");
            }

            return cart;
        }

        public async Task RemoveAllFromCartAsync(Guid userId)
        {
            var cart = await GetAsync(userId);

            cart.Products = new List<SizeWithAmountEntity>();

            await _dataContext.SaveChangesAsync();
        }

        public async Task ClearCartProductsAsync(Guid userId)
        {
            var cart = await GetAsync(userId);

            foreach (var productInCart in cart.Products)
            {

                var product = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == productInCart.Product.Id);
                var sizeWithAmount = product?.SizesWithAmounts.FirstOrDefault(x => string.Equals(x.Size, productInCart.Size, StringComparison.OrdinalIgnoreCase));
                sizeWithAmount.Amount--;

                _dataContext.Update(product);
            }

            await RemoveAllFromCartAsync(userId);
        }

        public async Task<int> DecreaseAmountOfProductInCartAsync(Guid productId, string size, Guid userId, DecreaseAmount amount)
        {
            var cart = await GetAsync(userId) ?? throw new Exception("Cart was not found");
            

            var product = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == productId);

            if (product == null)
            {
                throw new Exception("Product was not found");
            }

            var itemToDecrease = cart?.Products.FirstOrDefault(x => x.Product.Id == productId && x.Size == size);

            if (itemToDecrease == null)
            {
                throw new Exception("Cart does not contain this product");
            }

            if (amount == DecreaseAmount.All)
            {
                cart?.Products.Remove(itemToDecrease);
            }
            else
            {
                itemToDecrease.Amount--;

                if (itemToDecrease.Amount <= 0)
                {
                    cart?.Products.Remove(itemToDecrease);
                }
                else
                {
                    _dataContext.Update(cart);
                }
            }

            await _dataContext.SaveChangesAsync();

            return cart.Products.Count();

        }
    }
}
