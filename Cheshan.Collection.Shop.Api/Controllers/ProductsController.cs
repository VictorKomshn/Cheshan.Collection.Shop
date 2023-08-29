using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cheshan.Collection.Shop.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {

        private readonly IProductsService _service;

        public ProductsController(IProductsService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpPost]
        [Route("filter")]
        public async Task<IActionResult> GetByConditionAsync(ProductsCondition condition)
        {
            try
            {
                var products = await _service.GetByConditionAsync(condition);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            try
            {
                var products = await _service.GetAsync(id);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync(ProductModel model)
        {
            var newModelGuid = await _service.CreateAsync(model);
            return Ok(newModelGuid);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id,ProductModel updatedMode)
        {
            try
            {
                var result = await _service.UpdateAsync(id, updatedMode);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }

    }
}
