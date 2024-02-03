using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.Core.EmailCompositions.ViewModels;
using Cheshan.Collection.Shop.Core.Extensions;
using Cheshan.Collection.Shop.Database.Entities;
using System.Net;
using System.Net.Mail;

namespace Cheshan.Collection.Shop.Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _email = "collecti0n.info@yandex.ru";
        private readonly string _emailPassword = "hfiiolvarvbkmigr";

        private readonly string[] adminRecepientAdresses = new[]
        {
            "collection.chel@gmail.com",
            "collecti0n.info@yandex.ru"
        };

        private readonly string CustomerEmailSubject = "Благодарим за покупку!";

        public EmailService()
        {
        }

        public async Task SendPurchaseNotificationToCustomer(string customerEmail, string customerName, string? customerPhone, string purchaseId, string adress, string deliveryType, string paymentType, IEnumerable<PurchasedProductEntity> products)
        {

            var model = new CustomerMessageViewModel
            {
                Adress = adress,
                DeliveryType = deliveryType,
                Email = customerEmail,
                UserName = customerName,
                Phone = customerPhone,
                PaymentType = paymentType,
                PurchaseId = purchaseId,
                Products = products.Select(x =>
                {
                    return new EmailProductModel
                    {
                        Photo = x.Photo,
                        Price = x.Price,
                        SKU = x.SKU,
                        Size = x.Size,
                        Amount = x.Amount
                    };
                })
            };

            string template = File.ReadAllText(@".\\EmailCompositions\\CustomerMessage.html");

            string productTemplate = File.ReadAllText(@".\\EmailCompositions\\PurchasedProductElement.html");
            string purchasedProducts = string.Empty;
            foreach (var product in model.Products)
            {
                var replacements = new Dictionary<string, string>
                {
                    { "{sku}", product.SKU},
                    { "{price}", product.Price.ToString() },
                    { "{size}", product.Size },
                    { "{productPhoto}", "https://collectionchel.ru/img/" + product.Photo }
                };

                var productHtml = productTemplate.ReplaceAll(replacements) + '\n';
                purchasedProducts += productHtml;
            }

            var bodyReplacements = new Dictionary<string, string>
                {
                    { "{deliveryType}", model.DeliveryType},
                    { "{deliveryAdress}", model.Adress },
                    { "{paymentType}", model.PaymentType},
                    { "{userName}", model.UserName},
                    { "{phone}", model.Phone},
                    { "{email}", model.Email},
                    { "{purchasedProducts}", purchasedProducts},
                    { "{purchaseId}", purchaseId }
            };

            var mailBody = template.ReplaceAll(bodyReplacements);

            await SendMail(mailBody, customerEmail, customerName);
        }


        public async Task SendPurchaseNotificationToAdministration(string customerEmail, string customerName, string? customerPhone, string purchaseId, string adress, string deliveryType, string paymentType, IEnumerable<PurchasedProductEntity> products)
        {
            var model = new CustomerMessageViewModel
            {
                Adress = adress,
                DeliveryType = deliveryType,
                Email = _email,
                UserName = "Collectionchel",
                Phone = customerPhone,
                PaymentType = paymentType,
                PurchaseId = purchaseId,
                Products = products.Select(x =>
                {
                    return new EmailProductModel
                    {
                        Photo = x.Photo,
                        Price = x.Price,
                        SKU = x.SKU,
                        Size = x.Size,
                        Amount = x.Amount
                    };
                })
            };

            string template = File.ReadAllText("C:\\SCOUT\\work\\Cheshan.Collection.Shop\\Cheshan.Collection.Shop.Core\\EmailCompositions\\AdminMessage.html");

            string productTemplate = File.ReadAllText("C:\\SCOUT\\work\\Cheshan.Collection.Shop\\Cheshan.Collection.Shop.Core\\EmailCompositions\\PurchasedProductElement.html");
            string purchasedProducts = string.Empty;
            foreach (var product in model.Products)
            {
                var replacements = new Dictionary<string, string>
                {
                    { "{sku}", product.SKU},
                    { "{price}", product.Price.ToString() },
                    { "{size}", product.Size },
                    { "{productPhoto}", "https://collectionchel.ru/img/" + product.Photo }
                };

                var productHtml = productTemplate.ReplaceAll(replacements) + '\n';
                purchasedProducts += productHtml;
            }

            var bodyReplacements = new Dictionary<string, string>
                {
                    { "{deliveryType}", model.DeliveryType},
                    { "{deliveryAdress}", model.Adress },
                    { "{paymentType}", model.PaymentType},
                    { "{userName}", model.UserName},
                    { "{phone}", model.Phone},
                    { "{email}", model.Email},
                    { "{purchasedProducts}", purchasedProducts},
                    { "{purchaseId}", purchaseId }

                };

            var mailBody = template.ReplaceAll(bodyReplacements);

            foreach (var recepients in adminRecepientAdresses)
            {
                await SendMail(mailBody, recepients, "Collectionchel");
            }
        }

        private async Task SendMail(string mailBody, string recepientAdress, string recepientName)
        {
            var fromAdress = new MailAddress(_email, "Collection Shop");
            var customerAdress = new MailAddress(recepientAdress, recepientName);

            var smtp = new SmtpClient
            {
                Host = "smtp.yandex.ru",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(_email, _emailPassword)
            };


            using (var message = new MailMessage(fromAdress, customerAdress)
            {
                Subject = CustomerEmailSubject,
                Body = mailBody,
                IsBodyHtml = true,
                BodyEncoding = System.Text.Encoding.UTF8
            })
            {
                await smtp.SendMailAsync(message);

            }
        }
    }
}
