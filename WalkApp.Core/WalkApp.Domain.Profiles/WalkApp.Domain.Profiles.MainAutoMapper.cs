using AutoMapper;
using WalkApp.Domain.WalkApp.Domain.DTO.WalkApp.Domain.DTO.AddRequest;
using WalkApp.Domain.WalkApp.Domain.DTO.WalkApp.Domain.DTO.New;
using WalkApp.Domain.WalkApp.Domain.DTO.WalkApp.Domain.DTO.UpdateRequest;
using WalkApp.Domain.WalkApp.Domain.Models;

namespace WalkApp.Domain.WalkApp.Domain.Profiles
{
    public class MainAutoMapper : Profile
    {
        public MainAutoMapper()
        {
            //Region
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();

            //Walk
            CreateMap<Walks, AddWalkRequestDto>().ReverseMap();
            CreateMap<Walks, WalkDto>().ReverseMap();
            CreateMap<Walks, UpdateWalkRequestDto>().ReverseMap();

            //Difficulty
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();

        }
    }
}
