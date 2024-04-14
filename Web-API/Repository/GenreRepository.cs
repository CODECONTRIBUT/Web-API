using Microsoft.EntityFrameworkCore;
using Web_API.Interfaces;
using Web_API.Models;

namespace Web_API.Reposity
{
    public class GenreRepository
    {
        private readonly storeContext _dbContext;
        public GenreRepository(storeContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
