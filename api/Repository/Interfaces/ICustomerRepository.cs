using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Models.Dto.Customer;

namespace api.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<CustomerDto>> GetAllAsync();
        Task<CustomerDto> GetByIdAsync(int id);

        Task<CustomerDto> CreateCustomer(CreateCustomerDto createCustomerDto);
        Task<UpdateCustomerDto?> UpdateCustomer(int id, UpdateCustomerDto updateCustomerDto);
        Task<CustomerDto?> DeleteCustomer(int id);
    }
}