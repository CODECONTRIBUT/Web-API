using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web_API.Dtos.Genre;
using Web_API.Dtos.Product;
using Web_API.Interfaces;
using Web_API.Models;

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
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var genres = await _genreRepo.GetAllGenreAsync();
                if (genres == null)
                    return NotFound();

                var genreDtos = _mapper.Map<List<GenreDto>>(genres);
                var returnResults = new
                {
                    count = genreDtos.Count,
                    next = "",
                    results = genreDtos
                };
                return Ok(returnResults);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Genres Query failed");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetGenre([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var genre = await _genreRepo.GetGenreByIdAsync(id);
                return genre == null ? NotFound() : Ok(_mapper.Map<GenreDto>(genre));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Genre Query failed");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenre([FromBody] CreateGenreDto createdGenreDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdGenre = _mapper.Map<Genre>(createdGenreDto);
                if (createdGenre == null)
                    return BadRequest();

                var genre = await _genreRepo.CreateGenreAsync(createdGenre);
                return genre == null ? BadRequest("Create genre error")
                                           : CreatedAtAction(nameof(GetGenre), new { id = genre.Id }, _mapper.Map<GenreDto>(genre));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Genre creation failed");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateGenre([FromRoute] int id, [FromBody] UpdateGenreDto updatedGenreDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var updatedGenre = await _genreRepo.UpdateGenreAsync(id, updatedGenreDto);
                return updatedGenre == null ? NotFound() : Ok(_mapper.Map<GenreDto>(updatedGenre));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Genre update failed");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteGenre([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var deletedGenre = await _genreRepo.DeleteGenreAsync(id);
                return deletedGenre == null ? NotFound() : NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Genre update failed");
                return BadRequest(ex.Message);
            }
        }
    }
}
