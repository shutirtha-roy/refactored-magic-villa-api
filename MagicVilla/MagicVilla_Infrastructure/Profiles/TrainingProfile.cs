using AutoMapper;
using VillaBO = MagicVilla_Infrastructure.BusinessObjects.Villa;
using VillaEO = MagicVilla_Infrastructure.Entities.Villa;
using VillaNumberBO = MagicVilla_Infrastructure.BusinessObjects.VillaNumber;
using VillaNumberEO = MagicVilla_Infrastructure.Entities.VillaNumber;


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
        }
    }
}