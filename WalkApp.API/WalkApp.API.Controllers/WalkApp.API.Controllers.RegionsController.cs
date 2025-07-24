using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WalkApp.DAL.WalkApp.DAL.Repositories;
using WalkApp.Domain.WalkApp.Domain.DTO;
using Region = WalkApp.Domain.WalkApp.Domain.Models.Region;

namespace WalkApp.API.WalkApp.API.Controllers
{
    // https://localhost:1234/api/regions
    [ApiController]
    [Route("api/[controller]")]
    public class RegionsController : ControllerBase
    {
        // private readonly WalkAppDbContext _context;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(/* WalkAppDbContext context, */ IRegionRepository regionRepository, IMapper mapper)
        {
            // _context = context;
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        // Get All Regions 
        // GET: https://localhost:7204/api/Regions
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            //Get data from DB - core models
            var regionsDomain = await _regionRepository.GetAllRegionsAsync();

            //Map Domain models to DTOs
            /* var regionDto = new List<RegionDto>();
            foreach(var regionDomain in regionsDomain)
            {
                regionDto.Add(new RegionDto(){
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl 
                });
            } */

            //Using Mapper
            var regionDto = _mapper.Map<List<RegionDto>>(regionsDomain);

            //Return DTOs back to Client
            return Ok(regionDto);
        }

        // Get Region by id
        // GET: https://localhost:7204/api/Regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            //var regions = _context.Regions.Find(id);
            //Get Region Domain model from DB
            var regionDomain = await _regionRepository.GetRegionByIdAsync(id);
            if (regionDomain == null)
                return NotFound();

            //Map/Convert Region Domain_Model to Region_Dto
            /*var regionDto = new RegionDto{
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl 
            }; */

            //Using Mapper
            var regionDto = _mapper.Map<RegionDto>(regionDomain);

            //Return Dto back to client
            return Ok(regionDto);
        }

        //Create New Region
        //Post: https://localhost:7204/api/Regions
        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map or convert DTO TO DOMAIN MODEL
            /* var regionDomain = new Region {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl };
            */

            //Using Mapper
            var regionDomain = _mapper.Map<Region>(addRegionRequestDto);

            //Use Domain to create region
            regionDomain = await _regionRepository.CreateRegionAsync(regionDomain);

            //Maping domain model back to dto
            /* var regionDto = new RegionDto{
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl 
            };
             */
            //Using Automapper
            var regionDto = _mapper.Map<RegionDto>(regionDomain);


            //Return Dto back to client
            return CreatedAtAction(nameof(GetRegionById), new {id = regionDto.Id}, regionDto);
        }

        //Update Region
        //PUT: https://localhost:7204/api/Regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Map Dto to domain model
            /* var regionDomain = new Region
            {
                Code = updateRegionRequestDto.Code,
                Name = updateRegionRequestDto.Name,
                RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            }; */
            //Using Automapper
            var regionDomain = _mapper.Map<Region>(updateRegionRequestDto);

            //Check if the region exists
            regionDomain = await _regionRepository.UpdateRegionAsync(id, regionDomain);

            if (regionDomain == null)
                return NotFound();

            //Convert domain model to dto
            /* var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            }; */

            //Usingb Mapper
            var regionDto = _mapper.Map<RegionDto>(regionDomain);

            //Return Dto back to client
            return Ok(regionDto);
        }

        //Delete Region
        //DELETE: https://localhost:7204/api/Regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //Check if the region exists
            var regionDomain = await _regionRepository.DeleteRegionAsync(id);
            if (regionDomain == null)
                return NotFound();

            //return deleted region back
            //Maping domain model to Dto
            /* var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            }; */

            //using mapper
            var regionDto = _mapper.Map<RegionDto>(regionDomain);

            //Return Dto back to client
            return Ok(regionDto);
        }
    }
}
