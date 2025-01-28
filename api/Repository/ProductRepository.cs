using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using api.Models.Dto;
using api.Models.Dto.Product;
using api.Repository.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ProductRepository : IProductRepository
    {

        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        public ProductRepository(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //ANCHOR Get all products including customers 
        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Customers)
                .ToListAsync();
        }

        //ANCHOR Get product by id
        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Customers)
                .FirstOrDefaultAsync(p => p.Id == id);

            return product == null ? null : _mapper.Map<ProductDto>(product);
        }

        //ANCHOR Create a new product
        public async Task<Product> CreateAsync(CreateProductDto createProductDto)
        {
            // createProductDto is what the user will see and interact with
            // productModel is what will be saved to the database with proper Product model
            var productModel = _mapper.Map<Product>(createProductDto);

            await _context.Products.AddAsync(productModel);
            await _context.SaveChangesAsync();

            return productModel;
        }

        //ANCHOR Delete a product
        public async Task<Product?> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            // Product not found
            if (product == null)
            {
                return null;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        //ANCHOR Update a product
        public async Task<ProductDto?> UpdateAsync(int id, UpdateProductDto updateProductDto)
        {
            var existingProduct = await _context.Products.FindAsync(id);

            // Product not found
            if (existingProduct == null)
            {
                return null;
            }

            // Map the DTO to the existing entity
            _mapper.Map(updateProductDto, existingProduct);

            _context.Products.Update(existingProduct);
            _context.SaveChanges();

            var productDto = _mapper.Map<ProductDto>(existingProduct);
            return productDto;
        }
    }
}