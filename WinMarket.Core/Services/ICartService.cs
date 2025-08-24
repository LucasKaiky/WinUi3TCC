using System.Collections.Generic;
using System.Threading.Tasks;
using WinMarket.Core.Models;

namespace WinMarket.Core.Services
{
    public interface ICartService
    {
        Task<IReadOnlyList<CartLine>> GetLinesAsync();
        Task AddItemAsync(int productId, int quantity);
        Task UpdateQuantityAsync(int productId, int quantity);
        Task RemoveItemAsync(int productId);
        Task ClearAsync();
    }
}
