using Cheshan.Collection.Shop.Database.Entities;

namespace Cheshan.Collection.Shop.Database.Abstract
{
    public interface INotificationRecieversRepository
    {
        Task CreateAsync(string emailAdress);

        Task DeleteAsync(string emailAdress);

        Task<IEnumerable<NotificationRecieverEntity>> GetAllAsync();

        Task AddProductNotificationAsync(Guid productId, string email, string selectedSize);
    }
}
