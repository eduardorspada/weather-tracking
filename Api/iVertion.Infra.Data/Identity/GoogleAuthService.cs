using System.Net.Http.Headers;
using System.Text.Json;
using iVertion.Domain.Account;
using Microsoft.Extensions.Configuration;

namespace iVertion.Infra.Data.Identity
{
    public class GoogleAuthService : IGoogleAuthService<GoogleUserInfo>
    {
        private readonly IConfiguration _configuration;

        public GoogleAuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetGoogleAuthTokenAsync(string code)
        {
            var clientId = _configuration["Authentication:Google:ClientId"];
            var clientSecret = _configuration["Authentication:Google:ClientSecret"];

            var request = new HttpRequestMessage(HttpMethod.Post, "https://oauth2.googleapis.com/token");
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "code", code },
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "redirect_uri", "http://localhost:5001/api/Account/google-login" },
                { "grant_type", "authorization_code" }
            });
            var requestContent = await request.Content.ReadAsStringAsync();
            Console.WriteLine($"Request: {requestContent}");

            var client = new HttpClient();
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response: {responseContent}");

            var payload = await response.Content.ReadAsStringAsync();

            // Extraindo manualmente o token do JSON bruto sem desserializar
            var jsonDocument = JsonDocument.Parse(payload);
            var root = jsonDocument.RootElement;
            if (root.TryGetProperty("access_token", out var tokenElement))
            {
                return tokenElement.GetString();
            }
            throw new InvalidOperationException("Access token not found in the response.");
        }

        public async Task<GoogleUserInfo> GetGoogleUserInfoAsync(string token)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("https://www.googleapis.com/oauth2/v2/userinfo");

            response.EnsureSuccessStatusCode();
            var payload = await response.Content.ReadAsStringAsync();

            // Extraindo manualmente os dados do usuário do JSON bruto sem desserializar
            using var jsonDocument = JsonDocument.Parse(payload);
            var root = jsonDocument.RootElement;

            
            var googleUserInfo = new GoogleUserInfo
            {
                Id = root.GetProperty("id").GetString(),
                Email = root.GetProperty("email").GetString(),
                Name = root.GetProperty("name").GetString(),
                GivenName = root.GetProperty("given_name").GetString(),
                FamilyName = root.GetProperty("family_name").GetString(),
                Picture = root.GetProperty("picture").GetString()
            };

            return googleUserInfo;
        }
    }
}
