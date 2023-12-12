using Cheshan.Collection.Shop.Database.Abstract;
using Cheshan.Collection.Shop.Database.Database;
using Cheshan.Collection.Shop.Database.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cheshan.Collection.Shop.Core.Services
{
    public class ClearCartsBackgroundService : BackgroundService
    {

        private readonly ICartsRepository _repository;

        private readonly DateTime lastUpdate;

        private readonly TimeSpan _threshold = TimeSpan.FromDays(1);

        public ClearCartsBackgroundService(IServiceProvider services)
        {
            var scope = services.CreateScope();
            _repository = new CartsRepository(scope.ServiceProvider.GetService<DataContext>());

            lastUpdate = DateTime.UtcNow;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(_threshold);
                if ((lastUpdate - DateTime.UtcNow) > TimeSpan.FromDays(1))
                {
                    await _repository.ClearOldCarts();
                }
            }
        }
    }
}
