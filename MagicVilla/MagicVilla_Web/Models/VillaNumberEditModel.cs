using Autofac;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using MagicVilla_Infrastructure.BusinessObjects;

namespace MagicVilla_Web.Models
{
    public class VillaNumberEditModel : BaseModel
    {
        [Required]
        public int VillaNo { get; set; }

        [Required]
        public int VillaId { get; set; }
        public string SpecialDetails { get; set; }

        private IMapper _mapper;

        public VillaNumberEditModel()
        {

        }

        public VillaNumberEditModel(IMapper mapper)
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