using System.Text.Json.Serialization;
using WinMarket.Core.Models;

namespace WinMarket.Core.Models
{
    public class AuthLoginResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }

    public class AuthResult
    {
        public string Token { get; set; }
        public User User { get; set; }
        public bool Succeeded => !string.IsNullOrWhiteSpace(Token) && User != null;
        public static AuthResult Failed() => new AuthResult();
    }
}
