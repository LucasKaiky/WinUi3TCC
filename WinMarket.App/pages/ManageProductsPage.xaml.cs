using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using WinMarket.ViewModel.ViewModels;

namespace WinMarket.App.pages
{
    public sealed partial class ManageProductsPage : Page
    {
        public ManageProductsPageViewModel ViewModel { get; }

        public ManageProductsPage()
        {
            InitializeComponent();
            ViewModel = Ioc.Default.GetRequiredService<ManageProductsPageViewModel>();
            DataContext = ViewModel;
        }
    }
}
