using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Cheshan.Collection.Shop.Controllers
{
    [ApiController]
    [Route("brands")]
    public class BrandsController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService ?? throw new ArgumentNullException(nameof(brandService));
        }

        [HttpGet]
        [Route("{forFilter}")]
        public async Task<IActionResult> Index(bool? forFilter)
        {
            try
            {
                var brands = _brandService.GetAll();
                var viewModel = new BrandsViewModel
                {
                    Brands = brands,
                    IsForFilter = forFilter ?? false
                };

                return PartialView("BrandButtons", viewModel);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
