using Microsoft.EntityFrameworkCore;
using Web_API.Dtos.Product;

namespace Web_API.Models
{
    public class ProductModel
    {
        private readonly storeContext _dbContext;
        public ProductModel(storeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Product>?> GetAllProducts()
        {
            try
            {
                return await _dbContext.Products.ToListAsync();
            }
            catch (Exception ex)
            {             
                return null;
            }
        }

        public async Task<Product?> GetProduct(int id)
        {
            try
            {
                var productItem = await _dbContext.Products.FindAsync(id);
                if (productItem == null)
                {
                    return null;
                }

                return productItem;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int?> CreateProduct(Product product)
        {
            try
            {
                await _dbContext.Products.AddAsync(product);
                await _dbContext.SaveChangesAsync();
                return product.Id;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Product?> UpdateProduct(int id, UpdateProductRequestDto updatedProductDto)
        {
            try
            {
                var productFromDb = await _dbContext.Products.FirstOrDefaultAsync(m => m.Id == id);
                if (productFromDb == null)
                    return null;

                productFromDb.Slug = updatedProductDto.Slug;
                productFromDb.Name = updatedProductDto.Name;
                productFromDb.MetaCritic = updatedProductDto.MetaCritic;
                productFromDb.BackgroundImage = updatedProductDto.Background_Image;
                productFromDb.PlatformId = updatedProductDto.PlatformId;
                productFromDb.StoreId = updatedProductDto.StoreId;
                productFromDb.GenreId = updatedProductDto.GenreId;
                productFromDb.ScreenshotId = updatedProductDto.ScreenshotId;
                productFromDb.Description = updatedProductDto.Description;
                productFromDb.RatingTop = updatedProductDto.Rating_Top;
                productFromDb.TrailerId = updatedProductDto.TrailerId;
                await _dbContext.SaveChangesAsync();

                return productFromDb;
            }
            catch(Exception ex)
            { 
                return null; 
            }
        }

        public async Task<Boolean> DeleteProduct(int id)
        {
            try
            {
                var productFromDb = await _dbContext.Products.FirstOrDefaultAsync(m => m.Id == id);
                if (productFromDb == null)
                    return false;

                _dbContext.Products.Remove(productFromDb);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
