using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web_API.Dtos.Product;
using Web_API.Helpers;
using Web_API.Interfaces;
using Web_API.Models;

//Congrats. Yes!!!!!!  Integration is done successfully. Just need handle details.
//!!!!!!!

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductRepository _entityRepo;

        public ProductsController(IMapper mapper, ILogger<ProductsController> logger, IProductRepository entityRepo) 
        {
            _mapper = mapper;
            _logger = logger;
            _entityRepo = entityRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] QueryObject queryObj)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var (productList, totalCount) = await _entityRepo.GetAllProductsAsync(queryObj);         
                if (productList == null)
                    return NotFound();

                var productDtos = _mapper.Map<List<ProductDto>>(productList);
                var returnResults = new
                {
                    count = totalCount,
                    next = queryObj.page * queryObj.page_size >= totalCount ? null : "https://localhost:7040/api/products?page=" + (queryObj.page + 1).ToString(),
                    results = productDtos
                };
                return Ok(returnResults);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Products Query failed");
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id) 
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var product = await _entityRepo.GetProductByIdAsync(id);
                return product == null ? NotFound() : Ok(_mapper.Map<ProductDto>(product));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Product Query failed");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequestDto createdProductDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var product = _mapper.Map<Product>(createdProductDto);
                if (product == null)
                    return NotFound();

                var productItem = await _entityRepo.CreateProductAsync(product);
                return productItem == null ? BadRequest("Create product error") 
                                           : CreatedAtAction(nameof(GetProduct), new { id = productItem.Id }, _mapper.Map<ProductDto>(productItem));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Product creation failed");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] UpdateProductRequestDto updatedProductDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _entityRepo.UpdateProductAsync(id, updatedProductDto, _mapper);
                return result == null ? BadRequest("Product not exists or update error") : Ok(_mapper.Map<ProductDto>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Product update failed");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var deletedProduct = await _entityRepo.DeleteProductAsync(id);
                return deletedProduct == null ? BadRequest("Product not exists or DB delete error") : NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Deletion of product failed");
                return BadRequest(ex.Message);
            }
        }
    }
}
