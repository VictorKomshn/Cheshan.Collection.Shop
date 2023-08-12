namespace Cheshan.Collection.Shop.Core.Models
{
    public class CartModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public ICollection<ProductModel> Products { get; set; }

    }
}
