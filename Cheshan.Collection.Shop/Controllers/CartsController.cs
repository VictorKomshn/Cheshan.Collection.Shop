using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Cheshan.Collection.Shop.Controllers
{
    [Route("cart")]
    [ApiController]
    public class CartsController : Controller
    {
        private readonly ICartsService _service;
        private const string apiRoute = "https://alfa.rbsuat.com/payment/rest/register.do?";

        public CartsController(ICartsService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }


        public async Task<IActionResult> Index()
        {
            try
            {
                var activeuser = Guid.Parse(Request.Cookies["ActiveUser"]);
                var cart = await _service.GetAsync(activeuser);
                var viewModel = new CartViewModel
                {
                    Products = cart?.Products,
                };
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
                await _service.AddToCartAsync(productId, size, activeUser);

                //Response.Redirect(Request.GetDisplayUrl());
                //var a = Request.GetDisplayUrl();
                //return Redirect($"~/products"/*/{productId.ToString()}*/);
                return await GetAmountAsync();
            }
            catch (Exception ex)
            {
                return 0; NotFound(ex);
            }
        }

        [HttpPost]
        [Route("remove/{productId}")]
        public async Task<int> RemoveFromCartAsync(Guid productId, [FromBody] string size)
        {
            try
            {
                var activeUser = Guid.Parse(Request.Cookies["ActiveUser"]);
                await _service.RemoveProductFromCartAsync(productId, size, activeUser);
                return await GetAmountAsync();

            }
            catch (Exception ex)
            {
                return 0; NotFound(ex);
            }
        }

        [HttpPost]
        [Route("remove-single/{productId}")]
        public async Task<int> RemoveSingleFromCartAsync(Guid productId, [FromBody] string size)
        {
            try
            {
                var activeUser = Guid.Parse(Request.Cookies["ActiveUser"]);
                await _service.RemoveProductOneSizeFromCartAsync(productId, size, activeUser);
                return await GetAmountAsync();

            }
            catch (Exception ex)
            {
                return 0; NotFound(ex);
            }
        }

        [HttpPost]
        [Route("remove")]
        public async Task<int> RemoveAllFromCartAsync()
        {
            try
            {
                var activeUser = Guid.Parse(Request.Cookies["ActiveUser"]);
                await _service.RemoveAllFromCartAsync(activeUser);
                return await GetAmountAsync();

            }
            catch (Exception ex)
            {
                return 0; NotFound(ex);
            }
        }


        [HttpPost]
        [Route("pay")]
        public async Task ProceedToPayment()
        {
            try
            {
                var activeUser = Guid.Parse(Request.Cookies["ActiveUser"]);
                var cart = await _service.GetAsync(activeUser);


            }
            catch (Exception ex)
            {
                //NotFound(ex);
            }
        }

        //private string BuildPaymentString(CartModel cart)
        //{
        //    var productsForFurstSP = ""
        //    var productsForSecondSP
        //}

        [HttpGet]
        [Route("products")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var activeuser = Guid.Parse(Request.Cookies["ActiveUser"]);
                var cart = await _service.GetAsync(activeuser);
                return PartialView("ProductInCartMenu", cart?.Products);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpGet]
        [Route("count")]
        public async Task<int> GetAmountAsync()
        {
            try
            {
                var activeuser = Guid.Parse(Request.Cookies["ActiveUser"]);
                var cart = await _service.GetAsync(activeuser);
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
