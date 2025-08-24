using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WinMarket.Core.Models;
using WinMarket.Core.Services;

namespace WinMarket.ViewModel.ViewModels
{
    public partial class ManageProductsPageViewModel : ObservableObject
    {
        private const int PageSize = 10;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private List<Product> _allProducts = new();

        public ManageProductsPageViewModel(IProductService productService, ICartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
            RefreshCommand = new AsyncRelayCommand(RefreshAsync);
            LoadProductsAsync();
        }

        [ObservableProperty]
        private ObservableCollection<Product> products = new();

        [ObservableProperty]
        private string searchText;

        [ObservableProperty]
        private int currentPage = 1;

        [ObservableProperty]
        private int totalPages;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private bool hasResults = true;

        [ObservableProperty]
        private int filteredCount;

        public IAsyncRelayCommand RefreshCommand { get; }

        partial void OnSearchTextChanged(string value)
        {
            CurrentPage = 1;
            UpdatePaging();
        }

        [RelayCommand(CanExecute = nameof(CanPrevious))]
        private void PreviousPage()
        {
            CurrentPage--;
            UpdatePaging();
        }

        [RelayCommand(CanExecute = nameof(CanNext))]
        private void NextPage()
        {
            CurrentPage++;
            UpdatePaging();
        }

        [RelayCommand]
        private async Task AddToCart(int productId)
        {
            if (productId <= 0) return;
            await _cartService.AddItemAsync(productId, 1);
        }

        [RelayCommand]
        private async Task BuyNow(int productId)
        {
            if (productId <= 0) return;
            await _cartService.AddItemAsync(productId, 1);
        }

        private async void LoadProductsAsync()
        {
            IsBusy = true;
            _allProducts = (await _productService.GetFeaturedProductsAsync()).ToList();
            IsBusy = false;
            CurrentPage = 1;
            UpdatePaging();
        }

        private async Task RefreshAsync()
        {
            IsBusy = true;
            _allProducts = (await _productService.GetFeaturedProductsAsync()).ToList();
            IsBusy = false;
            CurrentPage = 1;
            UpdatePaging();
        }

        private void UpdatePaging()
        {
            var query = string.IsNullOrWhiteSpace(SearchText)
                ? _allProducts
                : _allProducts.Where(p => p.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

            var list = query.ToList();
            FilteredCount = list.Count;
            HasResults = FilteredCount > 0;

            TotalPages = Math.Max(1, (int)Math.Ceiling(FilteredCount / (double)PageSize));
            CurrentPage = Math.Clamp(CurrentPage, 1, TotalPages);

            Products = new ObservableCollection<Product>(
                list.Skip((CurrentPage - 1) * PageSize).Take(PageSize));

            NextPageCommand.NotifyCanExecuteChanged();
            PreviousPageCommand.NotifyCanExecuteChanged();
        }

        private bool CanPrevious() => CurrentPage > 1;
        private bool CanNext() => CurrentPage < TotalPages;
    }
}
