using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.Database.Entities;

namespace Cheshan.Collection.Shop.Core.Abstract
{
    public interface ICartsService
    {
        Task<CartModel> GetAsync(Guid id);

        Task AddToCartAsync(Guid productId,Guid cartId);

        Task RemoveFromCartAsync(Guid productId, Guid cartId);

        Task<Guid> CreateCartAsync(Guid userId);

    }
}
