using MagicVilla_Infrastructure.Enum;
using MagicVilla_Web.Models;
using IVillaServiceWeb = MagicVilla_Web.Services.IService.IVillaService;

namespace MagicVilla_Web.Services
{
    public class VillaService : BaseService, IVillaServiceWeb
    {
        private readonly IHttpClientFactory _clientFactory;
        private string _villaUrl;

        public VillaService(IHttpClientFactory clientFactory,
            IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            _villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> CreateAsync<T>(VillaCreateModel model, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.POST,
                Data = model,
                Url = $"{_villaUrl}/api/VillaAPI",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.DELETE,
                Url = $"{_villaUrl}/api/VillaAPI/{id}",
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.GET,
                Url = $"{_villaUrl}/api/VillaAPI/",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.GET,
                Url = $"{_villaUrl}/api/VillaAPI/{id}",
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(VillaEditModel model, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.PUT,
                Data = model,
                Url = $"{_villaUrl}/api/VillaAPI/",
                Token = token
            });
        }
    }
}