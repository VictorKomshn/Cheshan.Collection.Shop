using Cheshan.Collection.Shop.Core.Models;

namespace Cheshan.Collection.Shop.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        public ProductModel Product { get; set; }

        public IEnumerable<ProductModel> SuggestedProducts { get; set; }
    }
}
