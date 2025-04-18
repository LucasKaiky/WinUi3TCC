using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Linq;

namespace WinMarket.App.pages
{
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void OnNavigateClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is string tag)
            {
                var nav = MainWindow.Instance.NavView;

                // junta MenuItems + FooterMenuItems
                var allItems = nav.MenuItems
                                  .OfType<NavigationViewItem>()
                                  .Concat(nav.FooterMenuItems.OfType<NavigationViewItem>());

                // procura pelo Tag certo
                var item = allItems.FirstOrDefault(i => i.Tag?.ToString() == tag);
                if (item != null)
                {
                    nav.SelectedItem = item;
                }
            }
        }

    }
}
