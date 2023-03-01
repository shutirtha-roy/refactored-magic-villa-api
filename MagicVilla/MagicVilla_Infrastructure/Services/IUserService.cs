using MagicVilla_Infrastructure.BusinessObjects;

namespace MagicVilla_Infrastructure.Services
{
    public interface IUserService
    {
        Task<bool> IsUniqueUser(string username);
        Task<LoginResponse> Login(Login loginRequest);
        Task<LocalUser> Register(Registration registrationRequest);
    }
}