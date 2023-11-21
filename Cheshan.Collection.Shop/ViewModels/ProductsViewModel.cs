using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.Database.Entities.Enums;

namespace Cheshan.Collection.Shop.ViewModels
{
    public class ProductsViewModel
    {
        public BrandModel? Brand { get; set; }

        public CategoryType CategoryType { get; set; }

        public ICollection<ProductModel> Products { get; set; }

        /// <summary>
        /// Количество всех товаров заданного фильтра
        /// </summary>
        public int MaxAmount { get; set; }
    }
}
