using Cheshan.Collection.Shop.Core.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Cheshan.Collection.Shop.Controllers
{
    [Route("")]
    [Route("home")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IProductsService _service;
        private readonly ICartsService _carts;

        public HomeController(IProductsService service, ICartsService carts)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _carts = carts ?? throw new ArgumentNullException(nameof(carts));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var activeuser = Request.Cookies["ActiveUser"];
            if (activeuser == null)
            {

                var newUserGuid = Guid.NewGuid();
                Response.Cookies.Append("ActiveUser", newUserGuid.ToString());
                await _carts.CreateCartAsync(newUserGuid);
            }

            return View();
        }
    }
}
