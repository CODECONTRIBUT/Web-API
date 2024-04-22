using Microsoft.EntityFrameworkCore;
using Web_API.Interfaces;
using Web_API.Models;

namespace Web_API.Repository
{
    public class ParentPlatformRepository : IParentPlatformRepository
    {
        private readonly storeContext _dbContext;
        public ParentPlatformRepository(storeContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<List<Platform>> GetAllParentPlatforms()
        {
            return await _dbContext.Platforms.OrderBy(p => p.Id).ToListAsync();
        }
    }
}
