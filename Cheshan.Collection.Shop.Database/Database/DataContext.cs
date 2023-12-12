using Cheshan.Collection.Shop.Database.Entities;
using Cheshan.Collection.Shop.Database.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Cheshan.Collection.Shop.Database.Database
{
    public class DataContext : DbContext
    {
        public DbSet<ProductEntity> Products { get; set; }

        public DbSet<CartEntity> Carts { get; set; }

        public DbSet<NotificationRecieverEntity> Notifications { get; set; }

        public DbSet<ProductNotificationRecieverEntity> ProductNotifications { get; set; }

        public DbSet<PurchaseEntity> Purchases { get; set; }

        public DbSet<BrandEntity> Brands { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
            
            //try
            //{
            //    var databaseCreator = (Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator);
            //    databaseCreator.CreateTables();
            //}
            //catch (Exception ex)
            //{
            //    var a = "pizda";
            //    //A SqlException will be thrown if tables already exist. So simply ignore it.
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new CartEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.LogTo(Console.WriteLine);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
