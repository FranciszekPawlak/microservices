using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPlatformRepo _repository;
        public  PlatformsController(IPlatformRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(_repository.GetAllPlatforms()));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platform = _mapper.Map<PlatformReadDto>(_repository.GetPlatformById(id));
            if(platform == null)
            {
                return NotFound();
            }

            return Ok(platform);
        }


        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platform)
        {
            var platformToCreate = _mapper.Map<Platform>(platform);
             _repository.CreatePlatform(platformToCreate);
            _repository.SaveChanges();

            var createdPlatform = _mapper.Map<PlatformReadDto>(platformToCreate);

            return CreatedAtRoute(nameof(GetPlatformById), new { Id = createdPlatform.Id}, createdPlatform);
        }
    }
}
