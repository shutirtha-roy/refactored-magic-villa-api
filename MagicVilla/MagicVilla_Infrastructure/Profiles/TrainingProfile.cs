using AutoMapper;
using VillaBO = MagicVilla_Infrastructure.BusinessObjects.Villa;
using VillaEO = MagicVilla_Infrastructure.Entities.Villa;


namespace MagicVilla_Infrastructure.Profiles
{
    public class TrainingProfile : Profile
    {
        public TrainingProfile()
        {
            CreateMap<VillaEO, VillaBO>()
                .ReverseMap();
        }
    }
}