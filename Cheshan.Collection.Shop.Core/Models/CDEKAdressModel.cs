using System.Text.Json.Serialization;

namespace Cheshan.Collection.Shop.Core.Models
{
    public class CDEKAdressModel
    {
        [JsonPropertyName("city_code")]
        public int CityCode { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("postal_code")]
        public string PostalCode { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("have_cashless")]
        public bool HaveCashless { get; set; }

        [JsonPropertyName("have_cash")]
        public bool HaveCash { get; set; }

        [JsonPropertyName("allowed_cod")]
        public bool AllowedCod { get; set; }

        [JsonPropertyName("is_dressing_room")]
        public bool HasDressingRoom { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("work_time")]
        public string WorkTime { get; set; }

        [JsonPropertyName("location")]
        public double[] Location { get; set; }

        //[JsonPropertyName("dimensions")]
        //public object? Dimensions { get; set; }

        [JsonPropertyName("region")]
        public string? Region { get; set; }

        [JsonPropertyName("weight_max")]
        public int WeightMax { get; set; }

        [JsonPropertyName("weight_min")]
        public int WeightMin { get; set; }
    }
}
