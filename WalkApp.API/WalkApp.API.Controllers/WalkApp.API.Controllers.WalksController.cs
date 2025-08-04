using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WalkApp.DAL.WalkApp.DAL.Repositories.WalkApp.DAL.Repositories.Interface;
using WalkApp.Domain.WalkApp.Domain.DTO.WalkApp.Domain.DTO.AddRequest;
using WalkApp.Domain.WalkApp.Domain.DTO.WalkApp.Domain.DTO.New;
using WalkApp.Domain.WalkApp.Domain.DTO.WalkApp.Domain.DTO.UpdateRequest;
using WalkApp.Domain.WalkApp.Domain.Models;
using WalkApp.Domain.WalkApp.Domain.Validators;

namespace WalkApp.API.WalkApp.API.Controllers
{
    [ApiController]
    [Route("api/walk")]
    [Authorize]
    public class WalkApiController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;

        public WalkApiController(IMapper mapper, IWalkRepository walkRepository)
        {
            _mapper = mapper;
            _walkRepository = walkRepository;
        }

        //GET All
        //GET: https://localhost:7204/api/get_all_walks
        [HttpGet]
        [Route("get_all_walks")]
        public async Task<IActionResult> GetAllWalks([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
                                                     [FromQuery] string? sortBy,   [FromQuery] bool? isAscending,
                                                     [FromQuery] int pageSize = 1, [FromQuery] int pageNumber = 100)
        {
            //Get data from domain 
            var WalkDomain = await _walkRepository.GetAllWalkAsync(filterOn, filterQuery,sortBy, 
                                                                   isAscending ?? true, pageNumber,pageSize);

            //Map Domain to DTO
            var WalkDto = _mapper.Map<List<WalkDto>>(WalkDomain);

            //return dto back to the user 
            return Ok(WalkDto);
        }

        //GET By ID
        //GET: https://localhost:7204/api/walk/get_walk/id
        [HttpGet]
        [Route("get_walk/{id:Guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            //Get data from domain 
            var WalkDomain = await _walkRepository.GetWalkByIDAsync(id);

            //Map Domain to DTO
            var WalkDto = _mapper.Map<WalkDto>(WalkDomain);

            //return dto back to the user 
            return Ok(WalkDto);
        }

        // Create Walk
        // POST: https://localhost:7204/api/walk/create_walk
        [HttpPost]
        [Route("create_walk")]
        [ModelState]
        public async Task<IActionResult> CreateWalk([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            // Map DTO to domain model
            var walkDomain = _mapper.Map<Walks>(addWalkRequestDto);

            await _walkRepository.CreateWalkAsync(walkDomain);

            //Map domain model to DTO
            var walkDto = _mapper.Map<WalkDto>(walkDomain);

            // return DTO to the client
            return Ok(walkDto);
        }

        // Update Walk
        //PUT: https://localhost:7204/api/walk/update_walk/id 
        [HttpPut]
        [Route("update_walk/{id:Guid}")]
        [ModelState]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequest)
        {
            //Getting data from dto to domain
            var walkDomain = _mapper.Map<Walks>(updateWalkRequest);

            walkDomain = await _walkRepository.UpdateWalkAsync(id, walkDomain);
            if (walkDomain == null) return NotFound();

            //Mapping again from domain to dto
            var walkDto = _mapper.Map<WalkDto>(walkDomain);

            // return DTO to the client
            return Ok(walkDto);
        }


        // Delete Walk
        // DELETE: https://localhost:7204/api/walk/delete_walk/id
        [HttpDelete]
        [Route("delete_walk/{id:Guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
            var walkDomain = await _walkRepository.DeleteWalkAsync(id);

            if (walkDomain == null)
                return NotFound();

            //using mapper
            var walkDto = _mapper.Map<WalkDto>(walkDomain);

            //Return Dto back to client
            return Ok(walkDto);
        }
    }
}
