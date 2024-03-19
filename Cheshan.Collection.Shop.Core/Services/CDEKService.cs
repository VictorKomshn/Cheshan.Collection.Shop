using Azure.Core;
using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.Core.Extensions;
using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.Database.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Cheshan.Collection.Shop.Core.Services
{
    public class CDEKService : ICDEKService
    {
        private CDEKAdressModel? _selectedDestinationAdress;

        private readonly string JsonMediaType = "application/json";

        public async Task<Guid> RegisterDeliveryRequestAsync(PurchaseEntity purchase)
        {
            string testLink = "https://api.edu.cdek.ru/v2/orders";
            string prodLink = "https://api.cdek.ru/v2/orders";

            var successfulRequestState = "SUCCESSFUL ";
            var unsuccessfulRequestState = "INVALID ";

            try
            {
                var request = CreateRequest(purchase);

                var client = new HttpClient();
                var content = new StringContent(request.ToString(), Encoding.UTF8, JsonMediaType);
                var authToken = await GetAuthorizationHeaderAsync();

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var response = await client.PostAsync(prodLink, content);

                var responseData = await response.Content.ReadAsStringAsync();
                var responseJson = JsonConvert.DeserializeObject<JObject>(responseData);

                var state = responseJson?["requests"].First?["state"];

                if (state?.ToString() == unsuccessfulRequestState)
                {
                    var errorsArray = responseJson["requests"].First["errors"] as JArray;
                    throw new Exception(string.Join("\n", errorsArray));
                }
                else
                {
                    var requestId = responseJson["entity"]?["uuid"];
                    if (requestId != null)
                    {
                        return Guid.Parse(requestId.ToString());
                    }
                    else
                    {
                        throw new Exception("Непредвиденный ответ от CDEK API");
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Метод сохранения выбранного адреса
        /// </summary>
        /// <param name="adress"></param>
        public void SaveAdress(CDEKAdressModel adress)
        {
            _selectedDestinationAdress = adress;
        }

        /// <summary>
        /// Метод отправки асинхронного http запроса на CDEK API
        /// </summary>
        /// <param name="method">Название метода</param>
        /// <param name="requestData">параметры запроса</param>
        /// <returns></returns>
        public async Task<string> SendHttpRequestAsync(string method, Dictionary<string, string> requestData)
        {

            HttpClient _httpClient = new HttpClient();
            string _baseUrl = "https://api.cdek.ru/v2";

            var authToken = await GetAuthorizationHeaderAsync();

            var url = $"{_baseUrl}/{method}";
            var requestContent = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, JsonMediaType);
            _httpClient.DefaultRequestHeaders.Add("Accept", JsonMediaType);
            _httpClient.DefaultRequestHeaders.Add("X-App-Name", "widget_pvz");
            _httpClient.DefaultRequestHeaders.Add("X-Service-Version", "3.9.5");

            if (authToken != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
            }

            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
                Content = requestContent
            };

            var response = await _httpClient.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}");
            }

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string?> GetOrderIdAsync(string requestId)
        {
            var requestData = await GetRequestInfo(requestId);
            var requestJson = JsonConvert.DeserializeObject<JObject>(requestData);
            return requestJson?["entity"]?["cdek_number"]?.Value<string>();
        }

        public async Task<string?> GetAdressAsync(string requestId)
        {
            var requestData = await GetRequestInfo(requestId);
            var requestJson = JsonConvert.DeserializeObject<JObject>(requestData);
            return requestJson?["entity"]?["to_location"]?["address"]?.Value<string>();
        }

        public async Task<string> GetRequestInfo(string requestId)
        {
            var testLink = "https://api.edu.cdek.ru/v2/orders/";
            var prodLink = "https://api.cdek.ru/v2/orders/";

            HttpClient _httpClient = new HttpClient();
            var requestLink = prodLink + requestId;

            var authToken = await GetAuthorizationHeaderAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            var result = await _httpClient.GetAsync(requestLink);

            return await result.Content.ReadAsStringAsync();
        }

        public async Task<string> GetDileveryPoints()
        {

            var testLink = "https://api.edu.cdek.ru/v2/deliverypoints?";
            var prodLink = "https://api.cdek.ru/v2/orders/";

            HttpClient _httpClient = new HttpClient();
            var requestLink = prodLink + "postal_code=454091&type=PVZ";

            var authToken = await GetAuthorizationHeaderAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            var result = await _httpClient.GetAsync(requestLink);

            return await result.Content.ReadAsStringAsync();
        }


        #region Private methods

        /// <summary>
        /// Создание тела JSON запроса
        /// </summary>
        /// <param name="purchase"></param>
        /// <returns></returns>
        private JObject CreateRequest(PurchaseEntity purchase)
        {
            var request = GetBaseRequest(purchase.PurchaseId);

            var phones = new JObject()
            {
                { "number",  purchase.Phone  }
            };

            var recepient = new JObject
            {
                { "name" , purchase.Name} ,
                { "phones" , phones },
                { "email" , purchase.Email },
            };

            request.Add("recipient", recepient);

            var packages = GetPackagesJson(purchase.PurchaseId);
            request.Add("packages", packages);

            return request;
        }

        /// <summary>
        /// Создание JSON с основной информацией о заказе: отправителе, заказанных товарах, месте отправки и получения
        /// </summary>
        /// <param name="orderId">Номер заказа</param>
        /// <returns></returns>
        private JObject GetBaseRequest(string orderId)
        {

            var request = new JObject
            {
                { "type","2" },
                { "tariff_code", 483 },         // Экспресс склад-склад
                { "number",orderId },           // Номер заказа в системе collectionchel
                { "shipment_point", "CHEL71"},    // Код ПВЗ СДЭК, на который будет производиться самостоятельный привоз клиентом
                { "delivery_point", _selectedDestinationAdress.Code },    // Код ПВЗ СДЭК, на который будет производиться доставка
            };

            var senderInfo = new JObject
            {
                { "company" ,"Collectionchel" },
                { "name","Суворина Анна Сергеевна" },
                { "email","collecti0n.info@yandex.ru" },
            };

            var phones = new JObject()
            {
                { "number", "+73512636623" }
            };

            senderInfo.Add("phones", phones);

            request.Add("sender", senderInfo);

            return request;
        }

        /// <summary>
        /// Создание JSON с информацией о упаковке и отправляемых товарах
        /// </summary>
        /// <param name="products"> Заказанные продукты</param>
        /// <param name="purchaseId">Номер заказа</param>
        /// <returns></returns>
        private JObject GetPackagesJson(string purchaseId)
        {
            var packagesObject = new JObject
                {
                    {"number", purchaseId },
                    {"weight", 1 },
                    {"length",1 },
                    {"width",1 },
                    {"height",1 },
                    {"comment","Необходим перевес при передаче товара в ПВЗ" },
                };

            var items = new JArray();
            //foreach (var item in products)
            //{
            //    var itemInfo = new JObject
            //    {
            //        {"name",item.Name },
            //        {"ware_key",item.SKU },
            //        {"cost",item.Price },
            //        {"weight",1 },
            //        {"amount",item.Amount }
            //    };


            //    var payment = new JObject
            //    {
            //        {"value",0 },
            //    };

            //    itemInfo.Add("payment", payment);

            //    items.Add(itemInfo);
            //}

            packagesObject.Add("items", items);

            return packagesObject;
        }

        /// <summary>
        /// Возвращает authtoken без Bearer
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        private async Task<string> GetAuthorizationHeaderAsync()
        {
            string testAuthLink = "https://api.edu.cdek.ru/v2/oauth/token?parameters";
            string prodAuthLink = "https://api.cdek.ru/v2/oauth/token?parameters";

            var client = new HttpClient();
            var request = new JObject()
            {
                { "grant_type" ,    "client_credentials" },
                { "client_id" ,     "AKjmf1312fwyy5mNZOhpJoDC3A9nMBI8" },
                { "client_secret",  "AtKB8c4uXzer1nkNICIxvI6yTizQF5Dm" }
            };

            var content = new FormUrlEncodedContent(request.ToKeyValue());

            var response = await client.PostAsync(prodAuthLink, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(responseContent))
            {
                var resultJson = (JObject?)JsonConvert.DeserializeObject(responseContent);
                var accessToken = resultJson?["access_token"]?.Value<string>();
                if (accessToken == null)
                {
                    var error = resultJson?["error"]?.Value<string>();
                    var errorDescription = resultJson?["error_description"]?.Value<string>();
                    throw new Exception("Ошибки в запросе:\t" + error + ":\t" + errorDescription);
                }
                else
                {
                    return accessToken;
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(responseContent));
            }
        }

        #endregion
    }
}
