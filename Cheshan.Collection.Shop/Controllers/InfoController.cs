using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Cheshan.Collection.Shop.Controllers
{
    [Route("info")]
    [ApiController]
    public class InfoController : Controller
    {
        private readonly ICartsService _cartsService;
        private readonly IBrandService _brands;

        public InfoController(ICartsService cartsService, IBrandService brands)
        {
            _cartsService = cartsService ?? throw new ArgumentNullException(nameof(cartsService));
            _brands = brands;
        }

        [HttpGet]
        [Route("us")]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var activeuser = Request.Cookies["ActiveUser"];
            if (activeuser == null)
            {
                var newUserGuid = Guid.NewGuid();
                Response.Cookies.Append("ActiveUser", newUserGuid.ToString());
                await _cartsService.CreateCartAsync(newUserGuid);
            }
            var vm = new BaseViewModel()
            {
                AllBrands = _brands.GetAll()
            };
            return View(vm);
        }

        [HttpGet]
        [Route("delivery-and-payment")]
        public IActionResult DeliveryAndPayment()
        {
            var vm = new BaseViewModel()
            {
                AllBrands = _brands.GetAll()
            };
            return View(vm);
        }

        [HttpGet]
        [Route("refund")]
        public IActionResult Refund()
        {
            var vm = new BaseViewModel()
            {
                AllBrands = _brands.GetAll()
            };
            return View(vm);
        }

        [HttpGet]
        [Route("priveleges")]
        public IActionResult Priveleges()
        {
            var vm = new BaseViewModel()
            {
                AllBrands = _brands.GetAll()
            };
            return View(vm);
        }

        //[HttpGet]
        //[Route("sizes")]
        //public IActionResult Sizes()
        //{
        //    return View();
        //}
    }
}
