using Cheshan.Collection.Shop.Core.Models;

namespace Cheshan.Collection.Shop.Core.Abstract
{
    public interface IPurchaseService
    {
        Task<string> CreatePurhcaseAsync(PurchaseModel purchase, Guid userId);

        Task<string> CompletePurchaseAsync(Guid purchaseId);

    }
}
