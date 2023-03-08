using VillaBO = MagicVilla_Infrastructure.BusinessObjects.Villa;
using VillaEO = MagicVilla_Infrastructure.Entities.Villa;

namespace MagicVilla_Infrastructure.Services
{
    public interface IVillaService
    {
        Task CreateVilla(VillaBO villa);
        Task EditVilla(VillaBO villa);
        Task DeleteVilla(int id);
        Task<VillaBO> GetVilla(int id);
        Task<IList<VillaBO>> GetVillas();
        Task<IList<VillaBO>> GetAllWithRespectToPage(int pageSize, int pageNumber);
    }
}