using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.Database.Entities;

namespace Cheshan.Collection.Shop.Core.Mappers
{
    public static class PurchaseMapper
    {
        public static PurchaseEntity ToEntity(this PurchaseModel model, Guid userId, double priceForSP1, double priceForSP2)
        {
            var entity = new PurchaseEntity
            {
                IsComplited = false,
                DeliveryAdress = model.DeliveryAdress,
                DeliveryType = model.DeliveryType,
                Email = model.Email,
                Name = model.Name,
                PaymentType = model.PaymentType,
                Phone = model.Phone,
                Promocode = model.Promocode,
                SecondName = model.SecondName,
                UserId = userId,
                PriceForSP1 = priceForSP1,
                PriceForSP2 = priceForSP2,
            };

            return entity;
        }
    }
}
