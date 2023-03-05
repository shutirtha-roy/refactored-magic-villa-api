using MagicVilla_Web.Models;

namespace MagicVilla_Web.Services.IService
{
    public interface IVillaService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(VillaCreateModel model, string token);
        Task<T> UpdateAsync<T>(VillaEditModel model, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}