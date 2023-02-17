using MagicVilla_Web.Models;

namespace MagicVilla_Web.Services.IService
{
    public interface IVillaService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(VillaCreateModel model);
        Task<T> UpdateAsync<T>(VillaEditModel model);
        Task<T> DeleteAsync<T>(int id);
    }
}