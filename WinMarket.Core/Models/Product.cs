// WinMarket.Core/Models/Product.cs
using System.Text.Json.Serialization;

namespace WinMarket.Core.Models
{
    public class Product
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Name { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("image")]
        public string ImageUrl { get; set; }

        [JsonIgnore]
        public string PriceFormatted => Price.ToString("C");
    }
}
