using Autofac;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Models
{
    public class VillaNumberDeleteModel : BaseModel
    {
        [Required]
        public int VillaNo { get; set; }

        [Required]
        public int VillaId { get; set; }
        public string SpecialDetails { get; set; }

        private IMapper _mapper;

        public VillaNumberDeleteModel()
        {

        }

        public VillaNumberDeleteModel(IMapper mapper)
        {
            _mapper = mapper;
        }

        public override void ResolveDependency(ILifetimeScope scope)
        {
            base.ResolveDependency(scope);
            _mapper = _scope.Resolve<IMapper>();
        }
    }
}