using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web_API.Dtos.Genre;
using Web_API.Interfaces;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreRepository _genreRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsController> _logger;
        public GenresController(IGenreRepository genreRepo, IMapper mapper, ILogger<ProductsController> logger)
        {
            _genreRepo = genreRepo;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            try
            {
                var genres = await _genreRepo.GetAllGenreAsync();
                return Ok(_mapper.Map<List<GenreDto>>(genres));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Genres Query failed");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenre([FromRoute] int id)
        {
            try
            {
                var genre = await _genreRepo.GetGenreByIdAsync(id);
                if (genre == null)
                    return NotFound();

                return Ok(_mapper.Map<GenreDto>(genre));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Genre Query failed");
                return BadRequest(ex.Message);
            }
        }
    }
}
