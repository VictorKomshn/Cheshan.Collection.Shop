namespace Cheshan.Collection.Shop.Database.Entities
{
    public class PurchasedProductEntity
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string Photo { get; set; }

        public string Name { get; set; }

        public string SKU { get; set; }

        public string Size { get; set; }

        public int Amount { get; set; }

        public double Price { get; set; }
    }
}
