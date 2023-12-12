using Cheshan.Collection.Shop.Database.Entities;
using Cheshan.Collection.Shop.Database.Enums;

namespace Cheshan.Collection.Shop.Database.Abstract
{
    public interface ICartsRepository
    {
        Task ClearOldCarts();

        Task<CartEntity> GetAsync(Guid userId, bool asNoTracking = false);

        Task<Guid> CreateCartAsync(Guid userId);

        Task<int> AddToCartAsync(Guid productId, string size, Guid userId);

        Task RemoveAllFromCartAsync(Guid userId);

        Task ClearCartProductsAsync(Guid userId);

        Task<int> DecreaseAmountOfProductInCartAsync(Guid productId, string size, Guid userId, DecreaseAmount amount);

    }
}
