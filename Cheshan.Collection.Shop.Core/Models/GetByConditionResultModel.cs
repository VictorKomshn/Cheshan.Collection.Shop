namespace Cheshan.Collection.Shop.Core.Models
{
    public class GetByConditionResultModel
    {
        public IEnumerable<ProductModel> Products { get; set; }

        public int MaxAmount { get; set; }
    }
}
