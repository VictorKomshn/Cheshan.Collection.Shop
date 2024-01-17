using Cheshan.Collection.Shop.Core.Models;

namespace Cheshan.Collection.Shop.ViewModels
{
    public class BrandsViewModel
    {
        public IEnumerable<BrandModel> Brands { get; set; }

        public bool IsForFilter { get; set; }
    }
}
