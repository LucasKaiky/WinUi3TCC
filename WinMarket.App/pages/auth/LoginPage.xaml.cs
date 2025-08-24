using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using WinMarket.ViewModel.ViewModels.auth;

namespace WinMarket.App.pages.auth
{
    public sealed partial class LoginPage : Page
    {
        public LoginPageViewModel ViewModel { get; }

        public LoginPage()
        {
            InitializeComponent();
            ViewModel = Ioc.Default.GetRequiredService<LoginPageViewModel>();
            DataContext = ViewModel;
        }
    }
}
