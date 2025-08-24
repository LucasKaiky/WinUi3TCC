using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WinMarket.Core.Models;
using WinMarket.Core.Services;

namespace WinMarket.ViewModel.ViewModels
{
    public partial class CartPageViewModel : ObservableObject
    {
        private readonly ICartService _cart;

        public CartPageViewModel(ICartService cart)
        {
            _cart = cart;
            LoadCommand = new AsyncRelayCommand(LoadAsync);
            CheckoutCommand = new AsyncRelayCommand(CheckoutAsync);
            LoadCommand.Execute(null);
        }

        [ObservableProperty]
        private ObservableCollection<CartLine> items = new();

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string message;

        public IAsyncRelayCommand LoadCommand { get; }
        public IAsyncRelayCommand CheckoutCommand { get; }

        public int ItemsCount => Items?.Sum(i => i.Quantity) ?? 0;
        public decimal Subtotal => Items?.Sum(i => i.LineTotal) ?? 0m;
        public string SubtotalFormatted => Subtotal.ToString("C");
        public bool HasItems => Items?.Any() == true;

        [RelayCommand]
        private async Task Increase(CartLine line)
        {
            if (line == null) return;
            await _cart.UpdateQuantityAsync(line.Product.Id, line.Quantity + 1);
            await LoadAsync();
        }

        [RelayCommand]
        private async Task Decrease(CartLine line)
        {
            if (line == null) return;
            var newQty = line.Quantity - 1;
            await _cart.UpdateQuantityAsync(line.Product.Id, newQty);
            await LoadAsync();
        }

        [RelayCommand]
        private async Task Remove(CartLine line)
        {
            if (line == null) return;
            await _cart.RemoveItemAsync(line.Product.Id);
            await LoadAsync();
        }

        [RelayCommand]
        private async Task Clear()
        {
            await _cart.ClearAsync();
            await LoadAsync();
        }

        private async Task LoadAsync()
        {
            IsBusy = true;
            var list = await _cart.GetLinesAsync();
            Items = new ObservableCollection<CartLine>(list);
            IsBusy = false;
            OnPropertyChanged(nameof(ItemsCount));
            OnPropertyChanged(nameof(Subtotal));
            OnPropertyChanged(nameof(SubtotalFormatted));
            OnPropertyChanged(nameof(HasItems));
        }

        private async Task CheckoutAsync()
        {
            if (!HasItems) return;
            await _cart.ClearAsync();
            await LoadAsync();
            Message = "Compra finalizada com sucesso.";
        }
    }
}
