using Autofac;
using MagicVilla_Infrastructure.Services;
using VillaNumberBO = MagicVilla_Infrastructure.BusinessObjects.VillaNumber;

namespace MagicVilla_VillaAPI.Model
{
    public class VillaNumberListModel : BaseModel
    {
        private IVillaNumberService _villaNumberService;

        public VillaNumberListModel(IVillaNumberService villaNumberService)
        {
            _villaNumberService = villaNumberService;
        }

        public override void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
            _villaNumberService = _scope.Resolve<IVillaNumberService>();
        }

        internal async Task<IList<VillaNumberBO>> GetAllVillaNumbers()
        {
            var villaNumbers = await _villaNumberService.GetVillaNumbers();
            return villaNumbers;
        }

        internal async Task DeleteVillaNumber(int villaNo)
        {
            await _villaNumberService.DeleteVillaNumber(villaNo);
        }
    }
}