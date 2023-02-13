using MagicVilla_Infrastructure.Entities;

namespace MagicVilla_Infrastructure.Repositories
{
    public interface IVillaNumberRepository : IRepository<VillaNumber, int>
    {
        Task<VillaNumber> GetVillaNumberById(int VillaNo);
    }
}