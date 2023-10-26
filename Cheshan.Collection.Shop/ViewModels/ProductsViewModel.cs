using Cheshan.Collection.Shop.Core.Models;

namespace Cheshan.Collection.Shop.ViewModels
{
    public class ProductsViewModel
    {
        public BrandModel? Brand { get; set; }
        public ICollection<ProductModel> Products { get; set; }

        /// <summary>
        /// Количество всех товаров заданного фильтра
        /// </summary>
        public int MaxAmount { get; set; }
    }
}
