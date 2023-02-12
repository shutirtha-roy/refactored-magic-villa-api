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
            if (villaNumber.Id > 0)
                throw new Exception("Internal Server Error");

            var count = await _applicationUnitOfWork.VillaNumbers.GetCount(x => x.VillaNo == villaNumber.VillaNo);

            if (count > 0)
                throw new Exception("VillaNumber already exists");

            var villaNumberEntity = _mapper.Map<VillaNumberEO>(villaNumber);

            await _applicationUnitOfWork.VillaNumbers.Add(villaNumberEntity);
            _applicationUnitOfWork.Save();
        }

        public async Task DeleteVillaNumber(int villaNo)
        {
            var count = await _applicationUnitOfWork.VillaNumbers.GetCount(x => x.VillaNo == villaNo);

            if (count == 0)
                throw new Exception("Villa Number doesn't exist");

            await _applicationUnitOfWork.Villas.Remove(villaNo);
            _applicationUnitOfWork.Save();
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