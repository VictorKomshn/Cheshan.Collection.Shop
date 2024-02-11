using Cheshan.Collection.Shop.Core.Models;

namespace Cheshan.Collection.Shop.ViewModels
{
    public class BaseViewModel
    {
        public ICollection<BrandModel> AllBrands { get; set; }
    }
}
