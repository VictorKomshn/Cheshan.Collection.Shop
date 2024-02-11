using Cheshan.Collection.Shop.Core.Models;

namespace Cheshan.Collection.Shop.Core.Abstract
{
    public interface IBrandService
    {
        Task<BrandModel?> GetAsync(string name);

        ICollection<BrandModel> GetAll();

    }
}
