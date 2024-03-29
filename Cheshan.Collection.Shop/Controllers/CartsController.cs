﻿using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Cheshan.Collection.Shop.Controllers
{
    [Route("cart")]
    [ApiController]
    public class CartsController : Controller
    {
        private readonly ICartsService _cartsService;
        private readonly IBrandService _brands;
        public CartsController(ICartsService service, IBrandService brands)
        {
            _cartsService = service ?? throw new ArgumentNullException(nameof(service));
            _brands = brands;
        }


        public async Task<IActionResult> Index()
        {
            try
            {
                var activeuser = Guid.Parse(Request.Cookies["ActiveUser"]);
                var cart = await _cartsService.GetAsync(activeuser);
                var viewModel = new CartViewModel(cart?.Products);
                viewModel.AllBrands = _brands.GetAll();
                return View(viewModel);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        /// <summary>
        /// Добавить продукт в корзину
        /// </summary>
        /// <param name="productId"> Id продукта</param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add/{productId}")]
        public async Task<int> AddToCartAsync(Guid productId, [FromBody] string size)
        {
            try
            {
                var activeUser = Guid.Parse(Request.Cookies["ActiveUser"]);
                if (size.Contains('-'))
                {
                    size = size.Replace('-', ',');
                }
                await _cartsService.AddToCartAsync(productId, size, activeUser);

                return await GetAmountAsync();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        [HttpPost]
        [Route("remove/{productId}")]
        public async Task<int> RemoveFromCartAsync(Guid productId, [FromBody] string size)
        {
            try
            {
                var activeUser = Guid.Parse(Request.Cookies["ActiveUser"]);
                await _cartsService.RemoveProductFromCartAsync(productId, size, activeUser);
                return await GetAmountAsync();

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        [HttpPost]
        [Route("remove-single/{productId}")]
        public async Task<int> RemoveSingleFromCartAsync(Guid productId, [FromBody] string size)
        {
            try
            {
                var activeUser = Guid.Parse(Request.Cookies["ActiveUser"]);
                await _cartsService.RemoveProductOneSizeFromCartAsync(productId, size, activeUser);
                return await GetAmountAsync();

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        [HttpPost]
        [Route("remove")]
        public async Task<int> RemoveAllFromCartAsync()
        {
            try
            {
                var activeUser = Guid.Parse(Request.Cookies["ActiveUser"]);
                await _cartsService.RemoveAllFromCartAsync(activeUser);
                return await GetAmountAsync();

            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        [HttpPost]
        [Route("pay")]
        public async Task ProceedToPayment()
        {
            try
            {
                var activeUser = Guid.Parse(Request.Cookies["ActiveUser"]);
                var cart = await _cartsService.GetAsync(activeUser);
            }
            catch (Exception ex)
            {
            }
        }


        [HttpGet]
        [Route("products")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var activeuser = Guid.Parse(Request.Cookies["ActiveUser"]);
                var cart = await _cartsService.GetAsync(activeuser);
                return PartialView("ProductInCartMenu", cart?.Products);
            }
            catch (Exception ex)
            {
                await System.IO.File.AppendAllLinesAsync(@"../Cheshan.Collection.Shop/cartsControllerErrors.txt", new[] { $"{DateTime.UtcNow} error:\t" + ex.Message });
                if (ex is ArgumentException)
                {
                    return View("NotFound");

                }
                else
                {
                    return View("ServerError");
                }
            }
        }

        [HttpGet]
        [Route("count")]
        public async Task<int> GetAmountAsync()
        {
            try
            {
                var activeuser = Guid.Parse(Request.Cookies["ActiveUser"]);
                var cart = await _cartsService.GetAsync(activeuser);
                int productsAmount = 0;
                foreach (var productAmount in cart.Products.Select(x => x.Amount))
                {
                    productsAmount += productAmount;
                }
                return productsAmount;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
