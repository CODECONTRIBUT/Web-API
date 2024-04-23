using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web_API.Dtos.Platform;
using Web_API.Interfaces;

namespace Web_API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ParentPlatformsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ParentPlatformsController> _logger;
        private readonly IParentPlatformRepository _parentPfRepo;
        public ParentPlatformsController(IMapper mapper, ILogger<ParentPlatformsController> logger, IParentPlatformRepository parentPfRepo)
        {
            _logger = logger;
            _mapper = mapper;
            _parentPfRepo = parentPfRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetParentPlatforms()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var platforms = await _parentPfRepo.GetAllParentPlatforms();
                if (platforms == null)
                    return NotFound();

                var platformDtos = _mapper.Map<List<PlatformDto>>(platforms);
                var returnResults = new
                {
                    count = platformDtos.Count,
                    next = "",
                    results = platformDtos
                };
                return Ok(returnResults);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Platform query failed.");
                return BadRequest(ex.Message);
            }
        }
    }
}
