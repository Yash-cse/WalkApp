using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WalkApp.DAL.WalkApp.DAL.Repositories.WalkApp.DAL.Repositories.Interface;
using WalkApp.Domain.WalkApp.Domain.DTO.WalkApp.Domain.DTO.New;

namespace WalkApp.API.WalkApp.API.Controllers
{
    [Route("api/difficulty")]
    [ApiController]
    public class DifficultyApiController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDifficultyRepository _difficultyRepository;

        public DifficultyApiController(IMapper mapper, IDifficultyRepository difficultyRepository)
        {
            _mapper = mapper;
            _difficultyRepository = difficultyRepository;
        }

        //Get All difficulty
        //GET: 
        [HttpGet]
        [Route("get_all_difficulty")]
        public async Task<IActionResult> GetAll()
        {
            //Geting data to domain
            var domainDifficulty = await _difficultyRepository.GetAllDifficultyAsync();

            //Mapping domain to dto
            var difficultyDto = _mapper.Map<List<DifficultyDto>>(domainDifficulty);

            //return dto data 
            return Ok(difficultyDto);
        }
    }
}
