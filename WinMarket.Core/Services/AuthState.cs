using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json.Linq;
using WinMarket.Core.Models;

namespace WinMarket.Core.Services
{
    public partial class AuthState : ObservableObject, IAuthState
    {
        [ObservableProperty]
        private bool isAuthenticated;

        [ObservableProperty]
        private string token;

        [ObservableProperty]
        private User currentUser;

        public void SignIn(string token, User user)
        {
            Token = token;
            CurrentUser = user;
            IsAuthenticated = true;
        }

        public void SignOut()
        {
            Token = null;
            CurrentUser = null;
            IsAuthenticated = false;
        }
    }
}
