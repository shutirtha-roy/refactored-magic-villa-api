using MagicVilla_Infrastructure.Entities;

namespace MagicVilla_Infrastructure.Repositories
{
    public interface IUserRepository : IRepository<LocalUser, int>
    {
        Task<LocalUser> GetUserDetails(string username, string password);
    }
}