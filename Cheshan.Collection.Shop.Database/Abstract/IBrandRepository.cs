using Cheshan.Collection.Shop.Database.Entities;

namespace Cheshan.Collection.Shop.Database.Abstract
{
    public interface IBrandRepository
    {
        Task<BrandEntity?> GetAsync(string name);

        Task<IEnumerable<BrandEntity>> GetAllBrands();
    }
}
