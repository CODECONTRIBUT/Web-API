using AutoMapper;
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
            product.Platforms = GetPlatformsFromDb(product.Platforms.ToList());
            product.ParentPlatforms = GetPlatformsFromDb(product.ParentPlatforms.ToList());

            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> DeleteProductAsync(int id)
        {
            var existingProduct = await _dbContext.Products.Include(p => p.Screenshots)
                                            .Include(p => p.Platforms).Include(p => p.ParentPlatforms).FirstOrDefaultAsync(m => m.Id == id);
            if (existingProduct == null)
                return null;

            _dbContext.Products.Remove(existingProduct);
            await _dbContext.SaveChangesAsync();
            return existingProduct;
        }

        public async Task<(List<Product>? productList, int totalCount)> GetAllProductsAsync(QueryObject queryObj)
        {
            var products = _dbContext.Products.Include(p => p.Screenshots)
                                              .Include(p => p.Platforms).Include(p => p.ParentPlatforms).AsQueryable();

            var skipNumber = (queryObj.page - 1) * queryObj.page_size;

            //if search string exists, get all products with search string only
            if (!string.IsNullOrEmpty(queryObj.search) && !string.IsNullOrWhiteSpace(queryObj.search))
            {
                products = products.Where(p => p.Name.Contains(queryObj.search)).Distinct().OrderBy(p => p.Id);
                return products == null ? (null, 0) : (await products.Skip(skipNumber).Take(queryObj.page_size).ToListAsync(), products.Count());
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

            return products == null ? (null, 0) : (await products.Skip(skipNumber).Take(queryObj.page_size).ToListAsync(), products.Count());
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var productItem = await _dbContext.Products.Include(p => p.Screenshots)
                                                        .Include(p => p.Platforms).Include(p => p.ParentPlatforms).FirstOrDefaultAsync(p => p.Id == id);
            return productItem;
        }

        public async Task<Product?> UpdateProductAsync(int id, UpdateProductRequestDto updatedProductDto, IMapper _mapper)
        {
            var existingProduct = await _dbContext.Products.Include(p => p.Screenshots).Include(p => p.Platforms)
                                                            .Include(p => p.ParentPlatforms).FirstOrDefaultAsync(m => m.Id == id);
            if (existingProduct == null)
                return null;

            //existingProduct.Platforms.Clear();
            existingProduct.ParentPlatforms.Clear();

            existingProduct.Slug = updatedProductDto.Slug;
            existingProduct.Name = updatedProductDto.Name;
            existingProduct.MetaCritic = updatedProductDto.MetaCritic;
            //existingProduct.BackgroundImage = updatedProductDto.Background_Image;
            //existingProduct.StoreId = updatedProductDto.StoreId;
            existingProduct.GenreId = updatedProductDto.GenreId;
            existingProduct.Description = updatedProductDto.Description;
            existingProduct.RatingTop = updatedProductDto.Rating_Top;
            //existingProduct.TrailerId = updatedProductDto.TrailerId;
            existingProduct.ReleasedDatetime = updatedProductDto.ReleasedDatetime;

            //var updatedScreenshots = _mapper.Map<List<Screenshot>>(updatedProductDto.Screenshots);
            //if (!CheckSameScreenshotList(existingProduct.Screenshots.ToList(), updatedScreenshots))
            //    existingProduct.Screenshots = updatedScreenshots;  

            //existingProduct.Platforms = GetPlatformsFromDb(_mapper.Map<List<Platform>>(updatedProductDto.Platforms));
            existingProduct.ParentPlatforms = GetPlatformsFromDb(_mapper.Map<List<Platform>>(updatedProductDto.ParentPlatforms));

            await _dbContext.SaveChangesAsync();
            return existingProduct;
        }

        private List<Platform>? GetPlatformsFromDb(List<Platform> platforms)
        {
            var existingPlatformList = new List<Platform>();
            foreach (var platform in platforms)
            {
                var existingPlatform = _dbContext.Platforms.Find(platform.Id);
                if (existingPlatform == null)
                    return null;

                existingPlatformList.Add(existingPlatform);
            }
            return existingPlatformList;
        }

        private bool CheckSameScreenshotList(List<Screenshot> existingScr,  List<Screenshot> updatedScr)
        {
            if (updatedScr == null && existingScr == null) return true;

            if (updatedScr != null && existingScr != null && updatedScr.Count == existingScr.Count)
            {
                var updatedIds = new List<int>();
                foreach(var screenshot in updatedScr)
                {
                    updatedIds.Add(screenshot.Id);
                }

                foreach (var screenshot in existingScr)
                {
                    if (!updatedIds.Contains(screenshot.Id)) 
                        return false;
                }
                return true;
            }
            return false;
        }
    }
}
