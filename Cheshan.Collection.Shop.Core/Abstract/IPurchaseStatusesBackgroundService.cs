using Cheshan.Collection.Shop.Database.Entities;

namespace Cheshan.Collection.Shop.Core.Abstract
{
    public interface IPurchaseStatusesBackgroundService
    {
        void CachePurchase(Guid purchaseId, PurchaseEntity entity);
    }
}
