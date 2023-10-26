namespace Cheshan.Collection.Shop.Database.Entities
{
    public class ProductInCartEntity
    {

        public CartEntity Cart { get; set; }
        public ProductEntity Product { get; set; }

        public string Size { get; set; }
    }
}
