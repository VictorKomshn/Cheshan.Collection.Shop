using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.Core.Mappers;
using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.Database.Abstract;
using Cheshan.Collection.Shop.Database.Database;
using Cheshan.Collection.Shop.Database.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cheshan.Collection.Shop.Core.Services
{
    public class BrandsBackgroundService : BackgroundService, IBrandsBackgroundService
    {

        private readonly IBrandRepository _brandRepository;

        private IEnumerable<BrandModel> _existingBrands;

        private readonly TimeSpan _threshold;
        public BrandsBackgroundService(IServiceProvider services)
        {
            var scope = services.CreateScope();

            _brandRepository = new BrandRepository(scope.ServiceProvider.GetService<DataContext>());

            _existingBrands = Array.Empty<BrandModel>();

            _threshold = TimeSpan.FromHours(1);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var brands = await _brandRepository.GetAllBrandsAsync();

                if (brands != null)
                {
                    _existingBrands = brands.Select(x => x.ToModel());
                }

                await Task.Delay(_threshold, stoppingToken);
            }
        }


        public IEnumerable<BrandModel> GetBrands() => _existingBrands;
    }
}
