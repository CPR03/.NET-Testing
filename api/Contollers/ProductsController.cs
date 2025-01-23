using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using api.Models.Dto;
using api.Models.Dto.Product;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public ProductsController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Get all products
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _context.Products.ToList();

            // Mapping the products to ProductDto
            var productDtos = _mapper.Map<List<ProductDto>>(products);
            return Ok(productDtos);
        }

        // Get product by id
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var products = _mapper.Map<ProductDto>(_context.Products.Find(id));

            if (products == null)
            {
                return NotFound("Product not found");
            }

            return Ok(products);
        }

        // Create a new product
        [HttpPost]
        public IActionResult Create([FromBody] CreateProductDto createProductDto)
        {
            // createProductDto is what the user will see and interact with
            // productModel is what will be saved to the database with proper Product model
            var productModel = _mapper.Map<Product>(createProductDto);

            _context.Products.Add(productModel);
            _context.SaveChanges();

            // Sample usage for mapping the productModel to ProductDto once again.
            var productDto = _mapper.Map<ProductDto>(productModel);
            return CreatedAtAction(nameof(GetById), new { id = productModel.Id }, productDto);
        }

        // Delete a product
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound("Product not found");
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return Ok("Product deleted");
        }

        // Update a product
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateProductDto updateProductDto)
        {
            var existingProduct = _context.Products.Find(id);

            // Product not found
            if (existingProduct == null)
            {
                return NotFound("Product not found");
            }

            // Map the DTO to the existing entity
            _mapper.Map(updateProductDto, existingProduct);

            _context.Products.Update(existingProduct);
            _context.SaveChanges();

            // Return updated product
            var productDto = _mapper.Map<ProductDto>(existingProduct);
            return Ok(productDto);
        }
    }
}