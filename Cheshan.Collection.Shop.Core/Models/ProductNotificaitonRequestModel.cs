using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cheshan.Collection.Shop.Core.Models
{
    public class ProductNotificaitonRequestModel
    {

        [JsonPropertyName("size")]
        public string Size { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
