using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WinMarket.Core.Models;
using WinMarket.Core.Services;

namespace WinMarket.Data.Services
{
    public class CartService : ICartService
    {
        private readonly HttpClient _http;
        private readonly IAuthState _auth;
        private readonly IProductService _products;
        private CartDto _cart;

        public CartService(HttpClient http, IAuthState auth, IProductService products)
        {
            _http = http;
            _auth = auth;
            _products = products;
        }

        public async Task<IReadOnlyList<CartLine>> GetLinesAsync()
        {
            await EnsureCartAsync();
            var lines = new List<CartLine>();
            foreach (var p in _cart.Products)
            {
                var prod = await _products.GetByIdAsync(p.ProductId);
                if (prod != null)
                    lines.Add(new CartLine { Product = prod, Quantity = p.Quantity });
            }
            return lines;
        }

        public async Task AddItemAsync(int productId, int quantity)
        {
            await EnsureCartAsync();
            var item = _cart.Products.FirstOrDefault(x => x.ProductId == productId);
            if (item == null)
            {
                _cart.Products.Add(new CartProductDto { ProductId = productId, Quantity = quantity });
            }
            else
            {
                item.Quantity += quantity;
            }
            await SaveAsync();
        }

        public async Task UpdateQuantityAsync(int productId, int quantity)
        {
            await EnsureCartAsync();
            var item = _cart.Products.FirstOrDefault(x => x.ProductId == productId);
            if (item == null) return;
            if (quantity <= 0)
            {
                _cart.Products.Remove(item);
            }
            else
            {
                item.Quantity = quantity;
            }
            await SaveAsync();
        }

        public async Task RemoveItemAsync(int productId)
        {
            await EnsureCartAsync();
            _cart.Products.RemoveAll(x => x.ProductId == productId);
            await SaveAsync();
        }

        public async Task ClearAsync()
        {
            await EnsureCartAsync();
            _cart.Products.Clear();
            await SaveAsync();
        }

        private async Task EnsureCartAsync()
        {
            if (_auth.CurrentUser == null) throw new InvalidOperationException("User not authenticated");
            if (_cart != null) return;

            var payload = new CartDto
            {
                UserId = _auth.CurrentUser.Id,
                Date = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                Products = new List<CartProductDto>()
            };

            var created = await _http.PostAsJsonAsync("carts", payload);
            _cart = await created.Content.ReadFromJsonAsync<CartDto>() ?? payload;
        }

        private async Task SaveAsync()
        {
            if (_cart.Id > 0)
            {
                await _http.PutAsJsonAsync($"carts/{_cart.Id}", new CartDto
                {
                    UserId = _cart.UserId,
                    Date = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                    Products = _cart.Products
                });
            }
            else
            {
                var created = await _http.PostAsJsonAsync("carts", new CartDto
                {
                    UserId = _cart.UserId,
                    Date = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                    Products = _cart.Products
                });
                var dto = await created.Content.ReadFromJsonAsync<CartDto>();
                if (dto != null) _cart.Id = dto.Id;
            }
        }
    }
}
