//using Cheshan.Collection.Shop.Core.Abstract;
//using Microsoft.AspNetCore.Mvc;

//namespace Cheshan.Collection.Shop.Api.Controllers
//{
//    [Route("home")]
//    [ApiController]
//    public class HomeController : Controller
//    {
//        private readonly IProductsService _service;

//        public HomeController(IProductsService service)
//        {
//            _service = service ?? throw new ArgumentNullException(nameof(service));
//        }

//        [HttpGet]
//        public async Task<IActionResult> Index()
//        {
//            return View();
//        }
//    }
//}
