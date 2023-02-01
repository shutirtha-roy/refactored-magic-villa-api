using AutoMapper;
using MagicVilla_VillaAPI.Model;
using VillaBO = MagicVilla_Infrastructure.BusinessObjects.Villa;

namespace MagicVilla_VillaAPI.Profiles
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<VillaCreateModel, VillaBO>()
                .ReverseMap();

            CreateMap<VillaBO, VillaEditModel>()
                .ReverseMap();
        }
    }
}