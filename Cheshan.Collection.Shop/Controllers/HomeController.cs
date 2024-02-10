using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.Core.Services;
using Cheshan.Collection.Shop.Database.Entities;
using Cheshan.Collection.Shop.ViewModels;
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
        private readonly IBrandService _brands;

        public HomeController(IProductsService service, ICartsService carts, IBrandService brands)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _carts = carts ?? throw new ArgumentNullException(nameof(carts));
            _brands = brands;
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
            var viewModel = new BaseViewModel
            {
                AllBrands = _brands.GetAll()
            };
            return View(viewModel);
        }
    }
}
