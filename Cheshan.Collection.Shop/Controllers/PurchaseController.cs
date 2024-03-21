using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Cheshan.Collection.Shop.Controllers
{
    [Route("purchase")]
    [ApiController]
    public class PurchaseController : Controller
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IBrandService _brands;

        public PurchaseController(IPurchaseService purchaseService, IBrandService brands)
        {
            _purchaseService = purchaseService ?? throw new ArgumentNullException(nameof(purchaseService));
            _brands = brands ?? throw new ArgumentNullException(nameof(brands));
        }

        [HttpPost]
        public async Task<IActionResult> PurchaseProducts([FromForm] PurchaseModel purchase)
        {
            var activeUser = Guid.Parse(Request.Cookies["ActiveUser"]);
            var redirectUri = string.Empty;
            if (purchase.PaymentType == "cash")
            {
                redirectUri = await _purchaseService.SaveCashPurchase(purchase, activeUser);
            }
            else
            {
                redirectUri = await _purchaseService.CreatePurhcaseAsync(purchase, activeUser);
            }

            return Redirect(redirectUri);
        }


        /// <summary>
        /// Id линка оплаты, сформированной в CreatePurchase
        /// </summary>
        /// <param name="purchaseId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("complete")]
        public async Task<IActionResult> CompletePayment(string orderId, string? lang)
        {
            var purchaseGuid = Guid.Parse(orderId);
            var link = await _purchaseService.CompletePurchaseAsync(purchaseGuid);
            return Redirect(link);
        }

        [HttpGet]
        [Route("success")]
        [Route("success/{id}")]
        public async Task<IActionResult> Success(string id)
        {
            var activeUser = Guid.Parse(Request.Cookies["ActiveUser"]);

            var viewModel = new SuccessViewModel()
            {
                PurchaseId = id,
                AllBrands = _brands.GetAll()
            };
            return View("Success", viewModel);
        }

    }
}