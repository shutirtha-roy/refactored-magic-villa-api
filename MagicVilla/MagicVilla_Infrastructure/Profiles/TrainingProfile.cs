using AutoMapper;
using VillaBO = MagicVilla_Infrastructure.BusinessObjects.Villa;
using VillaEO = MagicVilla_Infrastructure.Entities.Villa;
using VillaNumberBO = MagicVilla_Infrastructure.BusinessObjects.VillaNumber;
using VillaNumberEO = MagicVilla_Infrastructure.Entities.VillaNumber;
using LocalUserBO = MagicVilla_Infrastructure.BusinessObjects.LocalUser;
using LocalUserEO = MagicVilla_Infrastructure.Entities.LocalUser;
using RegistrationBO = MagicVilla_Infrastructure.BusinessObjects.Registration;

namespace MagicVilla_Infrastructure.Profiles
{
    public class TrainingProfile : Profile
    {
        public TrainingProfile()
        {
            CreateMap<VillaEO, VillaBO>()
                .ReverseMap();

            CreateMap<VillaNumberEO, VillaNumberBO>()
                .ReverseMap();

            CreateMap<LocalUserBO, LocalUserEO>()
                .ReverseMap();

            CreateMap<LocalUserEO, RegistrationBO>()
                .ReverseMap();

            CreateMap<LocalUserEO, LocalUserBO>()
                .ReverseMap();
        }
    }
}