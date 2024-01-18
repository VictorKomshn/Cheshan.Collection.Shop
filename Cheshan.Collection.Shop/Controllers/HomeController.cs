using Cheshan.Collection.Shop.Core.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Cheshan.Collection.Shop.Controllers
{
    [Route("")]
    [Route("home")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IProductsService _service;
        private readonly ICartsService _carts;

        public HomeController(IProductsService service, ICartsService carts)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _carts = carts ?? throw new ArgumentNullException(nameof(carts));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var activeuser = Request.Cookies["ActiveUser"];
            if (activeuser == null)
            {
                var newUserGuid = Guid.NewGuid();
                Response.Cookies.Append("ActiveUser", newUserGuid.ToString());
                await _carts.CreateCartAsync(newUserGuid);
            }
            //for (int i = 0; i <= 32; i++)
            //{
            //    await AddProducts();
            //}
            //await _service.UpdateAsync(Guid.Parse("5abe4e1c-305c-4891-b582-c2c39f58ef99"), product1);
            //await AddProducts();
            return View();
        }


        //public async Task AddProducts()
        //{
        //    var product1 = new ProductModel
        //    {
        //        Brand = "ACNE STUDIOS",
        //        Category = "брюки",
        //        Colours = new[] { "000000", "cacaca", "ffffff" },
        //        Description = "Элегантная модель брюк свободного кроя со скрытой застежкой-молнией и металлическим крючком, боковыми карманами и шлевками для ремня. Изделие выполнено из натуральной чистой шерсти, прекрасно пропускающей воздух и обеспечивающей комфорт на протяжении всего дня.",
        //        MainPhoto = "photo1-main.png",
        //        Material = "100% хлопок",
        //        Name = "Acne Studios cool pants",
        //        Photos = new[] { "photo1-second.png", "photo1-third.png", "photo1-forth.png", "photo1-fifth.png" },
        //        Price = 1000000,
        //        SalePrice = 81100,
        //        IsMen = false,
        //        SalePercent = 18,
        //        SP = "745100194346",
        //        SKU = Guid.NewGuid().ToString(),
        //    };
        //    product1.SizesWithAmounts = new List<SizeWithAmountModel>
        //    {
        //        new SizeWithAmountModel
        //        {
        //            Amount = 10,
        //            Product = product1,
        //            Size = "S"
        //        },
        //        new SizeWithAmountModel
        //        {
        //            Amount = 10,
        //            Product = product1,
        //            Size = "M"
        //        },
        //        new SizeWithAmountModel
        //        {
        //            Amount = 10,
        //            Product = product1,
        //            Size = "L"
        //        },
        //    };
        //    var product2 = new ProductModel
        //    {
        //        Brand = "ACNE STUDIOS",
        //        Category = "брюки",
        //        Colours = new[] { "000000", "cacaca", "ffffff" },
        //        Description = "Элегантная модель брюк свободного кроя со скрытой застежкой-молнией и металлическим крючком, боковыми карманами и шлевками для ремня. Изделие выполнено из натуральной чистой шерсти, прекрасно пропускающей воздух и обеспечивающей комфорт на протяжении всего дня.",
        //        MainPhoto = "photo2-main.png",
        //        Material = "100% хлопок",
        //        Name = "ACNE STUDIOS cool pants",
        //        Photos = new string[0],
        //        Price = 1000000,
        //        SalePercent = 0,
        //        SalePrice = null,
        //        SP = "745100194346",
        //        SKU = Guid.NewGuid().ToString(),
        //        IsMen = true,
        //    };
        //    product2.SizesWithAmounts = new List<SizeWithAmountModel>
        //    {
        //        new SizeWithAmountModel
        //        {
        //            Amount = 10,
        //            Product = product1,
        //            Size = "S"
        //        },
        //        new SizeWithAmountModel
        //        {
        //            Amount = 0,
        //            Product = product1,
        //            Size = "M"
        //        },
        //        new SizeWithAmountModel
        //        {
        //            Amount = 10,
        //            Product = product1,
        //            Size = "L"
        //        },
        //    };

        //    var product3 = new ProductModel
        //    {
        //        Brand = "ACNE STUDIOS",
        //        Category = "брюки",
        //        Colours = new[] { "000000", "cacaca", "ffffff" },
        //        Description = "Элегантная модель брюк свободного кроя со скрытой застежкой-молнией и металлическим крючком, боковыми карманами и шлевками для ремня. Изделие выполнено из натуральной чистой шерсти, прекрасно пропускающей воздух и обеспечивающей комфорт на протяжении всего дня.",
        //        MainPhoto = "photo3-main.png",
        //        Material = "100% хлопок",
        //        Name = "ACNE STUDIOS cool pants 2",
        //        Photos = new string[0],
        //        Price = 100000,
        //        SP = "745100194346",
        //        IsMen = true,

        //        SKU = Guid.NewGuid().ToString(),
        //    };
        //    product3.SizesWithAmounts = new List<SizeWithAmountModel>
        //    {
        //        new SizeWithAmountModel
        //        {
        //            Amount = 10,
        //            Product = product1,
        //            Size = "S"
        //        },
        //        new SizeWithAmountModel
        //        {
        //            Amount = 100,
        //            Product = product1,
        //            Size = "M"
        //        },
        //        new SizeWithAmountModel
        //        {
        //            Amount = 10,
        //            Product = product1,
        //            Size = "L"
        //        },
        //    };

        //    var product4 = new ProductModel
        //    {
        //        Brand = "ACNE STUDIOS",
        //        Category = "куртка",
        //        Description = "Элегантная модель куртки свободного кроя со скрытой застежкой-молнией и металлическим крючком, боковыми карманами и шлевками для ремня. Изделие выполнено из натуральной чистой шерсти, прекрасно пропускающей воздух и обеспечивающей комфорт на протяжении всего дня.",
        //        MainPhoto = "photo4-main.png",
        //        Material = "100% хлопок",
        //        Name = "ACNE STUDIOS cool jacket",
        //        Photos = new string[0],
        //        Price = 100000,
        //        IsMen = false,
        //        SP = "745100194346",
        //        Colours = new string[0],
        //        SKU = Guid.NewGuid().ToString(),
        //    };
        //    product4.SizesWithAmounts = new List<SizeWithAmountModel>
        //    {
        //        new SizeWithAmountModel
        //        {
        //            Amount = 0,
        //            Product = product1,
        //            Size = "S"
        //        },
        //        new SizeWithAmountModel
        //        {
        //            Amount = 0,
        //            Product = product1,
        //            Size = "M"
        //        },
        //        new SizeWithAmountModel
        //        {
        //            Amount = 0,
        //            Product = product1,
        //            Size = "L"
        //        },
        //    };

        //    var product5 = new ProductModel
        //    {
        //        Brand = "ALTEA",
        //        Category = "куртка",
        //        Description = "Элегантная модель куртки свободного кроя со скрытой застежкой-молнией и металлическим крючком, боковыми карманами и шлевками для ремня. Изделие выполнено из натуральной чистой шерсти, прекрасно пропускающей воздух и обеспечивающей комфорт на протяжении всего дня.",
        //        MainPhoto = "photo5-main.png",
        //        Material = "100% хлопок",
        //        Name = "ALTEA cool jacket",
        //        Photos = new string[0],
        //        Price = 10030,
        //        IsMen = false,
        //        SP = "745100227337",
        //        SKU = Guid.NewGuid().ToString(),
        //        Colours = new string[0],
        //    };
        //    product5.SizesWithAmounts = new List<SizeWithAmountModel>
        //    {
        //        new SizeWithAmountModel
        //        {
        //            Amount = 0,
        //            Product = product1,
        //            Size = "S"
        //        },
        //        new SizeWithAmountModel
        //        {
        //            Amount = 0,
        //            Product = product1,
        //            Size = "M"
        //        },
        //        new SizeWithAmountModel
        //        {
        //            Amount = 20,
        //            Product = product1,
        //            Size = "L"
        //        },
        //    };

        //    var product6 = new ProductModel
        //    {
        //        Brand = "ALTEA",
        //        Category = "куртка",
        //        Description = "Элегантная модель куртки свободного кроя со скрытой застежкой-молнией и металлическим крючком, боковыми карманами и шлевками для ремня. Изделие выполнено из натуральной чистой шерсти, прекрасно пропускающей воздух и обеспечивающей комфорт на протяжении всего дня.",
        //        MainPhoto = "photo6-main.png",
        //        Material = "100% хлопок",
        //        Name = "ALTEA cool jacket",
        //        Photos = new string[0],
        //        Price = 777030,
        //        IsMen = true,
        //        SKU = Guid.NewGuid().ToString(),
        //        SP = "745100227337",

        //        Colours = new string[0],
        //    };
        //    product6.SizesWithAmounts = new List<SizeWithAmountModel>
        //    {
        //        new SizeWithAmountModel
        //        {
        //            Amount = 10,
        //            Product = product1,
        //            Size = "S"
        //        },
        //        new SizeWithAmountModel
        //        {
        //            Amount = 10,
        //            Product = product1,
        //            Size = "M"
        //        },
        //        new SizeWithAmountModel
        //        {
        //            Amount = 20,
        //            Product = product1,
        //            Size = "L"
        //        },
        //    };

        //    var product7 = new ProductModel
        //    {
        //        Brand = "ALTEA",
        //        Category = "кепка",
        //        Description = "Элегантная модель куртки свободного кроя со скрытой застежкой-молнией и металлическим крючком, боковыми карманами и шлевками для ремня. Изделие выполнено из натуральной чистой шерсти, прекрасно пропускающей воздух и обеспечивающей комфорт на протяжении всего дня.",
        //        MainPhoto = "photo7-main.png",
        //        Material = "100% хлопок",
        //        Name = "ALTEA nice cap",
        //        Photos = new string[0],
        //        Price = 9888,
        //        SP = "745100227337",
        //        SKU = Guid.NewGuid().ToString(),
        //        Colours = new string[0],
        //    };
        //    product7.SizesWithAmounts = new List<SizeWithAmountModel>
        //    {
        //        new SizeWithAmountModel
        //        {
        //            Amount = 10,
        //            Product = product1,
        //            Size = "OneSize"
        //        },
        //    };

        //    var product8 = new ProductModel
        //    {
        //        Brand = "ALTEA",
        //        Category = "сумка",
        //        Description = "Элегантная модель куртки свободного кроя со скрытой застежкой-молнией и металлическим крючком, боковыми карманами и шлевками для ремня. Изделие выполнено из натуральной чистой шерсти, прекрасно пропускающей воздух и обеспечивающей комфорт на протяжении всего дня.",
        //        MainPhoto = "photo8-main.png",
        //        Material = "100% хлопок",
        //        Name = "ALTEA nice bag",
        //        Photos = new string[0],
        //        Price = 1000000,
        //        SP = "745100227337",
        //        Colours = new string[0],
        //        SKU = Guid.NewGuid().ToString(),
        //    };
        //    product8.SizesWithAmounts = new List<SizeWithAmountModel>
        //    {
        //        new SizeWithAmountModel
        //        {
        //            Amount = 4,
        //            Product = product1,
        //            Size = "OneSize"
        //        },
        //    };

        //    await _service.CreateAsync(product1);
        //    await _service.CreateAsync(product2);
        //    await _service.CreateAsync(product3);
        //    await _service.CreateAsync(product4);
        //    await _service.CreateAsync(product5);
        //    await _service.CreateAsync(product6);
        //    await _service.CreateAsync(product7);
        //    await _service.CreateAsync(product8);
        //}
    }
}
