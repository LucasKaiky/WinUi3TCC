using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WinMarket.Core.Models;
using WinMarket.Core.Services;

namespace WinMarket.ViewModel.ViewModels.auth
{
    public partial class LoginPageViewModel : ObservableObject
    {
        private readonly IAuthService _authService;

        public LoginPageViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string errorMessage;

        [ObservableProperty]
        private bool isBusy;

        [RelayCommand]
        private async Task LoginAsync(string password)
        {
            ErrorMessage = null;
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(password))
            {
                ErrorMessage = "Informe usuário e senha.";
                return;
            }

            IsBusy = true;
            var result = await _authService.LoginAsync(Username, password);
            IsBusy = false;

            if (result == null || !result.Succeeded)
            {
                ErrorMessage = "Falha no login. Verifique suas credenciais.";
            }
        }
    }
}
