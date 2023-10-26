using Cheshan.Collection.Shop.Core.Models;

namespace Cheshan.Collection.Shop.Core.Abstract
{
    public interface ICartsService
    {
        Task<CartModel> GetAsync(Guid userId);

        Task<int> AddToCartAsync(Guid productId, string size, Guid userId);

        Task<int> RemoveProductFromCartAsync(Guid productId, string size, Guid userId);

        Task<int> RemoveProductOneSizeFromCartAsync(Guid productId, string size, Guid userId);

        Task RemoveAllFromCartAsync(Guid userId);

        Task<Guid> CreateCartAsync(Guid userId);

        Task ClearCartProductsAsync(Guid userId);
    }
}
