using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        readonly IPlatformRepo _platformRepo;
        readonly IMapper _mapper;
        public PlatformsController(IPlatformRepo platformRepo, IMapper mapper)
        {
            _platformRepo = platformRepo;
            _mapper = mapper;
        }

        
        [HttpGet("[action]")]
        public ActionResult<IEnumerable<PlatformReadDto>> GetAllPlatforms()
        {
            var platforms = _platformRepo.GetAllPlatforms();
            var mappedPlatforms = _mapper.Map<IEnumerable<PlatformReadDto>>(platforms);
            return Ok(mappedPlatforms);
        }
        [HttpGet("[action]")]
        
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platform = _platformRepo.GetById(id: id);
            var mappedPlatform = _mapper.Map<PlatformReadDto>(platform);
            if (mappedPlatform is null)
            {
               return NotFound(); 
            }
            return Ok(mappedPlatform);
        }

        [HttpPost("[action]")]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            var platformModel = _mapper.Map<Platform>(platformCreateDto);
            _platformRepo.CreatePlatform(model: platformModel);

            var platformDto = _mapper.Map<PlatformReadDto>(platformModel);

            return Created(nameof(GetPlatformById), platformCreateDto);

        }
    }
}