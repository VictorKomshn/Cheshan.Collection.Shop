using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cheshan.Collection.Shop.Controllers
{
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductsService _service;

        public ProductsController(IProductsService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<IActionResult> GetByConditionAsync(ProductsCondition condition)
        {
            try
            {
                var products = await _service.GetByConditionAsync(condition);
                return Ok(products);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(Guid productId)
        {
            try
            {
                var products = await _service.GetAsync(productId);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
