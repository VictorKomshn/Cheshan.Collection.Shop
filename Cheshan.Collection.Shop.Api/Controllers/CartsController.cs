﻿using Cheshan.Collection.Shop.Core.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Cheshan.Collection.Shop.Api.Controllers
{
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartsService _service;

        public CartsController(ICartsService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpPost]
        public async Task<IActionResult> AddToCartAsync(Guid productId, Guid cartId)
        {
            try
            {
                await _service.AddToCartAsync(productId, cartId);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPost]
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
        public async Task<IActionResult> GetAsync(Guid cartId)
        {
            try
            {
                var cart = await _service.GetAsync(cartId);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }


    }
}