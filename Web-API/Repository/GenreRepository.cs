using Microsoft.EntityFrameworkCore;
using Web_API.Dtos.Genre;
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

        public async Task<Genre> CreateGenreAsync(Genre genre)
        {
            await _dbContext.AddAsync(genre);
            await _dbContext.SaveChangesAsync();
            return genre;
        }

        public async Task<Genre?> DeleteGenreAsync(int id)
        {
            var deletedGenre = await _dbContext.Genres.FindAsync(id);
            if (deletedGenre == null)
                return null;

            _dbContext.Genres.Remove(deletedGenre);
            await _dbContext.SaveChangesAsync();
            return deletedGenre;
        }

        public async Task<List<Genre>> GetAllGenreAsync()
        {
            return await _dbContext.Genres.ToListAsync();
        }

        public async Task<Genre?> GetGenreByIdAsync(int id)
        {
            var genre = await _dbContext.Genres.FindAsync(id);
            return genre;
        }

        public async Task<Genre?> UpdateGenreAsync(int id, UpdateGenreDto genreDto)
        {
            var existingGenre = await _dbContext.Genres.FindAsync(id);
            if (existingGenre == null)
                return null;

            existingGenre.Slug = genreDto.Slug;
            existingGenre.Name = genreDto.Name;
            existingGenre.ImageBackground = genreDto.ImageBackground;
            existingGenre.GamesCount = genreDto.GamesCount;
            existingGenre.Description = genreDto.Description;
            await _dbContext.SaveChangesAsync();

            return existingGenre;
        }
    }
}
