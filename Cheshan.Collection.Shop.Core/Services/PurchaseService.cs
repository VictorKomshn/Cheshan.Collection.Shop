using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.Core.Mappers;
using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.Database.Abstract;
using Cheshan.Collection.Shop.Database.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Cheshan.Collection.Shop.Core.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly ICartsService _cartsService;
        private readonly IPurchasesRepository _repository;

        private readonly string prodString = "https://payment.alfabank.ru/payment/rest/register.do";
        private readonly string devString = "https://alfa.rbsuat.com/payment/rest/register.do";
        private readonly string enpointUrl = "https://localhost:44352/purchase/complete";

        private const string SP1Name = "745100194346";
        private const string SP2Name = "745100227337";
        private const string SP1Token = "pfbsq1d47ge6trd2audmpgl127"; // r-collectionchel_ru
        private const string SP2Token = "1ub4testfikatg052k7vg9laf9"; // r-collectionchel


        private const string SuccessString = "https://localhost:44352/purchase/success";



        private readonly HttpClient client;

        public PurchaseService(ICartsService cartsService, IPurchasesRepository repository)
        {
            _cartsService = cartsService;
            _repository = repository;
            client = new HttpClient();
        }

        public async Task<string> CompletePurchaseAsync(Guid purchaseId)
        {
            var purchase = await _repository.GetByPaymentIdAsync(purchaseId);
            if (purchase != null)
            {
                var paymentLink = purchase?.PaymentLinksWithPurchase.FirstOrDefault(x => x.Id == purchaseId);
                if (paymentLink != null)
                {
                    paymentLink.IsCompleted = true;
                }
            }

            await _repository.UpdatePurchaseAsync(purchase);

            foreach (var payment in purchase.PaymentLinksWithPurchase)
            {
                if (payment.IsCompleted == false)
                {
                    return payment.PaymentLink;
                }
            }

            await _repository.CompletePurchaseAsync(purchase.Id, true);

            return SuccessString;
        }

        public async Task<string> CreatePurhcaseAsync(PurchaseModel purchase, Guid userId)
        {
            var productsFromCart = await _cartsService.GetAsync(userId);
            int priceForSP1 = 0;
            int priceForSP2 = 0;
            foreach (var product in productsFromCart.Products)
            {
                if (product.Product.SP == SP1Name)
                {
                    if (product.Product.SalePrice.HasValue &&  product.Product.SalePrice != null && product.Product.SalePrice != 0)
                    {
                        priceForSP1 += product.Product.SalePrice.Value * product.Amount;
                    }
                    else
                    {
                        priceForSP1 += product.Product.Price * product.Amount;
                    }
                }
                else if (product.Product.SP == SP2Name)
                {
                    if (product.Product.SalePrice.HasValue && product.Product.SalePrice != null && product.Product.SalePrice != 0)
                    {
                        priceForSP2 += product.Product.SalePrice.Value * product.Amount;
                    }
                    else
                    {
                        priceForSP2 += product.Product.Price * product.Amount;
                    }
                }
            }

            var purchaseEntity = purchase.ToEntity(userId, priceForSP1, priceForSP2);
            purchaseEntity.PaymentLinksWithPurchase = new List<PaymentLinkEntity>();
            purchaseEntity.Id = Guid.NewGuid();
            PaymentLinkEntity payment1 = null;
            PaymentLinkEntity payment2 = null;

            if (priceForSP1 != 0)
            {
                payment1 = await GetPaymentEntity(purchaseEntity.Id, SP1Token, 100);
                purchaseEntity.PaymentLinksWithPurchase.Add(payment1);
            }
            if (priceForSP2 != 0)
            {
                payment2 = await GetPaymentEntity(purchaseEntity.Id, SP2Token, 100);
                purchaseEntity.PaymentLinksWithPurchase.Add(payment2);
            }

            await _repository.CreatePurchaseAsync(purchaseEntity);
            if (priceForSP1 != 0)
            {
                return payment1.PaymentLink;
            }
            else
            {
                return payment2.PaymentLink;
            }
        }

        private async Task<PaymentLinkEntity> GetPaymentEntity(Guid purchaseId, string token, int price)
        {
            var paymentStringWithOrder = await FormPaymentStringAsync(purchaseId, token, price);

            var payment = new PaymentLinkEntity
            {
                Id = paymentStringWithOrder.orderId,
                PaymentLink = paymentStringWithOrder.url,
                IsCompleted = false,
            };

            return payment;
        }

        private async Task<(Guid orderId, string url)> FormPaymentStringAsync(Guid purchaseId, string token, int price)
        {
            var paymentString = prodString + "?token=" + token + "&orderNumber=" + purchaseId.ToString() + "&amount=" + price + "&returnUrl=" + enpointUrl;
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
