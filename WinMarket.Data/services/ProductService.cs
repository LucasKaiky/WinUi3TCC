using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WinMarket.Core.Models;
using WinMarket.Core.Services;

namespace WinMarket.Data.Services
{
    public class ProductService : IProductService
    {
        private const string BaseUrl = "https://fakestoreapi.com/products";
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<IEnumerable<Product>> GetFeaturedProductsAsync()
        {
            var json = await _httpClient.GetStringAsync($"{BaseUrl}?limit=50");
            return JsonSerializer.Deserialize<List<Product>>(json);
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string query)
        {
            var json = await _httpClient.GetStringAsync(BaseUrl);
            var all = JsonSerializer.Deserialize<List<Product>>(json);
            return all.Where(p => p.Name.Contains(query, System.StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var json = await _httpClient.GetStringAsync($"{BaseUrl}/{id}");
            return JsonSerializer.Deserialize<Product>(json);
        }
    }
}
