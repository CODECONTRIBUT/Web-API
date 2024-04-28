using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web_API.Dtos.Genre;
using Web_API.Dtos.Store;
using Web_API.Helpers;
using Web_API.Interfaces;
using Web_API.Models;

namespace Web_API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<StoresController> _logger;
        private readonly IStoreRepository _storeRepo;
        public StoresController(IMapper mapper, ILogger<StoresController> logger, IStoreRepository storeRepo)
        {
            _mapper = mapper;
            _logger = logger;
            _storeRepo = storeRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetStores([FromQuery] QueryStoreObject queryStoreObj)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var (stores, totalCount) = await _storeRepo.GetAllStores(queryStoreObj);
                if (stores == null)
                    return NotFound();

                var storeDtos = _mapper.Map<List<StoreDto>>(stores);
                var returnResults = new
                {
                    count = totalCount,
                    next = queryStoreObj.page * queryStoreObj.page_size >= totalCount ? null : "https://youtoapi.azurewebsites.net/api/stores?page=" + (queryStoreObj.page + 1).ToString(),
                    results = storeDtos
                };
                return Ok(returnResults);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Query Stores failed.");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetStoreById([FromRoute] int Id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var store = await _storeRepo.GetStoreById(Id);
                return store == null ? NotFound() : Ok(_mapper.Map<StoreDto>(store));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Store query failed.");
                return BadRequest(ex.Message);
            }
        }
    }
}
