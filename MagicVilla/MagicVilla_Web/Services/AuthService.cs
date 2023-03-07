using MagicVilla_Infrastructure.Enum;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IService;

namespace MagicVilla_Web.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _villaUrl;

        public AuthService(IHttpClientFactory clientFactory,
            IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            _villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> LoginAsync<T>(LoginRequestModel obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.POST,
                Data = obj,
                Url = $"{_villaUrl}/api/v1/UsersAuth/login"
            });
        }

        public Task<T> RegisterAsync<T>(RegistrationRequestModel obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.POST,
                Data = obj,
                Url = $"{_villaUrl}/api/v1/UsersAuth/register"
            });
        }
    }
}