using Cheshan.Collection.Shop.Database.Abstract;
using Cheshan.Collection.Shop.Database.Database;
using Cheshan.Collection.Shop.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cheshan.Collection.Shop.Database.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly DataContext _dataContext;

        public BrandRepository(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task<BrandEntity?> GetAsync(string name)
        {
            //await CreateAsync("ACNE STUDIOS", "", "acne-studios-logo.png");
            //await CreateAsync("ALTEA", "Основатель текстильной династии Джузеппе Сартори (Giuseppe Sartori) открыл свой первый магазин по продаже галстуков и шарфов в 1892 г. в Милане. Последующее поколение семьи Сартори расширило семейное дело. Сегодня Altea выпускает коллекции изысканной женской и мужской одежды, аксессуаров и изделий из кожи. В компании Altea свято чтут традиции, заложенные еще Джузеппе Сартори. Как и сто лет назад, в изготовлении одежды используются лучшие ткани и дорогая фурнитура. Новый взгляд на высокие стандарты итальянского качества, креативное сочетание цветов и принтов, творческий подход к трактовке современной моды — все это главные слагаемые успеха Altea.", "altea-logo.png");
            //await CreateAsync("FABIANA FILIPPI", "Сегодня бренд Fabiana Filippi представлен в 30 странах мира и выделяется идеальным балансом качества и стиля своей продукции.Стиль Fabiana Filippi — это ощущение вечной красоты, становящееся уникальным благодаря личности обладательницы вещи. Fabiana Filippi — это элегантность вне времени.Благодаря сочетаемости моделей между собой по цвету и стилю легко собирать разные образы. В коллекциях преимущественно используются базовые цвета, которые легко впишутся в любой гардероб и внесут в него благородный шик.", "fabiana-filippi-logo.png");

            var brand = await _dataContext.Brands.FirstOrDefaultAsync(x => x.Name.ToLower().Equals(name));

            return brand;
        }



        private async Task CreateAsync(string name, string description, string logo)
        {
            var brand = new BrandEntity
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                Logo = logo
            };

            await _dataContext.AddAsync(brand);

            await _dataContext.SaveChangesAsync();
        }
    }
}
