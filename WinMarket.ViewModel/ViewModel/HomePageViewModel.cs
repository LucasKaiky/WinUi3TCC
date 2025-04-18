using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WinMarket.Core;
using WinMarket.Core.Models;

namespace WinMarket.ViewModel.ViewModels
{
    public partial class HomePageViewModel : ObservableObject
    {
        private readonly IProductService _productService;

        public ObservableCollection<Product> FeaturedProducts { get; } = new();

        [ObservableProperty]
        private string searchQuery;

        public HomePageViewModel(IProductService productService)
        {
            _productService = productService;
            _ = LoadFeaturedProductsAsync();
        }

        [RelayCommand]
        private async Task LoadFeaturedProductsAsync()
        {
            var products = await _productService.GetFeaturedProductsAsync();
            FeaturedProducts.Clear();
            foreach (var p in products)
                FeaturedProducts.Add(p);
        }

        [RelayCommand]
        private async Task SearchAsync(string query)
        {
            var results = await _productService.SearchProductsAsync(query);
            FeaturedProducts.Clear();
            foreach (var p in results)
                FeaturedProducts.Add(p);
        }

        [RelayCommand]
        private void ViewDetails(int productId)
        {
            // navegar passando productId (implemente via Messenger ou injeção de NavigationService)
        }
    }
}
