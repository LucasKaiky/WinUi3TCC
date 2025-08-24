using System.Threading.Tasks;
using WinMarket.Core.Models;

namespace WinMarket.Core.Services
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(string username, string password);
    }
}
