using AutoMapper;
using MagicVilla_Web.Models;
using VillaBO = MagicVilla_Infrastructure.BusinessObjects.Villa;
using VillaNumberBO = MagicVilla_Infrastructure.BusinessObjects.VillaNumber;

namespace MagicVilla_Web.Profiles
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<VillaCreateModel, VillaBO>()
                .ReverseMap();

            CreateMap<VillaBO, VillaEditModel>()
                .ReverseMap();

            CreateMap<VillaEditModel, VillaModel>()
                .ReverseMap();

            CreateMap<VillaNumberCreateModel, VillaNumberBO>()
                .ReverseMap();

            CreateMap<VillaNumberBO, VillaNumberEditModel>()
                .ReverseMap();

            CreateMap<VillaNumberBO, VillaNumberModel>()
                .ReverseMap();
        }
    }
}