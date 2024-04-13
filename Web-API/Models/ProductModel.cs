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
    }
}
