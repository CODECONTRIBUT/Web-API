namespace Web_API.Models
{
    public class ProductModel
    {
        private readonly storeContext _dbContext;
        public ProductModel(storeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Product> GetAllProducts()
        {
            try
            {
                return _dbContext.Products.ToList();
            }
            catch (Exception ex)
            {             
                return new List<Product>();
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
    }
}
