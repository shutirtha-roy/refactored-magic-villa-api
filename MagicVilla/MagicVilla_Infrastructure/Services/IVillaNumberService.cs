using VillaNumberBO = MagicVilla_Infrastructure.BusinessObjects.VillaNumber;
using VillaNumberEO = MagicVilla_Infrastructure.Entities.VillaNumber;

namespace MagicVilla_Infrastructure.Services
{
    public interface IVillaNumberService
    {
        Task CreateVillaNumber(VillaNumberBO villaNumber);
        Task EditVillaNumber(VillaNumberBO villaNumber);
        Task DeleteVillaNumber(int villaNo);
        Task<VillaNumberBO> GetVillaNumber(int villaNo);
        Task<IList<VillaNumberBO>> GetVillaNumbers();
    }
}