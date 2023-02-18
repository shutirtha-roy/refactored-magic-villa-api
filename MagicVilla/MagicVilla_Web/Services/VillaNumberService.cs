using MagicVilla_Infrastructure.Enum;
using MagicVilla_Web.Models;
using IVillaNumberServiceWeb = MagicVilla_Web.Services.IService.IVillaNumberService;

namespace MagicVilla_Web.Services
{
    public class VillaNumberService : BaseService, IVillaNumberServiceWeb
    {
        private readonly IHttpClientFactory _clientFactory;
        private string _villaUrl;

        public VillaNumberService(IHttpClientFactory clientFactory,
            IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            _villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> CreateAsync<T>(VillaNumberCreateModel model)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.POST,
                Data = model,
                Url = $"{_villaUrl}/api/VillaNumberAPI"
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.DELETE,
                Url = $"{_villaUrl}/api/VillaNumberAPI/{id}"
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.GET,
                Url = $"{_villaUrl}/api/VillaNumberAPI/"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.GET,
                Url = $"{_villaUrl}/api/VillaNumberAPI/{id}"
            });
        }

        public Task<T> UpdateAsync<T>(VillaNumberEditModel model)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.PUT,
                Data = model,
                Url = $"{_villaUrl}/api/VillaNumberAPI/"
            });
        }
    }
}