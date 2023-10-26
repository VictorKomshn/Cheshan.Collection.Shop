namespace Cheshan.Collection.Shop.Database.Entities
{
    public class ProductEntity
    {
        public Guid Id { get; set; }

        public string SP { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public string Material { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public string SKU { get; set; }

        public int? SalePrice { get; set; }

        public int? SalePercent { get; set; }

        public virtual ICollection<SizeWithAmountEntity> SizesWithAmounts { get; set; }

        public string MainPhoto { get; set; }

        public string[] Photos { get; set; }

        public string Category { get; set; }

        public bool? IsMen { get; set; }

        public string[] Colours { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
