using Cheshan.Collection.Shop.Database.Models;

namespace Cheshan.Collection.Shop.Database.Abstract
{
    public interface ICartsRepository
    {
        Task<CartEntity> GetAsync(Guid id);

        Task<Guid> CreateCartAsync(Guid userId);

        Task AddToCartAsync(Guid productId, Guid cartId);

        Task RemoveFromCartAsync(Guid productId, Guid cartId);
    }
}
