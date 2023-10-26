using Cheshan.Collection.Shop.Database.Entities;

namespace Cheshan.Collection.Shop.Database.Abstract
{
    public interface IPurchasesRepository
    {
        Task CreatePurchaseAsync(PurchaseEntity purchase);

        Task CompletePurchaseAsync(Guid purchaseId, bool purchaseComplited);

        Task UpdatePurchaseAsync(PurchaseEntity entity);

        Task<PurchaseEntity?> GetByPaymentIdAsync(Guid paymentId);

    }
}
