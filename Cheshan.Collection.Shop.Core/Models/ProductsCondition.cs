using Microsoft.AspNetCore.Mvc;

namespace Cheshan.Collection.Shop.Core.Models
{
    public class ProductsCondition
    {
        public int StartIndex { get; set; } = 0;

        //public string? Color { get; set; }

        public int? HighestPrice { get; set; } = int.MaxValue;

        public int? LowestPrice { get; set; } = 0;

        public bool? IsMan { get; set; }

        public string? Category { get; set; } = null;

        public string? Brand { get; set; } = null;

        public string? Sizes { get; set; } = null;

        public int? Take { get; set; } = 32;
    }
}
