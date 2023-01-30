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
            var count = await _applicationUnitOfWork.Villas.GetCount(x => x.Id == id);

            if (count == 0)
                throw new Exception("Villa doesn't exist");

            await _applicationUnitOfWork.Villas.Remove(id);
            _applicationUnitOfWork.Save();
        }

        public async Task EditVilla(VillaBO villa)
        {
            var villaEntity = await _applicationUnitOfWork.Villas.GetById(villa.Id);

            if (villaEntity == null)
                throw new Exception("Villa doesn't exist");

            villaEntity = _mapper.Map(villa, villaEntity);
            _applicationUnitOfWork.Save();
        }

        public async Task<VillaBO> GetVilla(int id)
        {
            var villaEntity = await _applicationUnitOfWork.Villas.GetById(id);

            if (villaEntity == null)
                throw new Exception("Villa doesn't exist");

            var villaBO = _mapper.Map<VillaBO>(villaEntity);

            return villaBO;
        }

        public async Task<IList<VillaBO>> GetVillas()
        {
            throw new NotImplementedException();
        }
    }
}