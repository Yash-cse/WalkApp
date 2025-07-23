using AutoMapper;
using WalkApp.Domain.Dtos;
using WalkApp.Domain.Models;

namespace WalkApp.Domain.MappingProfiles
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
