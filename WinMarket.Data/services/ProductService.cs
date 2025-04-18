using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WinMarket.Core;
using WinMarket.Core.Models;

namespace WinMarket.Data.services
{
    public class ProductService : IProductService
    {
        public async Task<IEnumerable<Product>> GetFeaturedProductsAsync()
        {
            var json = await File.ReadAllTextAsync("Assets/featured.json");
            return JsonConvert.DeserializeObject<List<Product>>(json);
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string query)
        {
            var all = await GetFeaturedProductsAsync();
            return all
                .Where(p => p.Name?.Contains(query, System.StringComparison.OrdinalIgnoreCase) == true)
                .ToList();
        }
    }
}
