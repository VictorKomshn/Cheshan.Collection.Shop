using Cheshan.Collection.Shop.Database.Entities.Enums;

namespace Cheshan.Collection.Shop.Core.Models
{
    public class ProductsCondition
    {
        public int StartIndex { get; set; } = 0;

        //public string? Color { get; set; }

        public int? HighestPrice { get; set; }

        public int? LowestPrice { get; set; }

        public bool? IsMan { get; set; }

        public string? Category { get; set; } = null;

        public string? Brand { get; set; } = null;

        public string? Sizes { get; set; } = null;

        public string? SearchString { get; set; } = null;

        public CategoryType? CategoryType { get; set; }
    }
}
