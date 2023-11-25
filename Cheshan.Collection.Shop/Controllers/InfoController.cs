using Cheshan.Collection.Shop.Core.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Cheshan.Collection.Shop.Controllers
{
    [Route("info")]
    [ApiController]
    public class InfoController : Controller
    {
        private readonly ICartsService _cartsService;

        public InfoController(ICartsService cartsService)
        {
            _cartsService = cartsService ?? throw new ArgumentNullException(nameof(cartsService));
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
            return View();
        }

        [HttpGet]
        [Route("delivery-and-payment")]
        public IActionResult DeliveryAndPayment()
        {
            return View();
        }

        [HttpGet]
        [Route("refund")]
        public IActionResult Refund()
        {
            return View();
        }

        [HttpGet]
        [Route("priveleges")]
        public IActionResult Priveleges()
        {
            return View();
        }

        //[HttpGet]
        //[Route("sizes")]
        //public IActionResult Sizes()
        //{
        //    return View();
        //}
    }
}
