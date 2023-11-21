using System.Net;
using System.Net.Mail;

namespace Cheshan.Collection.Shop.Core.Services
{
    public class EmailService
    {
        private readonly string _email = "collection.chel@gmail.com";
        private readonly string _emailPassword = "1234567";

        private readonly string CustomerEmailSubject = "Благодарим за покупку!";

        public async Task SendPurchaseNotificationToCustomer(string customerEmail, string customerName, string purchaseId, string info)
        {
            var fromAdress = new MailAddress(_email, "Collectionchel Shop");

            var customerAdress = new MailAddress(customerEmail, customerName);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAdress.Address, _emailPassword)
            };

            using (var message = new MailMessage(fromAdress, customerAdress)
            {
                Subject = CustomerEmailSubject,
                Body = "Благодарим за покупку, номер Вашего заказа: " + purchaseId
            })
            {
                await smtp.SendMailAsync(message);
            }

        }
    }
}
