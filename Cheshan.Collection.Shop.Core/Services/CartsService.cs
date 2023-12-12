using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.Core.Mappers;
using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.Database.Abstract;

namespace Cheshan.Collection.Shop.Core.Services
{
    public class CartsService : ICartsService
    {

        private readonly ICartsRepository _repository;

        public CartsService(ICartsRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<int> AddToCartAsync(Guid productId, string size, Guid userId)
        {
            try
            {
                var newAmount = await _repository.AddToCartAsync(productId, size, userId);
                return newAmount;
            }
            catch
            {
                throw;
            }
        }
        public async Task<Guid> CreateCartAsync(Guid userId)
        {
            var createdCartGuid = await _repository.CreateCartAsync(userId);

            return createdCartGuid;
        }

        public async Task<CartModel> GetAsync(Guid userId, bool asNoTracking = false)
        {
            try
            {
                var cartEntity = await _repository.GetAsync(userId, asNoTracking);
                return cartEntity.ToModel();

            }
            catch
            {
                throw;
            }
        }

        public async Task RemoveAllFromCartAsync(Guid userId)
        {
            try
            {
                await _repository.RemoveAllFromCartAsync(userId);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> RemoveProductFromCartAsync(Guid productId, string size, Guid userId)
        {
            try
            {
                var newAmount = await _repository.DecreaseAmountOfProductInCartAsync(productId, size, userId, Database.Enums.DecreaseAmount.All);
                return newAmount;

            }
            catch
            {
                throw;
            }
        }

        public async Task ClearCartProductsAsync(Guid userId)
        {
            try
            {
                await _repository.ClearCartProductsAsync(userId);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> RemoveProductOneSizeFromCartAsync(Guid productId, string size, Guid userId)
        {
            try
            {
                var newAmount = await _repository.DecreaseAmountOfProductInCartAsync(productId, size, userId, Database.Enums.DecreaseAmount.SingleItem);
                return newAmount;

            }
            catch
            {
                throw;
            }
        }
    }
}
