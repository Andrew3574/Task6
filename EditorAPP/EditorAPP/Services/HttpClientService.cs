using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text;

namespace EditorAPP.Services
{
    public class HttpClientService
    {
        private HttpClient? _httpClient;
        private CookieContainer? _cookieContainer;
        private readonly string _baseUrl;
        public HttpClientService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _baseUrl = configuration["EditorAPI:apiUrl"]!;
            InitiateHttpClient();
            SetUsernameCookie(httpContextAccessor);            
        }
        public HttpClient? GetClient()
        {
            return _httpClient;
        }

        private void SetUsernameCookie(IHttpContextAccessor httpContextAccessor)
        {
            var userSession = httpContextAccessor.HttpContext?.Session;
            if (userSession!.TryGetValue("username", out var username))
            {
                var cookie = new Cookie("username", Encoding.UTF8.GetString(username))
                {
                    Path = "/",
                    Secure = true,
                    HttpOnly = true
                };
                _cookieContainer!.Add(new Uri(_baseUrl), cookie);
            }
        }

        private void InitiateHttpClient()
        {
            _cookieContainer = new CookieContainer();
            var handler = new HttpClientHandler
            {
                CookieContainer = _cookieContainer,
                UseCookies = true
            };
            _httpClient = new HttpClient(handler);
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }
    }
}
