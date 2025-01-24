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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductsController(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        //ANCHOR Get all products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productRepository.GetAllAsync();

            // Mapping the products to ProductDto
            var productDtos = _mapper.Map<List<ProductDto>>(products);
            return Ok(productDtos);
        }

        //ANCHOR Get product by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var products = await _productRepository.GetByIdAsync(id);

            if (products == null)
            {
                return NotFound("Product not found");
            }

            return Ok(products);
        }

        //ANCHOR Create a new product
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto createProductDto)
        {
            var product = await _productRepository.CreateAsync(createProductDto);

            // Mapping the product to ProductDto
            var productDto = _mapper.Map<ProductDto>(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, productDto);
        }

        //ANCHOR Delete a product
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var product = await _productRepository.DeleteAsync(id);

            if (product == null)
            {
                return NotFound("Product not found");
            }

            return NoContent();
        }

        //ANCHOR Update a product
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductDto updateProductDto)
        {
            var productToUpdate = await _productRepository.UpdateAsync(id, updateProductDto);

            // Product not found
            if (productToUpdate == null)
            {
                return NotFound("Product not found");
            }

            return Ok(productToUpdate);
        }
    }
}