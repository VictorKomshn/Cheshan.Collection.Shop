﻿using Cheshan.Collection.Shop.Core.Abstract;
using Cheshan.Collection.Shop.Core.Models;
using Cheshan.Collection.Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Cheshan.Collection.Shop.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : Controller
    {

        private readonly IProductsService _productsService;
        private readonly ICartsService _cartsService;
        private readonly IBrandService _brandService;

        public ProductsController(IProductsService service, ICartsService carts, IBrandService brandService)
        {
            _productsService = service ?? throw new ArgumentNullException(nameof(service));
            _cartsService = carts ?? throw new ArgumentNullException(nameof(carts));
            _brandService = brandService ?? throw new ArgumentNullException(nameof(carts));
        }

        [HttpGet]
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
            var condition = new ProductsCondition();
            condition.StartIndex = 0;
            var products = await _productsService.GetByConditionAsync(condition);

            var result = new ProductsViewModel
            {
                Products = products.Products.ToArray(),
                MaxAmount = products.MaxAmount,
            };
            return View(result);
        }

        [HttpGet]
        [Route("brands/{brandName}")]
        public async Task<IActionResult> GetByBrandAsync(string brandName)
        {
            var activeuser = Request.Cookies["ActiveUser"];
            if (activeuser == null)
            {
                var newUserGuid = Guid.NewGuid();
                Response.Cookies.Append("ActiveUser", newUserGuid.ToString());
                await _cartsService.CreateCartAsync(newUserGuid);
            }

            brandName = brandName.Replace('-', ' ');
            var condition = new ProductsCondition
            {
                Brand = brandName
            };

            var products = await _productsService.GetByConditionAsync(condition);

            var brand = await _brandService.GetAsync(brandName);

            ProductsViewModel viewModel = null;

            if (brand == null)
            {
                viewModel = new ProductsViewModel
                {
                    Products = Array.Empty<ProductModel>(),
                    MaxAmount = 0,
                };
            }
            else
            {
                viewModel = new ProductsViewModel
                {
                    Brand = brand,
                    MaxAmount = products.MaxAmount,
                    Products = products.Products.ToArray()
                };
            }
            return View("Index", viewModel);
        }

        [HttpGet]
        [Route("filter")]
        [Route("brands/{brandName}/filter")]
        public async Task<IActionResult> GetByConditionAsync(bool? isMan, string? brandNames, string? categories = null, int startIndex = 0, int? highestPrice = int.MaxValue, int? lowestPrice = 0, string? sizes = null, int? sort = null)
        {
            try
            {
                var condition = new ProductsCondition
                {
                    StartIndex = startIndex,
                    Category = categories,
                    HighestPrice = highestPrice,
                    IsMan = isMan,
                    LowestPrice = lowestPrice,
                    Sizes = sizes,
                    Brand = brandNames,
                };

                var products = await _productsService.GetByConditionAsync(condition, (SortingType?)sort);
                ProductsViewModel viewModel = null;

                if (string.IsNullOrEmpty(brandNames) || brandNames.Length > 1)
                {
                    viewModel = new ProductsViewModel
                    {
                        Products = products.Products.ToArray(),
                        MaxAmount = products.MaxAmount,
                    };
                }
                else
                {
                    var brand = await _brandService.GetAsync(brandNames);

                    if (brand == null)
                    {
                        viewModel = new ProductsViewModel
                        {
                            Products = Array.Empty<ProductModel>(),
                            MaxAmount = 0,
                        };
                    }
                    else
                    {

                        viewModel = new ProductsViewModel
                        {
                            Brand = brand,
                            MaxAmount = products.MaxAmount,
                            Products = products.Products.ToArray()
                        };
                    }
                }
                return View("Index", viewModel);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }


        /// <summary>
        /// Получение страницы одного продукта
        /// </summary>
        /// <param name="id">id продукта</param>
        /// <returns>Продукт, и предлагаемые с ним товары</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            try
            {
                var product = await _productsService.GetAsync(id);

                var suggestedCondition = new ProductsCondition
                {
                    Category = product.Category,
                    Take = 4,
                };

                product.Brand = "fabiana filippi";
                var suggestedProducts = await _productsService.GetByConditionAsync(suggestedCondition, null, true);
                var viewModel = new ProductViewModel
                {
                    Product = product,
                    SuggestedProducts = suggestedProducts.Products
                };
                return View("Product", viewModel);
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
            var newModelGuid = await _productsService.CreateAsync(model);
            return View(newModelGuid);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await _productsService.DeleteAsync(id);
                return View();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, ProductModel updatedMode)
        {
            try
            {
                var result = await _productsService.UpdateAsync(id, updatedMode);
                return View(result);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

    }
}
