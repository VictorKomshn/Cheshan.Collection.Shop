using Cheshan.Collection.Shop.Core.Models;

namespace Cheshan.Collection.Shop.Core.EmailCompositions.ViewModels
{
    public class CustomerMessageViewModel
    {
        public IEnumerable<EmailProductModel> Products { get; set; }

        public string UserName { get; set; }

        public string DeliveryType { get; set; }

        public string PurchaseId { get; set; }

        public string PaymentType { get; set; }

        public string Adress { get; set; }

        public string Email { get; set; }

        public string? Phone { get; set; }

    }

    public class EmailProductModel
    {
        public string Photo { get; set; }

        public double Price { get; set; }

        public int Amount { get; set; }

        public string SKU { get; set; }

        public string Size { get; set; }
    }
}
