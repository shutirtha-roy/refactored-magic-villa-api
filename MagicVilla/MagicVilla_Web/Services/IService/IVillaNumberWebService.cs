using MagicVilla_Web.Models;

namespace MagicVilla_Web.Services.IService
{
    public interface IVillaNumberWebService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(VillaNumberCreateModel model);
        Task<T> UpdateAsync<T>(VillaNumberEditModel model);
        Task<T> DeleteAsync<T>(int id);
    }
}