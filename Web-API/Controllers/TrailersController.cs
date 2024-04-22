using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web_API.Dtos.Trailer;
using Web_API.Interfaces;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class TrailersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<TrailersController> _logger;
        private readonly ITrailerRepository _trailerRepo;
        public TrailersController(IMapper mapper, ILogger<TrailersController> logger, ITrailerRepository trailerRepo)
        {
            _logger = logger;
            _mapper = mapper;
            _trailerRepo = trailerRepo;
        }

        [HttpGet("{productId:int}/movies")]
        public async Task<IActionResult> GetTrailers([FromRoute] int productId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var trailers = await _trailerRepo.GetAllTrailers(productId);
                return trailers == null ? NotFound() : Ok(_mapper.Map<List<TrailerDto>>(trailers));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Trailer query failed.");
                return BadRequest(ex.Message);
            }
        }
    }
}
