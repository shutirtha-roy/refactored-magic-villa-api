using AutoMapper;
using MagicVilla_Infrastructure.BusinessObjects;
using MagicVilla_Infrastructure.UnitOfWorks;
using VillaNumberBO = MagicVilla_Infrastructure.BusinessObjects.VillaNumber;
using VillaNumberEO = MagicVilla_Infrastructure.Entities.VillaNumber;

namespace MagicVilla_Infrastructure.Services
{
    public class VillaNumberService : IVillaNumberService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;

        public VillaNumberService(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }

        public async Task CreateVillaNumber(VillaNumberBO villaNumber)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteVillaNumber(int id)
        {
            throw new NotImplementedException();
        }

        public async Task EditVillaNumber(VillaNumberBO villaNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<VillaNumberBO> GetVillaNumber(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<VillaNumberBO>> GetVillaNumbers()
        {
            throw new NotImplementedException();
        }
    }
}