using Cheshan.Collection.Shop.Database.Entities;

namespace Cheshan.Collection.Shop.Database.Models
{
    public class GetByConditionResult
    {
        public IEnumerable<ProductEntity> Products { get; set; }

        public int MaxAmount { get; set; }
    }
}
