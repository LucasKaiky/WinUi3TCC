using System.ComponentModel;
using WinMarket.Core.Models;

namespace WinMarket.Core.Services
{
    public interface IAuthState : INotifyPropertyChanged
    {
        bool IsAuthenticated { get; }
        string Token { get; }
        User CurrentUser { get; }
        void SignIn(string token, User user);
        void SignOut();
    }
}
