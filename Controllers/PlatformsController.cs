using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        readonly IPlatformRepo _platformRepo;
        readonly IMapper _mapper;
        readonly ICommandDataClient _commandDataClient;
        public PlatformsController(IPlatformRepo platformRepo, IMapper mapper, ICommandDataClient commandDataClient)
        {
            _platformRepo = platformRepo;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
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
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            var platformModel = _mapper.Map<Platform>(platformCreateDto);
            _platformRepo.CreatePlatform(model: platformModel);

            var platformDto = _mapper.Map<PlatformReadDto>(platformModel);

            try
            {
                await _commandDataClient.SendPlatformToCommand(request: platformDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine("an exception occured - {0}", ex.Message);
            }
            return Created(nameof(GetPlatformById), platformCreateDto);

        }
    }
}