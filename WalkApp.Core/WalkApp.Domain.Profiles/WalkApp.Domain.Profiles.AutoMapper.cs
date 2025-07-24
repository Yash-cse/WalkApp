using AutoMapper;
using WalkApp.Domain.WalkApp.Domain.DTO;
using WalkApp.Domain.WalkApp.Domain.Models;

namespace WalkApp.Domain.WalkApp.Domain.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();

            CreateMap<AddRegionRequestDto, Region>().ReverseMap();

            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
        }
    }
}
