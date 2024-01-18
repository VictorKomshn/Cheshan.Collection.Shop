using Cheshan.Collection.Shop.Core.Models;

namespace Cheshan.Collection.Shop.Core.Abstract
{
    public interface IBrandsBackgroundService
    {
        public IEnumerable<BrandModel> GetBrands();
    }
}
