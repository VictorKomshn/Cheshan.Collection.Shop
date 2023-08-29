namespace Cheshan.Collection.Shop.Database.Entities
{
    public class CartEntity
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public ICollection<ProductEntity> Products { get; set; }
    }
}
