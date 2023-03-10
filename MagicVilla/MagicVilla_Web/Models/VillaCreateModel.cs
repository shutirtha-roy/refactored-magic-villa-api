using Autofac;
using AutoMapper;
using MagicVilla_Infrastructure.Services;
using System.ComponentModel.DataAnnotations;
using VillaBO = MagicVilla_Infrastructure.BusinessObjects.Villa;

namespace MagicVilla_Web.Models
{
    public class VillaCreateModel : BaseModel
    {
        [Required]
        public string Name { get; set; }
        public string? Details { get; set; }

        [Required]
        public double Rate { get; set; }
        public int Sqft { get; set; }
        public int Occupancy { get; set; }
        public string? ImageUrl { get; set; }
        public string? Amenity { get; set; }

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