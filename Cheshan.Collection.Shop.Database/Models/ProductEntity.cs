using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cheshan.Collection.Shop.Database.Models
{
    public class ProductEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public string Size { get; set; }

        public int Amount { get; set; }
    }
}
