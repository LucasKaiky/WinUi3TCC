using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using WinMarket.Core.Services;

namespace WinMarket.ViewModel.ViewModels
{
    public partial class HomePageViewModel : ObservableObject
    {
        private readonly IAuthState _authState;

        public HomePageViewModel(IAuthState authState)
        {
            _authState = authState;
            _authState.PropertyChanged += OnAuthChanged;
        }

        public string DisplayName => _authState.CurrentUser?.FullName?.Trim() is { Length: > 0 } n ? n : "Cliente"
            ;

        public string Greeting => $"Bem-vindo, {DisplayName}";

        private void OnAuthChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IAuthState.CurrentUser))
            {
                OnPropertyChanged(nameof(DisplayName));
                OnPropertyChanged(nameof(Greeting));
            }
        }
    }
}
