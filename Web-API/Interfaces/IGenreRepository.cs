using Web_API.Models;

namespace Web_API.Interfaces
{
    public interface IGenreRepository
    {
        Task<List<Genre>> GetAllGenreAsync();

        Task<Genre?> GetGenreByIdAsync(int id);
    }
}
