using Autofac;
using MagicVilla_Infrastructure.Services;
using VillaBO = MagicVilla_Infrastructure.BusinessObjects.Villa;

namespace MagicVilla_Web.Models
{
    public class VillaListModel : BaseModel
    {
        private IVillaService _villaService;

        public VillaListModel(IVillaService villaService)
        {
            _villaService = villaService;
        }

        public override void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
            _villaService = _scope.Resolve<IVillaService>();
        }

        internal async Task<VillaBO> GetVilla(int id)
        {
            var villa = await _villaService.GetVilla(id);
            return villa;
        }

        internal async Task<IList<VillaBO>> GetAllVillas()
        {
            var villas = await _villaService.GetVillas();
            return villas;
        }

        internal async Task DeleteVilla(int id)
        {
            await _villaService.DeleteVilla(id);
        }
    }
}