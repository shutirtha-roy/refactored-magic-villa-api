using Autofac;
using AutoMapper;
using MagicVilla_Infrastructure.BusinessObjects;
using MagicVilla_Infrastructure.Services;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Models
{
    public class VillaNumberCreateModel : BaseModel
    {
        [Required]
        public int VillaNo { get; set; }

        [Required]
        public int VillaId { get; set; }
        public string SpecialDetails { get; set; }

        private IMapper _mapper;

        public VillaNumberCreateModel()
        {

        }

        public VillaNumberCreateModel(IMapper mapper)
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