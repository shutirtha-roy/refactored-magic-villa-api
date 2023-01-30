using VillaBO = MagicVilla_Infrastructure.BusinessObjects.Villa;
using VillaEO = MagicVilla_Infrastructure.Entities.Villa;

namespace MagicVilla_Infrastructure.Services
{
    public interface IVillaService
    {
        Task CreateVilla();
        Task EditVilla(VillaEO villa);
        Task DeleteVilla();
        Task<VillaBO> GetVilla(int id);
        Task<IList<VillaBO>> GetVillas();
    }
}