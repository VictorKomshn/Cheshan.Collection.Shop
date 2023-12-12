using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.Core.Mappers;
using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.Database.Abstract;
using Cheshan.Collection.Shop.Database.Entities;

namespace Cheshan.Collection.Shop.Core.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly ICartsService _cartsService;
        private readonly IPurchasesRepository _repository;
        private readonly IEmailService _emailService;
        private readonly IAlfaBankService _alfabankService;

        private readonly string devString = "https://alfa.rbsuat.com/payment/rest/register.do";

        private const string SP1Name = "745100194346";
        private const string SP2Name = "745100227337";
        private const string SP1Token = "pfbsq1d47ge6trd2audmpgl127"; // r-collectionchel_ru
        private const string SP2Token = "1ub4testfikatg052k7vg9laf9"; // r-collectionchel


        private const string SuccessString = "/purchase/success";

        public PurchaseService(ICartsService cartsService,
                               IEmailService emailService,
                               IPurchasesRepository repository,
                               IAlfaBankService alfabankService)
        {
            _cartsService = cartsService ?? throw new ArgumentNullException(nameof(cartsService));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _alfabankService = alfabankService ?? throw new ArgumentNullException(nameof(alfabankService));
        }

        public async Task<string> SaveCashPurchase(PurchaseModel purchase, Guid userId)
        {
            try
            {
                var productsFromCart = await _cartsService.GetAsync(userId);
                var prices = GetPrices(productsFromCart);

                var priceForSP1 = prices.priceForSp1;
                var priceForSP2 = prices.priceForSp2;

                var purchaseEntity = purchase.ToEntity(userId, priceForSP1, priceForSP2);
                purchaseEntity.PaymentLinksWithPurchase = new List<PaymentLinkEntity>();
                purchaseEntity.Id = Guid.NewGuid();

                var purchaseId = purchaseEntity.GetHashCode();
                purchaseEntity.PurchaseId = purchaseId.ToString();


                var purchasedProducts = productsFromCart.Products.Select(x => new PurchasedProductEntity
                {
                    Amount = x.Amount,
                    Id = Guid.NewGuid(),
                    Name = x.Product.Name,
                    Photo = x.Product.MainPhoto,
                    ProductId = x.Product.Id,
                    Size = x.Size,
                    SKU = x.Product.SKU,
                    Price = x.Product.SalePrice ?? x.Product.Price
                }).ToArray();

                purchaseEntity.PurchasedProducts = purchasedProducts;

                await _repository.CreatePurchaseAsync(purchaseEntity);

                await _emailService.SendPurchaseNotificationToCustomer(purchase.Email, purchase.Name, purchase.Phone, purchaseId.ToString(), purchase.DeliveryAdress, purchase.DeliveryType, purchase.PaymentType, purchasedProducts);

                await _emailService.SendPurchaseNotificationToAdministration(purchase.Email, purchase.Name, purchase.Phone, purchaseId.ToString(), purchase.DeliveryAdress, purchase.DeliveryType, purchase.PaymentType, purchasedProducts);

                await _cartsService.ClearCartProductsAsync(userId);


                return SuccessString + "/" + purchaseId.ToString();
            }
            catch
            {
                throw;
            }

        }

        public async Task<string> CompletePurchaseAsync(Guid purchaseId)
        {
            var purchase = await _repository.GetByPaymentIdAsync(purchaseId);
            if (purchase != null)
            {
                var paymentLink = purchase?.PaymentLinksWithPurchase.FirstOrDefault(x => x.Id == purchaseId);
                if (paymentLink != null)
                {
                    var status = await _alfabankService.GetStatusAsync(paymentLink.SP, paymentLink.Id.ToString());
                    paymentLink.IsCompleted = status;
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

            return SuccessString + "/" + purchase.PurchaseId;
        }


        /// <summary>
        /// Создание объекта заказа
        /// </summary>
        /// <param name="purchase"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<string> CreatePurhcaseAsync(PurchaseModel purchase, Guid userId)
        {
            var productsFromCart = await _cartsService.GetAsync(userId, true);
            var prices = GetPrices(productsFromCart);

            var priceForSP1 = prices.priceForSp1;
            var priceForSP2 = prices.priceForSp2;

            var purchaseEntity = purchase.ToEntity(userId, priceForSP1, priceForSP2);

            purchaseEntity.PurchasedProducts = productsFromCart.Products.Select(x => new PurchasedProductEntity
            {
                Amount = x.Amount,
                Id = Guid.NewGuid(),
                Name = x.Product.Name,
                Photo = x.Product.MainPhoto,
                ProductId = x.Product.Id,
                Size = x.Size,
                SKU = x.Product.SKU,
                Price = x.Product.SalePrice ?? x.Product.Price
            }).ToArray();

            purchaseEntity.PaymentLinksWithPurchase = new List<PaymentLinkEntity>();
            purchaseEntity.Id = Guid.NewGuid();

            PaymentLinkEntity payment1 = null;
            PaymentLinkEntity payment2 = null;

            if (priceForSP1 != 0)
            {
                payment1 = await _alfabankService.GetPaymentEntityAsync(purchaseEntity.Id, SP1Token, priceForSP1, "1");
                purchaseEntity.PaymentLinksWithPurchase.Add(payment1);
            }
            if (priceForSP2 != 0)
            {
                payment2 = await _alfabankService.GetPaymentEntityAsync(purchaseEntity.Id, SP2Token, priceForSP2, "2");
                purchaseEntity.PaymentLinksWithPurchase.Add(payment2);
            }

            purchaseEntity.PurchaseId = purchaseEntity.GetHashCode().ToString();

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

        private (double priceForSp1, double priceForSp2) GetPrices(CartModel cart)
        {
            double priceForSP1 = 0;
            double priceForSP2 = 0;
            foreach (var product in cart.Products)
            {
                if (product.Product.SP == SP1Name)
                {
                    if (product.Product.SalePrice.HasValue && product.Product.SalePrice != null && product.Product.SalePrice != 0)
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

            priceForSP1 = priceForSP1 * 100;
            priceForSP2 = priceForSP2 * 100;

            return new(priceForSP1, priceForSP2);
        }
    }
}
