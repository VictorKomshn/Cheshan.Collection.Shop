using Cheshan.Collection.Shop.Database.Entities.Enums;

namespace Cheshan.Collection.Shop.Database.Entities
{
    public class ProductEntity
    {
        public Guid Id { get; set; }

        public string SP { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public virtual ICollection<string>? Details { get; set; }

        public string? ModelParameters { get; set; }

        public string Material { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string SKU { get; set; }

        public double? SalePrice { get; set; }

        public int? SalePercent { get; set; }

        public virtual ICollection<SizeWithAmountEntity> SizesWithAmounts { get; set; }

        public string MainPhoto { get; set; }

        public virtual ICollection<string> Photos { get; set; }

        public string Category { get; set; }

        public CategoryType CategoryType { get; set; }

        public bool? IsMan { get; set; }

        public DateTime DateAdded { get; set; }

        public string SEO { get; set; }
    }
}
