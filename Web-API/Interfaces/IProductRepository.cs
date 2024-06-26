﻿using AutoMapper;
using Web_API.Dtos.Product;
using Web_API.Helpers;
using Web_API.Models;

namespace Web_API.Interfaces
{
    public interface IProductRepository
    {
        Task<(List<Product>? productList, int totalCount)> GetAllProductsAsync(QueryObject queryObj);

        Task<Product?> GetProductByIdAsync(int id);

        Task<Product?> CreateProductAsync(Product product);

        Task<Product?> UpdateProductAsync(int id, UpdateProductRequestDto productDto, IMapper _mapper);

        Task<Product?> DeleteProductAsync(int id);
    }
}
