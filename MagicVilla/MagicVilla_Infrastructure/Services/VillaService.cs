using AutoMapper;
using MagicVilla_Infrastructure.UnitOfWorks;
using VillaBO = MagicVilla_Infrastructure.BusinessObjects.Villa;
using VillaEO = MagicVilla_Infrastructure.Entities.Villa;

namespace MagicVilla_Infrastructure.Services
{
    public class VillaService : IVillaService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;

        public VillaService(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }

        public async Task CreateVilla(VillaBO villa)
        {
            if (villa.Id > 0)
                throw new Exception("Internal Server Error");

            var count = await _applicationUnitOfWork.Villas.GetCount(x => x.Name.ToLower() == villa.Name.ToLower());

            if (count > 0)
                throw new Exception("Villa name already exists");

            var villaEntity = _mapper.Map<VillaEO>(villa);

            await _applicationUnitOfWork.Villas.Add(villaEntity);
            _applicationUnitOfWork.Save();
        }

        public async Task DeleteVilla(int id)
        {
            throw new NotImplementedException();
        }

        public async Task EditVilla(VillaBO villa)
        {
            throw new NotImplementedException();
        }

        public async Task<VillaBO> GetVilla(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<VillaBO>> GetVillas()
        {
            throw new NotImplementedException();
        }
    }
}