using Cheshan.Collection.Shop.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cheshan.Collection.Shop.Database.EntityConfigurations
{
    public class CartEntityConfiguration : IEntityTypeConfiguration<CartEntity>
    {
        public void Configure(EntityTypeBuilder<CartEntity> builder)
        {
            builder.ToTable("Carts");
            builder.HasMany(x => x.Products);
        }
    }
}
