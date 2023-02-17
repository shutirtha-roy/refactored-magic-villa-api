using Autofac;
using AutoMapper;
using MagicVilla_Infrastructure.Services;
using VillaNumberBO = MagicVilla_Infrastructure.BusinessObjects.VillaNumber;

namespace MagicVilla_Web.Models
{
    public class VillaNumberListModel : BaseModel
    {
        public IList<VillaNumberModel> VillaNumberModels { get; private set; }

        private IVillaNumberService _villaNumberService;
        private IMapper _mapper;


        public VillaNumberListModel(IVillaNumberService villaNumberService, IMapper mapper)
        {
            _villaNumberService = villaNumberService;
            _mapper = mapper;
            VillaNumberModels = new List<VillaNumberModel>();
        }

        public override void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
            _mapper = _scope.Resolve<IMapper>();
            _villaNumberService = _scope.Resolve<IVillaNumberService>();
        }

        private async Task AddVillaNumbersToList(IList<VillaNumberBO> villaNumbers)
        {
            foreach (var villaNumber in villaNumbers)
            {
                VillaNumberModel villaNumberModel = new();
                villaNumberModel = _mapper.Map(villaNumber, villaNumberModel);
                villaNumberModel.Villa.VillaNumbers = null;
                VillaNumberModels.Add(villaNumberModel);
            }
        }

        internal async Task<VillaNumberBO> GetVillaNumber(int villaNo)
        {
            var villaNumber = await _villaNumberService.GetVillaNumber(villaNo);
            villaNumber.Villa.VillaNumbers = null;
            return villaNumber;
        }

        internal async Task<IList<VillaNumberModel>> GetAllVillaNumbers()
        {
            var villaNumbers = await _villaNumberService.GetVillaNumbers();
            await AddVillaNumbersToList(villaNumbers);
            return VillaNumberModels;
        }

        internal async Task DeleteVillaNumber(int villaNo)
        {
            await _villaNumberService.DeleteVillaNumber(villaNo);
        }
    }
}