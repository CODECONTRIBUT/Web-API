using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web_API.Dtos.Product;
using Web_API.Models;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly storeContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(storeContext dbContext, IMapper mapper, ILogger<ProductsController> logger) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var productModel = new ProductModel(_dbContext);
                var products = await productModel.GetAllProducts();
                if (products == null)
                    return NotFound();

                var productDtos = _mapper.Map<List<ProductDto>>(products);
                return Ok(productDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Products Query failed");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id) 
        {
            try
            {
                var productModel = new ProductModel(_dbContext);
                var product = await productModel.GetProduct(id);
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
                var product = _mapper.Map<Product>(createdProductDto);
                if (product == null)
                    return NotFound();

                var productModel = new ProductModel(_dbContext);
                var productId = await productModel.CreateProduct(product);
                return productId == null ? BadRequest("Create product error") : CreatedAtAction(nameof(GetProduct), new { id = productId }, _mapper.Map<ProductDto>(product));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Product creation failed");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] UpdateProductRequestDto updatedProductDto)
        {
            try
            {
                var productModel = new ProductModel(_dbContext);
                var result = await productModel.UpdateProduct(id, updatedProductDto);
                return result == null ? BadRequest("Product not exists or update error") : Ok(_mapper.Map<ProductDto>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Product update failed");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            try
            {
                var productModel = new ProductModel(_dbContext);
                var isSuccessful = await productModel.DeleteProduct(id);
                return isSuccessful ? NoContent() : BadRequest("Product not exists or DB delete error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Deletion of product failed");
                return BadRequest(ex.Message);
            }
        }
    }
}
