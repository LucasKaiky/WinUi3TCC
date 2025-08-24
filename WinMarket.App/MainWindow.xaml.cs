using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using WinMarket.App.pages;
using WinMarket.App.pages.auth;
using WinMarket.Core.Services;

namespace WinMarket.App
{
    public sealed partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }
        public NavigationView NavView => this.NavViewControl;
        private readonly IAuthState _authState;

        public MainWindow()
        {
            Instance = this;
            InitializeComponent();

            _authState = Ioc.Default.GetRequiredService<IAuthState>();
            _authState.PropertyChanged += OnAuthChanged;

            NavViewControl.Visibility = Visibility.Collapsed;
            AuthFrame.Navigate(typeof(LoginPage));
        }

        private void OnAuthChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IAuthState.IsAuthenticated))
            {
                if (_authState.IsAuthenticated)
                {
                    AuthFrame.Content = null;
                    NavViewControl.Visibility = Visibility.Visible;

                    var homeItem = NavView.MenuItems
                        .OfType<NavigationViewItem>()
                        .First(i => i.Tag?.ToString() == "HomePage");

                    NavView.SelectedItem = homeItem;
                    ContentFrame.Navigate(typeof(HomePage));
                }
                else
                {
                    NavViewControl.Visibility = Visibility.Collapsed;
                    AuthFrame.Navigate(typeof(LoginPage));
                }
            }
        }

        private void OnNavViewSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (!_authState.IsAuthenticated) return;
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

            if (ContentFrame.CurrentSourcePageType != pageType)
            {
                try
                {
                    ContentFrame.Navigate(pageType);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Navegação falhou: " + ex);
                }
            }
        }
    }
}
