using System.Collections.Generic;
using System.Threading.Tasks;
using WinMarket.Core.Models;

namespace WinMarket.Core.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetFeaturedProductsAsync();
        Task<IEnumerable<Product>> SearchProductsAsync(string query);
        Task<Product> GetByIdAsync(int id);
    }
}
