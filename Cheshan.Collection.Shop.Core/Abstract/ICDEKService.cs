using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.Database.Entities;

namespace Cheshan.Collection.Shop.Core.Abstract
{
    public interface ICDEKService
    {
        Task<Guid> RegisterDeliveryRequestAsync(PurchaseEntity purchase);

        /// <summary>
        /// Метод отправки асинхронного http запроса на CDEK API
        /// </summary>
        /// <param name="method"></param>
        /// <param name="requestData"></param>
        /// <returns></returns>
        Task<string> SendHttpRequestAsync(string method, Dictionary<string, string> requestData);

        Task<string?> GetOrderIdAsync(string requestId);

        Task<string?> GetAdressAsync(string requestId);
    }
}
