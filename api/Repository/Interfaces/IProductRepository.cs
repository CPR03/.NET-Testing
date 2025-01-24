using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Models.Dto;
using api.Models.Dto.Product;

namespace api.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<ProductDto?> GetByIdAsync(int id);
        Task<Product> CreateAsync(CreateProductDto createProductDto);
        Task<Product?> DeleteAsync(int id);
        Task<ProductDto?> UpdateAsync(int id, UpdateProductDto updateProductDto);
    }
}