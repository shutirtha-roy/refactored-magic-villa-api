using AutoMapper;
using MagicVilla_VillaAPI.Model;
using VillaBO = MagicVilla_Infrastructure.BusinessObjects.Villa;
using VillaNumberBO = MagicVilla_Infrastructure.BusinessObjects.VillaNumber;
using LoginBO = MagicVilla_Infrastructure.BusinessObjects.Login;
using RegistrationBO = MagicVilla_Infrastructure.BusinessObjects.Registration;

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

            CreateMap<VillaNumberCreateModel, VillaNumberBO>()
                .ReverseMap();

            CreateMap<VillaNumberBO, VillaNumberEditModel>()
                .ReverseMap();

            CreateMap<VillaNumberBO, VillaNumberModel>()
                .ReverseMap();

            CreateMap<LoginRequestModel, LoginBO>()
                .ReverseMap();

            CreateMap<RegistrationRequestModel, RegistrationBO>()
                .ReverseMap();
        }
    }
}