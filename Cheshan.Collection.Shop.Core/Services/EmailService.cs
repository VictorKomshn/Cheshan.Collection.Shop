using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.Core.EmailCompositions.ViewModels;
using Cheshan.Collection.Shop.Core.Extensions;
using Cheshan.Collection.Shop.Database.Entities;
using Cheshan.Collection.Shop.Database.Extensions;
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


        private readonly IProductsService _productsService;

        public EmailService()
        {
        }

        public async Task SendPurchaseNotificationToCustomer(string customerEmail, string customerName, string? customerPhone, string purchaseId, string adress, string deliveryType, string paymentType, IEnumerable<EmailProductModel> products)
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
                Products = products
            };

            string template = File.ReadAllText(@"../Cheshan.Collection.Shop.Core/EmailCompositions/CustomerMessage.html");

            string productTemplate = File.ReadAllText(@"../Cheshan.Collection.Shop.Core/EmailCompositions/PurchasedProductElement.html");
            string purchasedProducts = string.Empty;

            double summaryPrice = 0;
            double discountPrice = 0;
            double finalPrice = 0;

            string priceTemplate = string.Empty;
            string priceHtml = string.Empty;

            foreach (var product in model.Products)
            {
                if (product.SalePrice != null)
                {
                    priceTemplate = File.ReadAllText(@"../Cheshan.Collection.Shop.Core/EmailCompositions/PriceWithDiscount.html");
                    var priceReplacements = new Dictionary<string, string>
                    {
                        {"{price}", product.Price.ToString() },
                        {"{salePrice}", product.SalePrice.ToString() }
                    };

                    priceHtml = priceTemplate.ReplaceAll(priceReplacements);

                    discountPrice += product.Price - product.SalePrice.Value;
                }
                else
                {
                    priceTemplate = File.ReadAllText(@"../Cheshan.Collection.Shop.Core/EmailCompositions/Price.html");
                    var priceReplacements = new Dictionary<string, string>
                    {
                        {"{price}", product.Price.ToString() },
                    };

                    priceHtml = priceTemplate.ReplaceAll(priceReplacements);
                }


                var replacements = new Dictionary<string, string>
                {
                    { "{sku}", product.SKU},
                    { "{price}", priceHtml },
                    { "{size}", product.Size },
                    { "{productPhoto}", "https://collectionchel.ru/img/" + product.Photo },
                    {"{categoryName}",product.Name },
                    {"{amo}",product.Amount > 1 ? "x"+ product.Amount.ToString():string.Empty },
                    { "{brandName}",product.Brand }
                };

                var productHtml = productTemplate.ReplaceAll(replacements) + '\n';
                purchasedProducts += productHtml;
                summaryPrice += product.Price;
            }

            finalPrice = summaryPrice - discountPrice;

            var bodyReplacements = new Dictionary<string, string>
                {
                    { "{deliveryType}", model.DeliveryType == "selfpickup"? "самовывоз" : "курьер"},
                    { "{deliveryAdress}", model.Adress },
                    { "{paymentType}", model.PaymentType == "cash"? "при получении" : "онлайн на сайте"},
                    { "{userName}", model.UserName},
                    { "{phone}", model.Phone},
                    { "{purchasedProducts}", purchasedProducts},
                    { "{purchaseId}", purchaseId },
                    { "{purchaseSummaryPrice}",summaryPrice.ToString() },
                    { "{discountPrice}",discountPrice.ToString() },
                    { "{finalPrice}",finalPrice.ToString() },

            };

            var mailBody = template.ReplaceAll(bodyReplacements);
            await SendMail(mailBody, customerEmail, customerName);
        }


        public async Task SendPurchaseNotificationToAdministration(string customerEmail, string customerName, string? customerPhone, string purchaseId, string adress, string deliveryType, string paymentType, IEnumerable<EmailProductModel> products)
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
                Products = products
            };

            string template = File.ReadAllText(@"../Cheshan.Collection.Shop.Core/EmailCompositions/AdminMessage.html");

            string productTemplate = File.ReadAllText(@"../Cheshan.Collection.Shop.Core/EmailCompositions/PurchasedProductElement.html");
            string purchasedProducts = string.Empty;

            double summaryPrice = 0;
            double discountPrice = 0;
            double finalPrice = 0;

            string priceTemplate = string.Empty;
            string priceHtml = string.Empty;

            foreach (var product in model.Products)
            {
                if (product.SalePrice != null)
                {
                    priceTemplate = File.ReadAllText(@"../Cheshan.Collection.Shop.Core/EmailCompositions/PriceWithDiscount.html");
                    var priceReplacements = new Dictionary<string, string>
                    {
                        {"{price}", product.Price.ToString() },
                        {"{salePrice}", product.SalePrice.ToString() }
                    };

                    priceHtml = priceTemplate.ReplaceAll(priceReplacements);

                    discountPrice += product.Price - product.SalePrice.Value;
                }
                else
                {
                    priceTemplate = File.ReadAllText(@"../Cheshan.Collection.Shop.Core/EmailCompositions/Price.html");
                    var priceReplacements = new Dictionary<string, string>
                    {
                        {"{price}", product.Price.ToString() },
                    };

                    priceHtml = priceTemplate.ReplaceAll(priceReplacements);
                }


                var replacements = new Dictionary<string, string>
                {
                    { "{sku}", product.SKU},
                    { "{price}", priceHtml },
                    { "{size}", product.Size },
                    { "{productPhoto}", "https://collectionchel.ru/img/" + product.Photo },
                    {"{categoryName}",product.Name },
                    {"{amo}",product.Amount > 1 ? "x"+ product.Amount.ToString():string.Empty },
                    { "{brandName}",product.Brand }
                };

                var productHtml = productTemplate.ReplaceAll(replacements) + '\n';
                purchasedProducts += productHtml;
                summaryPrice += product.Price;
            }

            finalPrice = summaryPrice - discountPrice;

            var bodyReplacements = new Dictionary<string, string>
                {
                    { "{deliveryType}", model.DeliveryType == "selfpickup"? "самовывоз" : "курьер"},
                    { "{deliveryAdress}", model.Adress },
                    { "{paymentType}", model.PaymentType == "cash"? "при получении" : "онлайн на сайте"},
                    { "{userName}", model.UserName},
                    { "{phone}", model.Phone},
                    { "{email}", model.Email},
                    { "{purchasedProducts}", purchasedProducts},
                    { "{purchaseId}", purchaseId },
                    { "{purchaseSummaryPrice}",summaryPrice.ToString() },
                    { "{discountPrice}",discountPrice.ToString() },
                    { "{finalPrice}",finalPrice.ToString() },

            };

            var mailBody = template.ReplaceAll(bodyReplacements);
            await SendMail(mailBody, customerEmail, customerName);

            //foreach (var recepients in adminRecepientAdresses)
            //{
            //    await SendMail(mailBody, recepients, "Collectionchel");
            //}
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
