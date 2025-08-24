using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WinMarket.Core.Models
{
    public class CartProductDto
    {
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }

    public class CartDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("products")]
        public List<CartProductDto> Products { get; set; } = new();
    }

    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice => Product?.Price ?? 0m;
        public decimal LineTotal => UnitPrice * Quantity;
        public string UnitPriceFormatted => UnitPrice.ToString("C");
        public string LineTotalFormatted => LineTotal.ToString("C");
    }
}
