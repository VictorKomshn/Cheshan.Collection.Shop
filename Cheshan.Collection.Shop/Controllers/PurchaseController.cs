﻿using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cheshan.Collection.Shop.Controllers
{
    [Route("purchase")]
    [ApiController]
    public class PurchaseController : Controller
    {
        private readonly IPurchaseService _purchaseService;
        private readonly ICartsService _cartsService;

        public PurchaseController(IPurchaseService purchaseService, ICartsService cartsService)
        {
            _purchaseService = purchaseService ?? throw new ArgumentNullException(nameof(purchaseService));
            _cartsService= cartsService?? throw new ArgumentNullException(nameof(cartsService));
        }

        [HttpPost]
        public async Task<IActionResult> PurchaseProducts([FromForm] PurchaseModel purcahse)
        {
            var activeUser = Guid.Parse(Request.Cookies["ActiveUser"]);
            var paymentUri = await _purchaseService.CreatePurhcaseAsync(purcahse, activeUser);
            return Redirect(paymentUri);
        }


        /// <summary>
        /// Id линка оплаты, сформированной в CreatePurchase
        /// </summary>
        /// <param name="purchaseId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("complete")]
        public async Task<IActionResult> CompletePayment( string orderId, string? lang)
        {
            var purchaseGuid = Guid.Parse(orderId);
            var link = await _purchaseService.CompletePurchaseAsync(purchaseGuid);
            return Redirect(link);
        }

        [HttpGet]
        [Route("success")]
        public async Task<IActionResult> Success()
        {
            var activeUser = Guid.Parse(Request.Cookies["ActiveUser"]);
            if(activeUser != null)
            {
                await _cartsService.ClearCartProductsAsync(activeUser);
            }
            return View("Success");
        }

    }
}