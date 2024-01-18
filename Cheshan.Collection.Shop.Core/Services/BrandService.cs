using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.Core.Mappers;
using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.Database.Abstract;

namespace Cheshan.Collection.Shop.Core.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        private readonly IBrandsBackgroundService _brandsBackgroundService;

        public BrandService(IBrandRepository brandRepository, IBrandsBackgroundService brandsBackgroundService)
        {
            _brandRepository = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
            _brandsBackgroundService = brandsBackgroundService;
        }

        public async Task<BrandModel?> GetAsync(string name)
        {
            try
            {
                var brand = await _brandRepository.GetAsync(name);

                return brand?.ToModel();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<BrandModel> GetAll()
        {
            try
            {
                return _brandsBackgroundService.GetBrands();
            }
            catch
            {
                throw;
            }
        }
    }
}
