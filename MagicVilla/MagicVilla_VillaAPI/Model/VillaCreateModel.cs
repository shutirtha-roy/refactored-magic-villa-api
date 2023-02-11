using Autofac;
using AutoMapper;
using MagicVilla_Infrastructure.Services;
using VillaBO = MagicVilla_Infrastructure.BusinessObjects.Villa;

namespace MagicVilla_VillaAPI.Model
{
    public class VillaCreateModel : BaseModel
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public double Rate { get; set; }
        public int Sqft { get; set; }
        public int Occupancy { get; set; }
        public string ImageUrl { get; set; }
        public string Amenity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        private IVillaService _villaService;
        private IMapper _mapper;

        public VillaCreateModel()
        {
        }

        public VillaCreateModel(IVillaService villaService, IMapper mapper)
        {
            _villaService = villaService;
            _mapper = mapper;
        }

        public override void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
            _villaService = _scope.Resolve<IVillaService>();
            _mapper = _scope.Resolve<IMapper>();
        }

        internal async Task CreateVilla()
        {
            var villa = _mapper.Map<VillaBO>(this);

            await _villaService.CreateVilla(villa);
        }
    }
}