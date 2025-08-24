using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WinMarket.Core.Models;
using WinMarket.Core.Services;

namespace WinMarket.Data.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly IAuthState _authState;

        public AuthService(HttpClient http, IAuthState authState)
        {
            _http = http;
            _authState = authState;
        }

        public async Task<AuthResult> LoginAsync(string username, string password)
        {
            var response = await _http.PostAsJsonAsync("auth/login", new { username, password });
            if (!response.IsSuccessStatusCode) return AuthResult.Failed();

            var auth = await response.Content.ReadFromJsonAsync<AuthLoginResponse>();
            if (auth == null || string.IsNullOrWhiteSpace(auth.Token)) return AuthResult.Failed();

            var users = await _http.GetFromJsonAsync<List<User>>("users");
            var user = users?.FirstOrDefault(u => u.Username?.ToLower() == username?.ToLower());
            if (user == null) return AuthResult.Failed();

            _authState.SignIn(auth.Token, user);
            return new AuthResult { Token = auth.Token, User = user };
        }
    }
}
