using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cheshan.Collection.Shop.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICartsService, CartsService>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<INotificationRecieversService, NotificationRecieversService>();

            return services;
        }
    }
}
