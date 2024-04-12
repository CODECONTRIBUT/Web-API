using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_API.Models;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly storeContext _dbContext;
        public ProductController(storeContext dbContext) 
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            try
            {
                var productModel = new ProductModel(_dbContext);
                var products = productModel.GetAllProducts();
                return products == null ? NotFound() : Ok(products);
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
                return product == null ? NotFound() : Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
