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

        public List<Product>? GetAllProducts()
        {
            try
            {
                return _dbContext.Products.ToList();
            }
            catch (Exception ex)
            {             
                return null;
            }
        }

        public Product? GetProduct(int id)
        {
            try
            {
                var productItem = _dbContext.Products.Find(id);
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

        public int? CreateProduct(Product product)
        {
            try
            {
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();
                return product.Id;
            }
            catch
            {
                return null;
            }
        }

        public Product? UpdateProduct(int id, UpdateProductRequestDto updatedProductDto)
        {
            try
            {
                var productFromDb = _dbContext.Products.FirstOrDefault(m => m.Id == id);
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
                _dbContext.SaveChanges();

                return productFromDb;
            }
            catch(Exception ex)
            { 
                return null; 
            }
        }
    }
}
