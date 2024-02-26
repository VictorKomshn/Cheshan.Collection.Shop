using Cheshan.Collection.Shop.Core.Models;

namespace Cheshan.Collection.Shop.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public IEnumerable<ProductModel> SeasonalItems {get;set;}
    }
}
