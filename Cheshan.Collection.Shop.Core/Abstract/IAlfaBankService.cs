using Cheshan.Collection.Shop.Database.Entities;

namespace Cheshan.Collection.Shop.Core.Abstract
{
    public interface IAlfaBankService
    {
        Task<PaymentLinkEntity> GetPaymentEntityAsync(Guid purchaseId, string token, double price, string SP);

        Task<bool> GetStatusAsync(string SP, string orderId);
    }
}
