using Cheshan.Collection.Shop.Database.Entities;

namespace Cheshan.Collection.Shop.Core.Abstract
{
    public interface IEmailService
    {
        /// <summary>
        /// Отправка увдомления об успешной покупке покупателю
        /// </summary>
        /// <param name="customerEmail"></param>
        /// <param name="customerName"></param>
        /// <param name="customerPhone"></param>
        /// <param name="purchaseId"></param>
        /// <param name="adress"></param>
        /// <param name="deliveryType"></param>
        /// <param name="paymentType"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        public Task SendPurchaseNotificationToCustomer(string customerEmail, string customerName, string? customerPhone, string purchaseId, string adress, string deliveryType, string paymentType, IEnumerable<PurchasedProductEntity> products);

        /// <summary>
        /// Отправка уведомления об успешной покупке администрации
        /// </summary>
        /// <param name="customerEmail"></param>
        /// <param name="customerName"></param>
        /// <param name="customerPhone"></param>
        /// <param name="purchaseId"></param>
        /// <param name="adress"></param>
        /// <param name="deliveryType"></param>
        /// <param name="paymentType"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        Task SendPurchaseNotificationToAdministration(string customerEmail, string customerName, string? customerPhone, string purchaseId, string adress, string deliveryType, string paymentType, IEnumerable<PurchasedProductEntity> products);
    }
}
