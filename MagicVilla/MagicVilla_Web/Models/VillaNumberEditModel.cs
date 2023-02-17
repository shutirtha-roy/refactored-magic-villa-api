using Autofac;
using AutoMapper;
using MagicVilla_Infrastructure.Services;
using System.ComponentModel.DataAnnotations;
using VillaNumberBO = MagicVilla_Infrastructure.BusinessObjects.VillaNumber;
using VillaNumberEO = MagicVilla_Infrastructure.Entities.VillaNumber;

namespace MagicVilla_Web.Models
{
    public class VillaNumberEditModel : BaseModel
    {
        [Required]
        public int VillaNo { get; set; }

        [Required]
        public int VillaId { get; set; }
        public string SpecialDetails { get; set; }

        private IVillaNumberService _villaNumberService;
        private IMapper _mapper;

        public VillaNumberEditModel(IVillaNumberService villaNumberService, IMapper mapper)
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

        internal async Task<VillaNumberEditModel> GetVillaNumber(int villaNo)
        {
            var villaNumber = await _villaNumberService.GetVillaNumber(villaNo);
            return _mapper.Map<VillaNumberEditModel>(villaNumber);
        }

        internal async Task EditVillaNumber()
        {
            var villaNumber = _mapper.Map<VillaNumberBO>(this);
            await _villaNumberService.EditVillaNumber(villaNumber);
        }
    }
}