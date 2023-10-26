using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.Core.Mappers;
using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.Database.Abstract;

namespace Cheshan.Collection.Shop.Core.Services
{
    public class NotificationRecieversService : INotificationRecieversService
    {

        private readonly INotificationRecieversRepository _repository;

        public NotificationRecieversService(INotificationRecieversRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task AddNotificationAsync(string emailAdress)
        {
            await _repository.CreateAsync(emailAdress);
        }

        public async Task DeleteNotificationAsync(string emailAdress)
        {
            await _repository.DeleteAsync(emailAdress);
        }

        public async Task<IEnumerable<NotificationRecieverModel>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();

            return result.Select(x => x.ToModel());
        }

        public async Task AddProductNotificationAsync(Guid productId, string email, string selectedSize)
        {
            await _repository.AddProductNotificationAsync(productId, email, selectedSize);
        }
    }
}
