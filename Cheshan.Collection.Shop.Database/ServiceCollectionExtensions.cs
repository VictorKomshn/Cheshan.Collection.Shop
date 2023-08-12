using Cheshan.Collection.Shop.Database.Abstract;
using Cheshan.Collection.Shop.Database.Options;
using Cheshan.Collection.Shop.Database.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Cheshan.Collection.Shop.Database
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {


            services.AddScoped<ICartsRepository, CartsRepository>();
            services.AddScoped<IProductsRepository, ProductsRepository>();
            return services;
        }

        private static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<IValidateOptions<PostgresOptions>, PostgresOptionsValidator>();
            services.Configure<PostgresOptions>(config.GetSection())
        }
    }
}
