using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.Database.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Cheshan.Collection.Shop.Core.Services
{
    public class AlfaBankService : IAlfaBankService
    {
        private readonly string prodRegisterString = "https://payment.alfabank.ru/payment/rest/register.do";

        private readonly string enpointUrl = "https://collectionchel.ru/purchase/complete";

        private readonly string prodGetStatusLink = "https://payment.alfabank.ru/payment/rest/getOrderStatus.do";

        private readonly HttpClient client;

        private const string SP1Token = "pfbsq1d47ge6trd2audmpgl127";
        private const string SP2Token = "1ub4testfikatg052k7vg9laf9";

        public AlfaBankService()
        {
            client = new HttpClient();
        }


        public async Task<PaymentLinkEntity> GetPaymentEntityAsync(Guid purchaseId, string token, double price, string SP)
        {
            var paymentStringWithOrder = await FormPaymentStringAsync(purchaseId, token, price);

            var payment = new PaymentLinkEntity
            {
                Id = paymentStringWithOrder.orderId,
                PaymentLink = paymentStringWithOrder.url,
                IsCompleted = false,
                SP = SP
            };

            return payment;
        }

        public async Task<bool> GetStatusAsync(string SP, string orderId)
        {
            try
            {
                string token = string.Empty;
                if (SP == "1")
                {
                    token = SP1Token;
                }
                else
                {
                    token = SP2Token;
                }
                var url = prodGetStatusLink + "?token=" + token + "&orderId=" + orderId;
                string? result = null;
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(url);

                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    var jsonResponse = (JObject)JsonConvert.DeserializeObject(responseBody);

                    result = jsonResponse["OrderStatus"]?.Value<string>();

                }

                if (result == null)
                {
                    return false;
                }
                if (result == "2")
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private async Task<(Guid orderId, string url)> FormPaymentStringAsync(Guid purchaseId, string token, double price)
        {
            var paymentString = prodRegisterString + "?token=" + token + "&orderNumber=" + purchaseId.ToString() + "&amount=" + price + "&returnUrl=" + enpointUrl;
            var formedPaymentString = await GetFormUrlAsync(paymentString);
            return formedPaymentString;
        }

        private async Task<(Guid orderId, string url)> GetFormUrlAsync(string paymentLink)
        {
            var requestResult = await client.GetStringAsync(paymentLink);
            var requestResultJson = (JObject)JsonConvert.DeserializeObject(requestResult);
            string formUrl = requestResultJson["formUrl"].Value<string>();
            var orderId = Guid.Parse(requestResultJson["orderId"].Value<string>());
            return (orderId, formUrl);
        }

    }
}
