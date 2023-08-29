using Cheshan.Collection.Shop.Database.Entities;

namespace Cheshan.Collection.Shop.Database.Abstract
{
    public interface ICartsRepository
    {
        Task<CartEntity> GetAsync(Guid userId);

        Task<Guid> CreateCartAsync(Guid userId);

        Task AddToCartAsync(Guid productId, Guid cartId);

        Task RemoveFromCartAsync(Guid productId, Guid cartId);
    }
}
