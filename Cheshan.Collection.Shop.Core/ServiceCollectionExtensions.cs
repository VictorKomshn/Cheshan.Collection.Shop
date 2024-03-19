using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.Core.Services;
using Cheshan.Collection.Shop.Database.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace Cheshan.Collection.Shop.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<ICartsService, CartsService>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<INotificationRecieversService, NotificationRecieversService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IHelpService, HelpService>();
            services.AddTransient<ICDEKService, CDEKService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IAlfaBankService, AlfaBankService>();
            services.AddSingleton<IBrandsBackgroundService, BrandsBackgroundService>();
            services.AddHostedService(x => x.GetRequiredService<IBrandsBackgroundService>() as BrandsBackgroundService);

            services.AddTransient<IBrandService, BrandService>(x =>
            {
                var backgroundService = x.GetRequiredService<IBrandsBackgroundService>();
                var database = x.GetRequiredService<IBrandRepository>();

                return new BrandService(database, backgroundService);
            });


            services.AddTransient<IPurchaseService, PurchaseService>(x =>
            {
                var carts = x.GetRequiredService<ICartsService>();
                var purchasesRepository = x.GetRequiredService<IPurchasesRepository>();
                var porductsRepository = x.GetRequiredService<IProductsRepository>();
                var email = x.GetRequiredService<IEmailService>();
                var alfa = x.GetRequiredService<IAlfaBankService>();
                return new PurchaseService(carts, email, purchasesRepository, porductsRepository, alfa);
            });

            services.AddHostedService<PurchaseStatusesBackgroundService>();

            services.AddHostedService<ClearCartsBackgroundService>();

            return services;
        }
    }
}
