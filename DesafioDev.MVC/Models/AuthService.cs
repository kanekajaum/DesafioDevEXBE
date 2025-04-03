namespace DesafioDev.MVC.Models
{
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public AuthService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiBaseUrl"];
        }

        public async Task<string> ValidateUserAsync(LoginRequest request)
        {
            using var httpClient = new HttpClient();

            var loginData = new
            {
                email = request.Email,
                senha = request.Password
            };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(loginData),
                Encoding.UTF8,
                "application/json"
            );

            string url = $"{_apiBaseUrl}api/usuarios/validarLogin?email={request.Email}&senha={request.Password}";

            HttpResponseMessage response = await httpClient.PostAsync(url, jsonContent);

            var json = await response.Content.ReadAsStringAsync();
            var autenticacaoToken = JsonSerializer.Deserialize<AutenticacaoToken>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return autenticacaoToken.token;
        }
    }
    public class AutenticacaoToken
    {
        public string? token { get; set; }
    }
}
