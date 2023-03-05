using MagicVilla_Web.Models;

namespace MagicVilla_Web.Services.IService
{
    public interface IVillaNumberWebService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(VillaNumberCreateModel model, string token);
        Task<T> UpdateAsync<T>(VillaNumberEditModel model, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}