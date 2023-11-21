namespace Cheshan.Collection.Shop.Database.Entities
{
    public class CartEntity
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public virtual ICollection<SizeWithAmountEntity> Products { get; set; }

        public DateTime Created { get; set; }
    }
}
