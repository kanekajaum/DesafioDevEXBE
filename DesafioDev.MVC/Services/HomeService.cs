namespace DesafioDev.MVC.Services
{
    public class HomeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public HomeService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiBaseUrl"];
        }
    }
}