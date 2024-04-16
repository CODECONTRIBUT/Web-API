using Web_API.Dtos.Genre;
using Web_API.Models;

namespace Web_API.Interfaces
{
    public interface IGenreRepository
    {
        Task<List<Genre>> GetAllGenreAsync();

        Task<Genre?> GetGenreByIdAsync(int id);

        Task<Genre> CreateGenreAsync(Genre genre);

        Task<Genre?> UpdateGenreAsync(int id, UpdateGenreDto genreDto);

        Task<Genre?> DeleteGenreAsync(int id);
    }
}
