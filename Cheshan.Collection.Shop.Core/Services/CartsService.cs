﻿using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.Core.Mappers;
using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.Database.Abstract;

namespace Cheshan.Collection.Shop.Core.Services
{
    public class CartsService : ICartsService
    {

        private readonly ICartsRepository _repository;

        public CartsService(ICartsRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task AddToCartAsync(Guid productId, Guid cartId)
        {
            try
            {
                await _repository.AddToCartAsync(productId, cartId);
            }
            catch
            {
                throw;
            }
        }
        public async Task<Guid> CreateCartAsync(Guid userId)
        {
            var createdCartGuid = await _repository.CreateCartAsync(userId);

            return createdCartGuid;
        }

        public async Task<CartModel> GetAsync(Guid id)
        {
            try
            {
                var cartEntity = await _repository.GetAsync(id);
                return cartEntity.ToModel();
            }
            catch
            {
                throw;
            }
        }

        public async Task RemoveFromCartAsync(Guid productId, Guid cartId)
        {
            try
            {
                await _repository.RemoveFromCartAsync(productId, cartId);
            }
            catch
            {
                throw;
            }
        }
    }
}
