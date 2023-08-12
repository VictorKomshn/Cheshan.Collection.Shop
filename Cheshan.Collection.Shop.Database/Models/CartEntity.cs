using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cheshan.Collection.Shop.Database.Models
{
    public class CartEntity
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public ICollection<ProductEntity> Products { get; set; }
    }
}
