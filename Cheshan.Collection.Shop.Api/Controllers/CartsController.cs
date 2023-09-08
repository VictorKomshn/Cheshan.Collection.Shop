using Cheshan.Collection.Shop.Core.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Cheshan.Collection.Shop.Api.Controllers
{
    [Route("cart")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartsService _service;

        public CartsController(ICartsService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }


        /// <summary>
        /// Добавить продукт в корзину
        /// </summary>
        /// <param name="productId"> Id продукта</param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add/{productId}")]
        public async Task<IActionResult> AddToCartAsync(Guid productId, Guid userId)
        {
            try
            {
                await _service.AddToCartAsync(productId, userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPost]
        [Route("remove/{productId}")]
        public async Task<IActionResult> RemoveFromCartAsync(Guid productId, Guid cartId)
        {
            try
            {
                await _service.RemoveFromCartAsync(productId, cartId);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCartAsync(Guid userId)
        {
            try
            {
                var cartGuid = await _service.CreateCartAsync(userId);
                return Ok(cartGuid);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetAsync(Guid userId)
        {
            try
            {
                var cart = await _service.GetAsync(userId);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}
