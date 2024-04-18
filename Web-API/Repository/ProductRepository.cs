using Microsoft.EntityFrameworkCore;
using Web_API.Dtos.Product;
using Web_API.Helpers;
using Web_API.Interfaces;
using Web_API.Models;

namespace Web_API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly storeContext _dbContext;
        public ProductRepository(storeContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> DeleteProductAsync(int id)
        {
            var existingProduct = await _dbContext.Products.FirstOrDefaultAsync(m => m.Id == id);
            if (existingProduct == null)
                return null;

            var screenshots = await _dbContext.Screenshots.Where(s => s.ProductId == id).ToListAsync();
            _dbContext.Screenshots.RemoveRange(screenshots);
            _dbContext.Products.Remove(existingProduct);
            await _dbContext.SaveChangesAsync();
            return existingProduct;
        }

        public async Task<List<Product>> GetAllProductsAsync(QueryObject queryObj)
        {

            var products = _dbContext.Products.Include(p => p.Screenshots).AsQueryable();

            //if search string exists, get all products with search string only
            if (!string.IsNullOrEmpty(queryObj.search) && !string.IsNullOrWhiteSpace(queryObj.search))
            {
                products = products.Where(p => p.Name.Contains(queryObj.search));
                return await products.ToListAsync();
            }

            if (queryObj.genres != null)
                products = products.Where(p => p.GenreId == queryObj.genres);

            if (queryObj.platforms != null)
                products = (from Products in products
                           join Platformofproducts in _dbContext.Platformofproducts
                           on Products.Id equals Platformofproducts.ProductId into pgroup
                           from Platformofproducts in pgroup.DefaultIfEmpty()
                           where Platformofproducts.PlatformId == queryObj.platforms
                           select Products).Distinct().OrderBy(s => s.Id);

            return await products.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var productItem = await _dbContext.Products.Include(p => p.Screenshots).FirstOrDefaultAsync(p => p.Id == id);
            return productItem;
        }

        public async Task<Product?> UpdateProductAsync(int id, UpdateProductRequestDto updatedProductDto)
        {
            var existingProduct = await _dbContext.Products.FirstOrDefaultAsync(m => m.Id == id);
            if (existingProduct == null)
                return null;

            existingProduct.Slug = updatedProductDto.Slug;
            existingProduct.Name = updatedProductDto.Name;
            existingProduct.MetaCritic = updatedProductDto.MetaCritic;
            existingProduct.BackgroundImage = updatedProductDto.Background_Image;
            existingProduct.StoreId = updatedProductDto.StoreId;
            existingProduct.GenreId = updatedProductDto.GenreId;
            existingProduct.Description = updatedProductDto.Description;
            existingProduct.RatingTop = updatedProductDto.Rating_Top;
            existingProduct.TrailerId = updatedProductDto.TrailerId;
            await _dbContext.SaveChangesAsync();
            return existingProduct;
        }
    }
}
