using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using WinMarket.ViewModel.ViewModels;

namespace WinMarket.App.pages
{
    public sealed partial class UserProfilePage : Page
    {
        public UserProfilePageViewModel ViewModel { get; }

        public UserProfilePage()
        {
            InitializeComponent();
            ViewModel = Ioc.Default.GetRequiredService<UserProfilePageViewModel>();
            DataContext = ViewModel;
        }
    }
}
