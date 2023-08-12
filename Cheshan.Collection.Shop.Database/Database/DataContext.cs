using Cheshan.Collection.Shop.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Cheshan.Collection.Shop.Database.Database
{
    public class DataContext: DbContext
    {

        public DbSet<ProductEntity> Products { get; set; }

        public DbSet<CartEntity> Carts { get; set; }

        public DataContext()
        {
            Database.EnsureCreated();
        }

    }
}
