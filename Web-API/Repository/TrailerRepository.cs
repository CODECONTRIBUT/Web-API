using Microsoft.EntityFrameworkCore;
using Web_API.Interfaces;
using Web_API.Models;

namespace Web_API.Repository
{
    public class TrailerRepository : ITrailerRepository
    {
        private readonly storeContext _dbContext;
        public TrailerRepository(storeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Trailer>> GetAllTrailers(int productId)
        {
            return await _dbContext.Trailers.Where(t => t.ProductId == productId).OrderBy(t => t.Id).ToListAsync();
        }
    }
}
