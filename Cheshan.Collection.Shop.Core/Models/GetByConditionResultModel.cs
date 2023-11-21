namespace Cheshan.Collection.Shop.Core.Models
{
    public class GetByConditionResultModel
    {
        public ICollection<ProductModel> Products { get; set; }

        public int MaxAmount { get; set; }

        public Database.Entities.Enums.CategoryType CategoryType { get; set; }
    }
}
