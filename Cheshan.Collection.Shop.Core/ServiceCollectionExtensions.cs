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
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IHelpService, HelpService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IAlfaBankService, AlfaBankService>();


            services.AddTransient<IPurchaseService, PurchaseService>(x =>
            {
                var carts = x.GetRequiredService<ICartsService>();
                var repository = x.GetRequiredService<IPurchasesRepository>();
                var email = x.GetRequiredService<IEmailService>();
                var alfa = x.GetRequiredService<IAlfaBankService>();
                return new PurchaseService(carts, email, repository, alfa);
            });

            services.AddHostedService<PurchaseStatusesBackgroundService>();

            services.AddHostedService<ClearCartsBackgroundService>();

            return services;
        }
    }
}
