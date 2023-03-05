using MagicVilla_Web.Models;

namespace MagicVilla_Web.Services.IService
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginRequestModel obj);
        Task<T> RegisterAsync<T>(RegistrationRequestModel obj);
    }
}