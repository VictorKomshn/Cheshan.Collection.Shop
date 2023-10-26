using Microsoft.EntityFrameworkCore;

namespace Cheshan.Collection.Shop.Database.Entities
{
    [Owned]
    public class SizeWithAmountEntity
    {
        public Guid Id { get; set; }

        public ProductEntity Product { get; set; }

        public string Size { get; set; }

        public int Amount { get; set; }
    }
}
