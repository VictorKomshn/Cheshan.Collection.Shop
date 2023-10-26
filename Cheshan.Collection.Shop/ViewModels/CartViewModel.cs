using Cheshan.Collection.Shop.Core.Models;

namespace Cheshan.Collection.Shop.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<SizeWithAmountModel> Products { get; set; }

        public int SumPrice
        {
            get
            {
                var sumPrice = 0;
                foreach (var product in Products.Select(x => x.Product))
                {
                    if (product.SalePrice.HasValue && product.SalePrice != 0 && product.SalePrice < product.Price)
                    {
                        sumPrice += product.SalePrice.Value;
                    }
                    else
                    {
                        sumPrice += product.Price;
                    }
                }
                return sumPrice;
            }
        }
    }
}
