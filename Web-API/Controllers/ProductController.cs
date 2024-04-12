using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_API.Dtos.Product;
using Web_API.Models;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly storeContext _dbContext;
        private readonly IMapper _mapper;
        public ProductController(storeContext dbContext, IMapper mapper) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            try
            {
                var productModel = new ProductModel(_dbContext);
                var products = productModel.GetAllProducts();
                if (products == null)
                    return NotFound();

                var productDtos = _mapper.Map<List<ProductDto>>(products);
                return Ok(productDtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct([FromRoute] int id) 
        {
            try
            {
                var productModel = new ProductModel(_dbContext);
                var product = productModel.GetProduct(id);
                return product == null ? NotFound() : Ok(_mapper.Map<ProductDto>(product));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
