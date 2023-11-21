using Cheshan.Collection.Shop.Database.Abstract;
using Cheshan.Collection.Shop.Database.Database;
using Cheshan.Collection.Shop.Database.Options;
using Cheshan.Collection.Shop.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Cheshan.Collection.Shop.Database
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>();
            services.AddSingleton(provider =>
            {
                var postgresOptions = new PostgresOptions();
                configuration.Bind(PostgresOptions.SectionName, postgresOptions);
                var connectionBuilder = new NpgsqlConnectionStringBuilder
                {
                    Host = postgresOptions.Host,
                    Port = postgresOptions.Port,
                    Username = postgresOptions.Username,
                    Password = postgresOptions.Password,
                    Database = postgresOptions.Database,

                };

                var builder = new DbContextOptionsBuilder<DataContext>()
                    //.UseNpgsql(connectionBuilder.ToString()).EnableSensitiveDataLogging();
                    .UseSqlServer(connectionBuilder.ToString()).EnableSensitiveDataLogging();

                return builder.Options;
            });


            services.AddTransient<ICartsRepository, CartsRepository>();
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<INotificationRecieversRepository, NotificationRecieversRepository>();
            services.AddScoped<IPurchasesRepository, PurchasesRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();


            var provider = services.BuildServiceProvider();
            var postgresOptions = provider.GetRequiredService<IOptions<PostgresOptions>>().Value;



            return services;
        }

    }
}
