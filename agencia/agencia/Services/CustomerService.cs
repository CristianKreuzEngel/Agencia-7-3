using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using agencia.DTOs;
using agencia.Interfaces;
using agencia.Models;

namespace agencia.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<CustomerDto>> GetCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                Preferences = c.Preferences.Select(p => p.Name).ToList()
            }).ToList();
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null) return null;
            
            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Preferences = customer.Preferences.Select(p => p.Name).ToList()
            };
        }

        public async Task<CustomerDto> AddCustomerAsync(Customer customer)
        {
            var newCustomer = await _customerRepository.AddAsync(customer);
            return new CustomerDto
            {
                Id = newCustomer.Id,
                Name = newCustomer.Name,
                Preferences = newCustomer.Preferences.Select(p => p.Name).ToList()
            };
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            await _customerRepository.UpdateAsync(customer);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            await _customerRepository.DeleteAsync(id);
        }
    }
}