using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.Core.EmailCompositions.ViewModels;
using Cheshan.Collection.Shop.Database.Abstract;
using Cheshan.Collection.Shop.Database.Database;
using Cheshan.Collection.Shop.Database.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace Cheshan.Collection.Shop.Core.Services
{
    public class PurchaseStatusesBackgroundService : BackgroundService
    {
        private readonly IPurchasesRepository _purchasesRepository;
        private readonly ICartsRepository _cartsRepository;
        private readonly IProductsRepository _productsRepository;

        private readonly ICDEKService _cdekService;
        private readonly IAlfaBankService _alfaBankService;
        private readonly IEmailService _emailService;

        private readonly TimeSpan _statusThreshold = TimeSpan.FromMilliseconds(1000);
        private readonly TimeSpan _purchaseThreshold = TimeSpan.FromMilliseconds(1000);
        private const string errorsRoot = @"../Cheshan.Collection.Shop.Core/emailErrors.txt";

        public PurchaseStatusesBackgroundService(IServiceProvider services,
                                                 IEmailService emailService,
                                                 IAlfaBankService alfaBankService,
                                                 ICDEKService cdekService)
        {
            var scope = services.CreateScope();

            _purchasesRepository = new PurchasesRepository(scope.ServiceProvider.GetService<DataContext>());
            _cartsRepository = new CartsRepository(scope.ServiceProvider.GetService<DataContext>());
            _productsRepository = new ProductsRepository(scope.ServiceProvider.GetService<DataContext>());

            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _alfaBankService = alfaBankService ?? throw new ArgumentNullException(nameof(alfaBankService));
            _cdekService = cdekService ?? throw new ArgumentNullException(nameof(cdekService)); ;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var stopwatch = Stopwatch.StartNew();
                var recentPurchases = await _purchasesRepository.GetIncompleteRecent();
                bool newPurchaseAdded = false;
                await File.AppendAllLinesAsync(errorsRoot, new[] { $"{DateTime.UtcNow} info:\t starting service" });

                while (!stoppingToken.IsCancellationRequested)
                {

                    await Task.Delay(_statusThreshold);

                    if (stopwatch.Elapsed >= _purchaseThreshold)
                    {
                        newPurchaseAdded = false;
                        var previousPurchases = recentPurchases;
                        recentPurchases = await _purchasesRepository.GetIncompleteRecent();
                        if (previousPurchases != null &&
                            recentPurchases != null &&
                            previousPurchases.Any() &&
                            recentPurchases.Any() &&
                            previousPurchases.OrderByDescending(x => x.Created).First().Id == recentPurchases.OrderByDescending(x => x.Created).First().Id)
                        {
                            newPurchaseAdded = true;
                        }
                        stopwatch.Restart();
                    }
                    foreach (var purchase in recentPurchases)
                    {
                        if (newPurchaseAdded)
                        {
                            await File.AppendAllLinesAsync(errorsRoot, new[] { $"{DateTime.UtcNow} info:\t new purchase - P{purchase.Id}" });
                        }

                        var paymentComplete = false;

                        if (purchase.PriceForSP1 > 0)
                        {
                            var paymentLinkEntity = purchase.PaymentLinksWithPurchase.First(x => x.SP == "1");

                            var status = await _alfaBankService.GetStatusAsync("1", paymentLinkEntity.Id.ToString());

                            paymentLinkEntity.IsCompleted = status;

                        }

                        if (purchase.PriceForSP2 > 0)
                        {
                            var paymentLinkEntity = purchase.PaymentLinksWithPurchase.First(x => x.SP == "2");

                            var status = await _alfaBankService.GetStatusAsync("2", paymentLinkEntity.Id.ToString());

                            paymentLinkEntity.IsCompleted = status;
                        }

                        if (purchase.PaymentLinksWithPurchase.All(x => x.IsCompleted == true))
                        {
                            purchase.IsComplited = true;

                            string? cdekOrderNumber = null;
                            if (purchase.DeliveryType == "cdek")
                            {
                                var cdekOrderId = await _cdekService.RegisterDeliveryRequestAsync(purchase);
                                cdekOrderNumber = await _cdekService.GetOrderIdAsync(cdekOrderId.ToString());
                                var adress = await _cdekService.GetAdressAsync(cdekOrderId.ToString());
                                purchase.CDEKOrderNumber = cdekOrderNumber;
                                purchase.DeliveryAdress = adress ?? "Ошибка, пожалуйста, обратитесь в службу поддержки";
                            }

                            await _purchasesRepository.UpdatePurchaseAsync(purchase);

                            await File.AppendAllLinesAsync(errorsRoot, new[] { $"{DateTime.UtcNow} info:\tpurchase " + purchase.Id + " complete, sending notifications" });

                            var purchasedProducts = purchase.PurchasedProducts;

                            var emailProducts = purchasedProducts.Select(x =>
                            {
                                var product = _productsRepository.GetAsync(x.ProductId).GetAwaiter().GetResult();
                                return new EmailProductModel
                                {
                                    Name = x.Name,
                                    Photo = x.Photo,
                                    Price = x.Price,
                                    SalePrice = product.SalePrice,
                                    SKU = x.SKU,
                                    Size = x.Size,
                                    Amount = x.Amount,
                                    Brand = product.Name
                                };
                            });

                            await _emailService.SendPurchaseNotificationToCustomer(purchase.Email, purchase.Name, purchase.Phone, purchase.PurchaseId, purchase.DeliveryAdress, purchase.DeliveryType, purchase.PaymentType, emailProducts, cdekOrderNumber);

                            await File.AppendAllLinesAsync(errorsRoot, new[] { $"{DateTime.UtcNow} info:\tpurchase " + purchase.Id + " notification to customer sent" });

                            await _emailService.SendPurchaseNotificationToAdministration(purchase.Email, purchase.Name, purchase.Phone, purchase.PurchaseId, purchase.DeliveryAdress, purchase.DeliveryType, purchase.PaymentType, emailProducts, cdekOrderNumber);

                            await File.AppendAllLinesAsync(errorsRoot, new[] { $"{DateTime.UtcNow} info:\tpurchase " + purchase.Id + " complete, notification to admin sent" });

                            await _cartsRepository.ClearCartProductsAsync(purchase.UserId);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                await File.AppendAllLinesAsync(errorsRoot, new[] { $"{DateTime.UtcNow} error:\t" + ex.Message + $"\n\t\t{ex.StackTrace}" });
            }
        }
    }
}
