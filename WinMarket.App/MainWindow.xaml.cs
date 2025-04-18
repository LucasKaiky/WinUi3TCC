using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Linq;
using WinMarket.App.pages;

namespace WinMarket.App
{
    public sealed partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }
        public NavigationView NavView => this.NavViewControl; // ou x:Name="NavViewControl" no XAML

        public MainWindow()
        {
            Instance = this;
            InitializeComponent();

            // Define seleção inicial (Home) e dispara navigation
            NavView.SelectedItem = NavView.MenuItems
                .OfType<NavigationViewItem>()
                .First(i => i.Tag?.ToString() == "HomePage");
        }

        private void OnNavViewSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected) return;
            if (args.SelectedItem is not NavigationViewItem sel) return;

            var pageType = sel.Tag?.ToString() switch
            {
                "HomePage" => typeof(HomePage),
                "ManageProductsPage" => typeof(ManageProductsPage),
                "CartPage" => typeof(CartPage),
                "ProductDetailsPage" => typeof(ProductDetailsPage),
                "UserProfilePage" => typeof(UserProfilePage),
                _ => typeof(HomePage)
            };

            ContentFrame.Navigate(pageType);
        }
    }
}
