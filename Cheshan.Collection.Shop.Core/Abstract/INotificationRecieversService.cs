using Cheshan.Collection.Shop.Core.Models;

namespace Cheshan.Collection.Shop.Core.Abstract
{
    public interface INotificationRecieversService
    {
        Task AddNotificationAsync(string emailAdress);

        Task DeleteNotificationAsync(string emailAdress);

        Task<IEnumerable<NotificationRecieverModel>> GetAllAsync();

        Task AddProductNotificationAsync(Guid productId, string email, string selectedSize);
    }
}
