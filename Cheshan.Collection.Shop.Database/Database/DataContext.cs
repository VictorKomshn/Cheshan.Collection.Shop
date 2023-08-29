using Cheshan.Collection.Shop.Database.Entities;
using Cheshan.Collection.Shop.Database.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Cheshan.Collection.Shop.Database.Database
{
    public class DataContext : DbContext
    {

        public DbSet<ProductEntity> Products { get; set; }

        public DbSet<CartEntity> Carts { get; set; }

        public DbSet<NotificationRecieverEntity> Notifications { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CartEntityConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
