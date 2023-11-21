using Cheshan.Collection.Shop.Database.Entities.Enums;

namespace Cheshan.Collection.Shop.Core.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }

        public string SP { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public IEnumerable<string>? Details { get; set; }

        public string ModelParameters { get; set; }

        public string Material { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string SKU { get; set; }

        public double? SalePrice { get; set; }

        public int? SalePercent { get; set; }

        public List<SizeWithAmountModel> SizesWithAmounts { get; set; }

        public string MainPhoto { get; set; }

        public IEnumerable<string> Photos { get; set; }

        public string Category { get; set; }

        public CategoryType CategoryType { get; set; }

        public bool? IsMan { get; set; }

        public string[] Colours { get; set; }

        public string SEO { get; set; }

        public bool IsAvailable()
        {
            return SizesWithAmounts.Any(x => x.Amount > 0);
        }
    }
}
