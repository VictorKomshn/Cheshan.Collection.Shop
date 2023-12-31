﻿using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.Database.Abstract;
using Cheshan.Collection.Shop.Database.Database;
using Cheshan.Collection.Shop.Database.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Cheshan.Collection.Shop.Core.Services
{
    public class PurchaseStatusesBackgroundService : BackgroundService
    {
        private readonly IPurchasesRepository _purchasesRepository;
        private readonly ICartsRepository _cartsRepository;

        private readonly IAlfaBankService _alfaBankService;

        private readonly IEmailService _emailService;

        private readonly TimeSpan _statusThreshold = TimeSpan.FromMilliseconds(5000);

        private readonly TimeSpan _purchaseThreshold = TimeSpan.FromMilliseconds(10000);

        public PurchaseStatusesBackgroundService(IServiceProvider services,
                                                 IEmailService emailService,
                                                 IAlfaBankService alfaBankService)
        {
            var scope = services.CreateScope();

            _purchasesRepository = new PurchasesRepository(scope.ServiceProvider.GetService<DataContext>());
            _cartsRepository = new CartsRepository(scope.ServiceProvider.GetService<DataContext>());


            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _alfaBankService = alfaBankService ?? throw new ArgumentNullException(nameof(alfaBankService)) ;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {

                var stopwatch = Stopwatch.StartNew();
                var recentPurchases = await _purchasesRepository.GetIncompleteRecent();
                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(_statusThreshold);

                    if (stopwatch.Elapsed >= _purchaseThreshold)
                    {
                        recentPurchases = await _purchasesRepository.GetIncompleteRecent();

                        stopwatch.Restart();
                    }

                    foreach (var purchase in recentPurchases)
                    {
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

                            await _purchasesRepository.UpdatePurchaseAsync(purchase);

                            var purchasedProducts = purchase.PurchasedProducts;

                            await _emailService.SendPurchaseNotificationToCustomer(purchase.Email, purchase.Name, purchase.Phone, purchase.PurchaseId, purchase.DeliveryAdress, purchase.DeliveryType, purchase.PaymentType, purchasedProducts);

                            await _emailService.SendPurchaseNotificationToAdministration(purchase.Email, purchase.Name, purchase.Phone, purchase.PurchaseId, purchase.DeliveryAdress, purchase.DeliveryType, purchase.PaymentType, purchasedProducts);

                            await _cartsRepository.ClearCartProductsAsync(purchase.UserId);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                var a = ex.Message;
            }
        }
    }
}
