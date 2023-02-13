using Autofac;
using AutoMapper;
using MagicVilla_Infrastructure.BusinessObjects;
using MagicVilla_Infrastructure.Services;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Model
{
    public class VillaNumberCreateModel : BaseModel
    {
        [Required]
        public int VillaNo { get; set; }

        [Required]
        public int VillaID { get; set; }
        public string SpecialDetails { get; set; }

        private IVillaNumberService _villaNumberService;
        private IMapper _mapper;

        public VillaNumberCreateModel(IVillaNumberService villaNumberService, IMapper mapper)
        {
            _villaNumberService = villaNumberService;
            _mapper = mapper;
        }

        public override void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
            _villaNumberService = _scope.Resolve<IVillaNumberService>();
            _mapper = _scope.Resolve<IMapper>();   
        }

        internal async Task CreateVillaNumber()
        {
            var villaNumber = _mapper.Map<VillaNumber>(this);

            await _villaNumberService.CreateVillaNumber(villaNumber);
        }
    }
}