using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Linq;
using WinMarket.ViewModel.ViewModels;

namespace WinMarket.App.pages
{
    public sealed partial class HomePage : Page
    {
        public HomePageViewModel ViewModel { get; }

        public HomePage()
        {
            InitializeComponent();
            ViewModel = Ioc.Default.GetRequiredService<HomePageViewModel>();
            DataContext = ViewModel;
        }

        private void OnNavigateClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is string tag)
            {
                var nav = MainWindow.Instance.NavView;
                var all = nav.MenuItems.OfType<NavigationViewItem>()
                           .Concat(nav.FooterMenuItems.OfType<NavigationViewItem>());
                var item = all.FirstOrDefault(i => i.Tag?.ToString() == tag);
                if (item != null) nav.SelectedItem = item;
            }
        }
    }
}
