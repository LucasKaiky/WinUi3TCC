// WinMarket.ViewModel/ViewModels/ManageProductsPageViewModel.cs
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private List<Product> _allProducts = new();

        public ManageProductsPageViewModel(IProductService productService)
        {
            _productService = productService;
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
        private void AddToCart(Product product) { }

        [RelayCommand]
        private void BuyNow(Product product) { }

        private async void LoadProductsAsync()
        {
            _allProducts = (await _productService.GetFeaturedProductsAsync()).ToList();
            CurrentPage = 1;
            UpdatePaging();
        }

        private void UpdatePaging()
        {
            var filtro = string.IsNullOrWhiteSpace(SearchText)
                ? _allProducts
                : _allProducts.Where(p => p.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

            TotalPages = (int)Math.Ceiling(filtro.Count() / (double)PageSize);
            Products = new ObservableCollection<Product>(
                filtro
                  .Skip((CurrentPage - 1) * PageSize)
                  .Take(PageSize));

            NextPageCommand.NotifyCanExecuteChanged();
            PreviousPageCommand.NotifyCanExecuteChanged();
        }

        private bool CanPrevious() => CurrentPage > 1;
        private bool CanNext() => CurrentPage < TotalPages;
    }
}
