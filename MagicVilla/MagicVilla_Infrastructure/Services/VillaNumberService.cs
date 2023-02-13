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

            var villaCount = await _applicationUnitOfWork.VillaNumbers.GetCount(x => x.VillaId == villaNumber.VillaId);

            if (villaCount == 0)
                throw new Exception("Villa doesn't exist, you must have a valid Villa");

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
            var villaNumberEntity = await _applicationUnitOfWork.VillaNumbers.GetById(villaNumber.VillaNo);

            if (villaNumberEntity == null)
                throw new Exception("Villa Number doesn't exist");

            var villaCount = await _applicationUnitOfWork.VillaNumbers.GetCount(x => x.VillaId == villaNumber.VillaId);

            if (villaCount == 0)
                throw new Exception("Villa doesn't exist, you must have a valid Villa");

            villaNumberEntity = _mapper.Map(villaNumber, villaNumberEntity);
            _applicationUnitOfWork.Save();
        }

        public async Task<VillaNumberBO> GetVillaNumber(int villaNo)
        {
            var villaNumberEntity = await _applicationUnitOfWork.VillaNumbers.GetVillaNumberById(villaNo);

            if (villaNumberEntity == null)
                throw new Exception("Villa Number doesn't exist");

            var villaNumberBO = _mapper.Map<VillaNumberBO>(villaNumberEntity);

            return villaNumberBO;
        }

        public async Task<IList<VillaNumberBO>> GetVillaNumbers()
        {
            var villaNumbersEO = await _applicationUnitOfWork.VillaNumbers.GetAllVillaNumbers();

            var villaNumbers = new List<VillaNumberBO>();

            foreach (var villaNumberEO in villaNumbersEO)
            {
                var villaNumberBO = _mapper.Map<VillaNumberBO>(villaNumberEO);
                villaNumbers.Add(villaNumberBO);
            }

            return villaNumbers;
        }
    }
}