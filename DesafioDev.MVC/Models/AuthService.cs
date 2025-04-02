namespace DesafioDev.MVC.Models
{
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
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

        public async Task<bool> ValidateUserAsync(LoginRequest request)
        {
            var loginData = new { email = request.Email, senha = request.Password };
            var jsonContent = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync($"{_apiBaseUrl}api/usuarios/validarLogin", jsonContent);

            return response.IsSuccessStatusCode;
        }
    }
}
