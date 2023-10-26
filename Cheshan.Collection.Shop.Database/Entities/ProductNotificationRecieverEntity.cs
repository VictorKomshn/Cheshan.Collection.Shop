namespace Cheshan.Collection.Shop.Database.Entities
{
    public class ProductNotificationRecieverEntity
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public Guid ProductId { get; set; }

        public string Size { get; set; }
    }
}
