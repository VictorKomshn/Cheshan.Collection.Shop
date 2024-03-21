namespace Cheshan.Collection.Shop.Core.Models
{
    public class PurchaseModel
    {
        public string Name { get; set; }

        public string SecondName { get; set; }

        public string Email { get; set; }

        public string? Phone { get; set; }

        public string DeliveryType { get; set; }

        public string DeliveryAdress { get; set; }

        public string PaymentType { get; set; }

        public string? Promocode { get; set; }

        public bool IsComplited { get; set; }

        public string? CDEKItemId { get; set; }
    }
}
