using Cheshan.Collection.Shop.Core.Models;

namespace Cheshan.Collection.Shop.ViewModels
{
    public class CartViewModel:BaseViewModel
    {
        public CartViewModel(IEnumerable<SizeWithAmountModel> products)
        {
            Products = products;

            double sumPrice = 0;
            double priceForSP1 = 0;
            double priceForSP2 = 0;
            foreach (var product in Products)
            {
                if (product.Product.SalePrice.HasValue && product.Product.SalePrice != 0 && product.Product.SalePrice < product.Product.Price)
                {
                    var price = product.Product.SalePrice.Value * product.Amount;
                    sumPrice += price;

                    if (string.Equals(product.Product.SP, SP1))
                    {
                        priceForSP1 += price;
                    }
                    else
                    {
                        priceForSP2 += price;
                    }
                }
                else
                {
                    var price = product.Product.Price * product.Amount;
                    sumPrice += price;

                    if (string.Equals(product.Product.SP, SP1))
                    {
                        priceForSP1 += price;
                    }
                    else
                    {
                        priceForSP2 += price;
                    }
                }
            }

            SumPrice = sumPrice;
            PriceForSP1 = priceForSP1;
            PriceForSP2 = priceForSP2;
        }

        private readonly string SP1 = "745100194346";
        private readonly string SP2 = "745100227337";
        public IEnumerable<SizeWithAmountModel> Products { get; set; }

        public double SumPrice { get; }

        public double PriceForSP1 { get; }

        public double PriceForSP2 { get; }
    }
}
