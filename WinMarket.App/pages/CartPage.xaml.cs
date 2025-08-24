using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using WinMarket.ViewModel.ViewModels;

namespace WinMarket.App.pages
{
    public sealed partial class CartPage : Page
    {
        public CartPageViewModel ViewModel { get; }

        public CartPage()
        {
            InitializeComponent();
            ViewModel = Ioc.Default.GetRequiredService<CartPageViewModel>();
            DataContext = ViewModel;
        }
    }
}
