// WinMarket.Data/Services/FakeStoreProductService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WinMarket.Core.Models;
using WinMarket.Core.Services;

namespace WinMarket.Data.Services
{
    public class FakeStoreProductService : IProductService
    {
        private const string BaseUrl = "https://fakestoreapi.com/products";
        private readonly HttpClient _http;

        public FakeStoreProductService(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<Product>> GetFeaturedProductsAsync()
        {
            var json = await _http.GetStringAsync($"{BaseUrl}?limit=10");
            return JsonSerializer.Deserialize<List<Product>>(json);
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string query)
        {
            var json = await _http.GetStringAsync(BaseUrl);
            var all = JsonSerializer.Deserialize<List<Product>>(json);
            return all.Where(p => p.Name.Contains(query, StringComparison.OrdinalIgnoreCase));
        }
    }
}
