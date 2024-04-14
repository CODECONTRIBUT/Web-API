using Microsoft.EntityFrameworkCore;
using Web_API.Interfaces;
using Web_API.Models;

namespace Web_API.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly storeContext _dbContext;
        public GenreRepository(storeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Genre>> GetAllGenreAsync()
        {
            return await _dbContext.Genres.ToListAsync();
        }

        public async Task<Genre?> GetGenreByIdAsync(int id)
        {
            var genre = await _dbContext.Genres.FindAsync(id);
            if (genre == null)
                return null;

            return genre;
        }
    }
}
