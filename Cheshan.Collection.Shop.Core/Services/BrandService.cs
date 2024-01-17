using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.Core.Mappers;
using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.Database.Abstract;

namespace Cheshan.Collection.Shop.Core.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
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

        public async Task<IEnumerable<BrandModel>> GetAllAsync()
        {
            try
            {
                var brandEntities = await _brandRepository.GetAllBrands();
                return brandEntities.Select(x => x.ToModel());
            }
            catch
            {
                throw;
            }
        }
    }
}
