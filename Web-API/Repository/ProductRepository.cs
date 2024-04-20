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

        public async Task<Product?> CreateProductAsync(Product product)
        {
            //When create a new product with a reference of existing platforms and parent platforms,
            //attach relationships and let EF know that rathen than handle as new platforms and parent platforms.
            var existingPlatformList = new List<Platform>();
            var platformList= product.Platforms.ToList();
            foreach(var platform in platformList)
            {
                var existingPlatform = _dbContext.Platforms.Find(platform.Id);
                if (existingPlatform == null)
                    return null;

                existingPlatformList.Add(existingPlatform);
            }
            product.Platforms = existingPlatformList;

            var parentPlatformList = product.ParentPlatforms.ToList();
            var existingParentPlatformList = new List<Platform>();
            foreach (var platform in parentPlatformList)
            {
                var existingParentPlatform = _dbContext.Platforms.Find(platform.Id);
                if (existingParentPlatform == null)
                    return null;

                existingParentPlatformList.Add(existingParentPlatform);
            }
            product.ParentPlatforms = existingParentPlatformList;

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
            var products = _dbContext.Products.Include(p => p.Screenshots)
                                              .Include(p => p.Platforms).Include(p => p.ParentPlatforms).AsQueryable();

            //if search string exists, get all products with search string only
            if (!string.IsNullOrEmpty(queryObj.search) && !string.IsNullOrWhiteSpace(queryObj.search))
            {
                products = products.Where(p => p.Name.Contains(queryObj.search)).Distinct().OrderBy(p => p.Id);
                return await products.ToListAsync();
            }

            if (queryObj.genres != null)
                products = products.Where(p => p.GenreId == queryObj.genres);

            if (queryObj.platforms != null)
                products = products.Where(p => p.Platforms.Any(s => s.Id == queryObj.platforms));

            products = products.OrderBy(p => p.Id);

            if (!string.IsNullOrEmpty(queryObj.ordering) && !string.IsNullOrWhiteSpace(queryObj.ordering))
            {
                switch (queryObj.ordering)
                {
                    case "-added":
                        {
                            products = products.OrderByDescending(p => p.CreatedDatetime);
                            break;
                        }
                    case "name":
                        {
                            products = products.OrderBy(p => p.Name);
                            break;
                        }
                    case "-released":
                        {
                            products = products.OrderByDescending(p => p.ReleasedDatetime);
                            break;
                        }                      
                    case "-metacritic":
                        {
                            products = products.OrderByDescending(p => p.MetaCritic);
                            break;
                        }
                    case "-rating":
                        {
                            products = products.OrderByDescending(p => p.RatingTop);
                            break;
                        }
                    default:
                        break;
                }
            }

            var skipNumber = (queryObj.page - 1) * queryObj.page_size;

            return await products.Skip(skipNumber).Take(queryObj.page_size).ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var productItem = await _dbContext.Products.Include(p => p.Screenshots).Include(p => p.Platforms).Include(p => p.ParentPlatforms).FirstOrDefaultAsync(p => p.Id == id);
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
            existingProduct.ReleasedDatetime = updatedProductDto.ReleasedDatetime;
            await _dbContext.SaveChangesAsync();
            return existingProduct;
        }
    }
}
