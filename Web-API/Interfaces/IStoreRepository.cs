﻿using Web_API.Helpers;
using Web_API.Models;

namespace Web_API.Interfaces
{
    public interface IStoreRepository
    {
        Task<(List<Store>? storeList, int totalCount)> GetAllStores(QueryStoreObject queryStoreObj);

        Task<Store?> GetStoreById(int Id);
    }
}
