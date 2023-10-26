using Cheshan.Collection.Shop.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cheshan.Collection.Shop.Database.EntityConfigurations
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("Products");
            builder.OwnsMany(x => x.SizesWithAmounts).WithOwner(x => x.Product).HasForeignKey(x=> x.Id);
        }
    }
}
