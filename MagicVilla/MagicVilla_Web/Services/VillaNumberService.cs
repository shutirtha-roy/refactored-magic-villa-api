using MagicVilla_Infrastructure.Enum;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IService;

namespace MagicVilla_Web.Services
{
    public class VillaNumberService : BaseService, IVillaNumberWebService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string _villaUrl;

        public VillaNumberService(IHttpClientFactory clientFactory,
            IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            _villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> CreateAsync<T>(VillaNumberCreateModel model, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.POST,
                Data = model,
                Url = $"{_villaUrl}/api/VillaNumberAPI",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.DELETE,
                Url = $"{_villaUrl}/api/VillaNumberAPI/{id}",
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.GET,
                Url = $"{_villaUrl}/api/VillaNumberAPI/",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.GET,
                Url = $"{_villaUrl}/api/VillaNumberAPI/{id}",
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(VillaNumberEditModel model, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.PUT,
                Data = model,
                Url = $"{_villaUrl}/api/VillaNumberAPI/",
                Token = token
            });
        }
    }
}