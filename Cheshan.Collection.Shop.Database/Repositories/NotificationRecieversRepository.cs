using Cheshan.Collection.Shop.Database.Abstract;
using Cheshan.Collection.Shop.Database.Database;
using Cheshan.Collection.Shop.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cheshan.Collection.Shop.Database.Repositories
{
    public class NotificationRecieversRepository: INotificationRecieversRepository
    {
        private readonly DataContext _context;

        public NotificationRecieversRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task CreateAsync(string emailAdress)
        {
            var newReciever = new NotificationRecieverEntity
            {
                Id = Guid.NewGuid(),
                GmailAdress = emailAdress,
            };

            await _context.AddAsync(newReciever);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string emailAdress)
        {
            var newReciever = new NotificationRecieverEntity
            {
                Id = Guid.NewGuid(),
                GmailAdress = emailAdress,
            };


            var recieverToDelete = await _context.Notifications.FirstOrDefaultAsync(x => x.GmailAdress == newReciever.GmailAdress);

            if (recieverToDelete != null)
            {
                _context.Notifications.Remove(recieverToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<NotificationRecieverEntity>> GetAllAsync()
        {
            var result = await _context.Notifications.ToArrayAsync();
            return result;
        }

        public async Task AddProductNotificationAsync(Guid productId, string email, string selectedSize)
        {
            var entity = new ProductNotificationRecieverEntity
            {
                Email = email,
                ProductId = productId,
                Size = selectedSize
            };

            await _context.ProductNotifications.AddAsync(entity);

            await _context.SaveChangesAsync();
        }

    }
}
