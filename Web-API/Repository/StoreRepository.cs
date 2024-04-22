using Microsoft.EntityFrameworkCore;
using Web_API.Helpers;
using Web_API.Interfaces;
using Web_API.Models;

namespace Web_API.Repository
{
    public class StoreRepository : IStoreRepository
    {
        private readonly storeContext _dbContext;
        public StoreRepository(storeContext dbContext) 
        { 
            _dbContext = dbContext;
        }

        public async Task<List<Store>> GetAllStores(QueryStoreObject queryStoreObj)
        {
            var skipNumber = (queryStoreObj.page - 1) * queryStoreObj.page_size;

            return await _dbContext.Stores.Skip(skipNumber).Take(queryStoreObj.page_size).ToListAsync();
        }

        public async Task<Store?> GetStoreById(int Id)
        {
            return await _dbContext.Stores.FirstOrDefaultAsync(s => s.Id == Id);
        }
    }
}
