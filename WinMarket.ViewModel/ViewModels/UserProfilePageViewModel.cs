using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WinMarket.Core.Models;
using WinMarket.Core.Services;

namespace WinMarket.ViewModel.ViewModels
{
    public partial class UserProfilePageViewModel : ObservableObject
    {
        private readonly IAuthState _authState;

        public UserProfilePageViewModel(IAuthState authState)
        {
            _authState = authState;
            _authState.PropertyChanged += AuthStateChanged;
        }

        public User CurrentUser => _authState.CurrentUser;

        [RelayCommand]
        private void Logout()
        {
            _authState.SignOut();
        }

        private void AuthStateChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IAuthState.CurrentUser))
            {
                OnPropertyChanged(nameof(CurrentUser));
            }
        }
    }
}
