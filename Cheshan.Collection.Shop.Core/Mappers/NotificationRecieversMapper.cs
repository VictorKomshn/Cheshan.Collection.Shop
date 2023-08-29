using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.Database.Entities;

namespace Cheshan.Collection.Shop.Core.Mappers
{
    public static class NotificationRecieversMapper
    {
        public static NotificationRecieverEntity ToEntity(this NotificationRecieverModel model)
        {
            return new NotificationRecieverEntity
            {
                GmailAdress = model.GmailAdress,
                Id = model.Id,
            };
        }

        public static NotificationRecieverModel ToModel(this NotificationRecieverEntity model)
        {
            return new NotificationRecieverModel
            {
                GmailAdress = model.GmailAdress,
                Id = model.Id,
            };
        }
    }
}
