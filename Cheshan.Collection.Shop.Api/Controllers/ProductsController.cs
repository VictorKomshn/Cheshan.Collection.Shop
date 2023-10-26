//using Cheshan.Collection.Shop.Api.ViewModels;
//using Cheshan.Collection.Shop.Core.Abstract;
//using Cheshan.Collection.Shop.Core.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace Cheshan.Collection.Shop.Controllers
//{
//    [ApiController]
//    [Route("products")]
//    public class ProductsController : ControllerBase
//    {

//        private readonly IProductsService _service;

//        public ProductsController(IProductsService service)
//        {
//            _service = service ?? throw new ArgumentNullException(nameof(service));
//        }

//        [HttpPost]
//        [Route("")]
//        [Route("filter")]
//        public async Task<IActionResult> GetByConditionAsync(ProductsCondition? condition)
//        {
//            try
//            {
//                if (condition == null)
//                {
//                    condition = new ProductsCondition();
//                    condition.StartIndex = 0;
//                }
//                var products = await _service.GetByConditionAsync(condition);
//                var viewModel = new ProductsViewModel
//                {
//                    Products = products
//                };
//                return Ok(viewModel);
//            }
//            catch (Exception ex)
//            {
//                return NotFound();
//            }
//        }

//        [HttpGet]
//        [Route("{id}")]
//        public async Task<IActionResult> GetAsync(Guid id)
//        {
//            try
//            {
//                var products = await _service.GetAsync(id);
//                return Ok(products);
//            }
//            catch (Exception ex)
//            {
//                return NotFound();
//            }
//        }

//        [HttpPost]
//        [Route("create")]
//        public async Task<IActionResult> CreateAsync(ProductModel model)
//        {
//            var model1 = new ProductModel
//            {
//                Brand = "Gucci",
//                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam et mi facilisis, sollicitudin sem sollicitudin, accumsan ex. Nullam eros nulla, finibus id tellus vel, interdum maximus magna.",
//                Id = Guid.NewGuid(),
//                MainPhoto = "photo1-main.png",
//                Material = "кожа 100%",
//                Name = "Akihabara pants",
//                Photos = new[] { "photo1-second.png", "photo1-thrird.png", "photo1-forth.png", "photo1-fifth.png" },
//                Price = 60000,
//                SalePercent = 0,
//                SalePrice = 0,
//                SizesWithAmounts = new List<(string, int)>
//                {
//                    ("S", 10),
//                    ("M", 10),
//                    ("L", 10),
//                },
//                SKU = Guid.NewGuid().ToString(),
//            };

//            var model2 = new ProductModel
//            {
//                Brand = "Louis Vuitton",
//                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam et mi facilisis, sollicitudin sem sollicitudin, accumsan ex. Nullam eros nulla, finibus id tellus vel, interdum maximus magna.",
//                Id = Guid.NewGuid(),
//                MainPhoto = "photo2-main.png",
//                Material = "кожа 100%",
//                Name = "Krytble pants",
//                Photos = new[] { "photo2-second.png", "photo2-thrird.png", "photo2-forth.png", "photo2-fifth.png" },
//                Price = 100000,
//                SalePercent = 50,
//                SalePrice = 50000,
//                SizesWithAmounts = new List<(string, int)>
//                {
//                    ("S", 10),
//                    ("M", 0),
//                    ("L", 100)  ,
//                },
//                SKU = Guid.NewGuid().ToString(),
//            };
//            var newModelGuid = await _service.CreateAsync(model);
//            return Ok(newModelGuid);
//        }


//        [HttpDelete]
//        [Route("{id}")]
//        public async Task<IActionResult> DeleteAsync(Guid id)
//        {
//            try
//            {
//                await _service.DeleteAsync(id);
//                return Ok();
//            }
//            catch (Exception ex)
//            {
//                return NotFound();
//            }
//        }

//        [HttpPut]
//        [Route("{id}")]
//        public async Task<IActionResult> UpdateAsync(Guid id, ProductModel updatedMode)
//        {
//            try
//            {
//                var result = await _service.UpdateAsync(id, updatedMode);
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                return NotFound();
//            }
//        }

//    }
//}
