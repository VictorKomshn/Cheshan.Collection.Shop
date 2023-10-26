namespace Cheshan.Collection.Shop.Database.Entities
{
    public class PurchaseEntity
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string SecondName { get; set; }

        public string Email { get; set; }

        public string? Phone { get; set; }

        public string DeliveryType { get; set; }

        public string DeliveryAdress { get; set; }

        public string PaymentType { get; set; }

        public string? Promocode { get; set; }

        public double PriceForSP1 { get; set; }

        public double PriceForSP2 { get; set; }

        public bool IsComplited { get; set; }

        public virtual ICollection<PaymentLinkEntity> PaymentLinksWithPurchase { get; set; }
    }
}
