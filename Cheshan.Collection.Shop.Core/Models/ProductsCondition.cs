using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cheshan.Collection.Shop.Core.Models
{
    public class ProductsCondition
    {
        public int Amount { get; set; }

        public string Color { get; set; }

        public int HighestPrice { get; set; }

        public int LowestPrice { get; set; }

        public string Category { get; set; }

        public string Size { get; set; }
    }
}
