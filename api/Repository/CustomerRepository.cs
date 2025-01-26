using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using api.Models.Dto.Customer;
using api.Repository.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly IMapper _mapper;
        private readonly ApplicationDBContext _context;
        public CustomerRepository(IMapper mapper, ApplicationDBContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        // ANCHOR Get all
        public async Task<List<CustomerDto>> GetAllAsync()
        {
            var customers = await _context.Customers.ToListAsync();
            var customersDto = _mapper.Map<List<CustomerDto>>(customers);
            return customersDto;
        }

        // ANCHOR Get by ID
        public async Task<CustomerDto> GetByIdAsync(int id)
        {
            var customerById = await _context.Customers.FindAsync(id);
            var customerByIdDto = _mapper.Map<CustomerDto>(customerById);

            return customerByIdDto;
        }

        // ANCHOR Create customer
        public async Task<CustomerDto> CreateCustomer(CreateCustomerDto createCustomerDto)
        {
            var customerModel = _mapper.Map<Customer>(createCustomerDto);

            await _context.Customers.AddAsync(customerModel);
            await _context.SaveChangesAsync();

            var customerDto = _mapper.Map<CustomerDto>(customerModel);

            return customerDto;
        }

        // ANCHOR Update Customer
        public async Task<UpdateCustomerDto?> UpdateCustomer(int id, UpdateCustomerDto updateCustomerDto)
        {
            var customerToUpdate = await _context.Customers.FindAsync(id);

            if (customerToUpdate == null)
            {
                return null;
            }

            _mapper.Map(updateCustomerDto, customerToUpdate);
            await _context.SaveChangesAsync();

            return updateCustomerDto;
        }

        // ANCHOR Delete Customer
        public async Task<CustomerDto?> DeleteCustomer(int id)
        {
            var customerToDelete = await _context.Customers.FindAsync(id);

            if (customerToDelete == null)
            {
                return null;
            }

            _context.Customers.Remove(customerToDelete);
            await _context.SaveChangesAsync();

            return _mapper.Map<CustomerDto>(customerToDelete);
        }

    }
}