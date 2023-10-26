namespace Cheshan.Collection.Shop.Database.Entities
{
    public class PaymentLinkEntity
    {
        public Guid Id { get; set; }

        public string PaymentLink { get; set; }

        public bool IsCompleted { get; set; }
    }
}
