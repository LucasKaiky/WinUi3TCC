using System.Collections.Generic;
using System.Threading.Tasks;
using WinMarket.Core.Models;

namespace WinMarket.Core
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetFeaturedProductsAsync();
        Task<IEnumerable<Product>> SearchProductsAsync(string query);
    }
}
