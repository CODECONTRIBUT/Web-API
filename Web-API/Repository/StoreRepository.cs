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

        public async Task<(List<Store>? storeList, int totalCount)> GetAllStores(QueryStoreObject queryStoreObj)
        {
            var skipNumber = (queryStoreObj.page - 1) * queryStoreObj.page_size;
            var stores = _dbContext.Stores.OrderBy(s => s.Id);

            return (await stores.Skip(skipNumber).Take(queryStoreObj.page_size).ToListAsync(), stores.Count());
        }

        public async Task<Store?> GetStoreById(int Id)
        {
            return await _dbContext.Stores.FirstOrDefaultAsync(s => s.Id == Id);
        }
    }
}
