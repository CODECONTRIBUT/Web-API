using Web_API.Dtos.Product;
using Web_API.Models;

namespace Web_API.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();

        Task<Product?> GetProductByIdAsync(int id);

        Task<Product> CreateProductAsync(Product product);

        Task<Product?> UpdateProductAsync(int id, UpdateProductRequestDto productDto);

        Task<Product?> DeleteProductAsync(int id);
    }
}
