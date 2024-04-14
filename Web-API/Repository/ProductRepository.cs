﻿using Microsoft.EntityFrameworkCore;
using Web_API.Dtos.Product;
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

            _dbContext.Products.Remove(existingProduct);
            await _dbContext.SaveChangesAsync();
            return existingProduct;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var productItem = await _dbContext.Products.FindAsync(id);
            if (productItem == null)
                return null;

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
            existingProduct.PlatformId = updatedProductDto.PlatformId;
            existingProduct.StoreId = updatedProductDto.StoreId;
            existingProduct.GenreId = updatedProductDto.GenreId;
            existingProduct.ScreenshotId = updatedProductDto.ScreenshotId;
            existingProduct.Description = updatedProductDto.Description;
            existingProduct.RatingTop = updatedProductDto.Rating_Top;
            existingProduct.TrailerId = updatedProductDto.TrailerId;
            await _dbContext.SaveChangesAsync();
            return existingProduct;
        }
    }
}