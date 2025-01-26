using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Models.Dto.Customer;
using api.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private ICustomerRepository _customerRepo;

        public CustomerController(IMapper mapper, ICustomerRepository customerRepo)
        {
            _mapper = mapper;
            _customerRepo = customerRepo;
        }

        // ANCHOR Get all
        [HttpGet]
        public async Task<IActionResult> GetAllCustomerAsync()
        {
            var customers = await _customerRepo.GetAllAsync();
            return Ok(customers);
        }

        // ANCHOR Get by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById([FromRoute] int id)
        {
            var customerById = await _customerRepo.GetByIdAsync(id);

            if (customerById == null)
            {
                return NotFound("Customer not found");
            }

            return Ok(customerById);
        }

        // ANCHOR Create customer
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto createCustomerDto)
        {
            var customerToCreate = await _customerRepo.CreateCustomer(createCustomerDto);
            var customerModel = _mapper.Map<Customer>(customerToCreate);

            return CreatedAtAction(nameof(GetCustomerById), new { id = customerModel.Id }, customerToCreate);
        }

        // ANCHOR Update Customer
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] int id, [FromBody] UpdateCustomerDto updateCustomerDto)
        {
            var customerToUpdate = await _customerRepo.UpdateCustomer(id, updateCustomerDto);

            if (customerToUpdate == null)
            {
                return NotFound("Customer not found");
            }

            return Ok("Customer Updated Successfully");
        }

        // ANCHOR Delete Customer
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            var customerToDelete = await _customerRepo.DeleteCustomer(id);

            if (customerToDelete == null)
            {
                return NotFound("Customer not found");
            }

            return NoContent();
        }
    }
}