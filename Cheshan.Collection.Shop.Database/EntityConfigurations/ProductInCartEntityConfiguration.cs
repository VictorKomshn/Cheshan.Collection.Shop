using Cheshan.Collection.Shop.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cheshan.Collection.Shop.Database.EntityConfigurations
{
    internal class ProductInCartEntityConfiguration : IEntityTypeConfiguration<ProductInCartEntity>
    {
        public void Configure(EntityTypeBuilder<ProductInCartEntity> builder)
        {
            builder.ToTable("ProductsInCarts");
            builder.HasOne(x => x.Product);
            builder.HasOne(x => x.Cart);
        }
    }
}
