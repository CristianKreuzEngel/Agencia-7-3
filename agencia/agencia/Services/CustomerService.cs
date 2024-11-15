using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using agencia.Database;
using agencia.DTOs;
using agencia.Models;
using agencia.Repositories;

namespace agencia.Services
{
    /// <summary>
    /// Serviço responsável pelas operações relacionadas a clientes.
    /// </summary>
    public class CustomerService
    {
        private readonly CustomerRepository _customerRepository;

        /// <summary>
        /// Construtor que recebe o contexto do banco de dados.
        /// </summary>
        /// <param name="context">Contexto do banco de dados</param>
        public CustomerService(DbContextMemory context)
        {
            _customerRepository = new CustomerRepository(context);
        }

        /// <summary>
        /// Obtém a lista de todos os clientes.
        /// </summary>
        /// <returns>Lista de clientes</returns>
        public async Task<List<CustomerDto>> GetCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                Preferences = c.Preferences.ToList()
            }).ToList();
        }

        /// <summary>
        /// Obtém um cliente por seu ID.
        /// </summary>
        /// <param name="id">ID do cliente</param>
        /// <returns>Dados do cliente</returns>
        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null) return null;

            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Preferences = customer.Preferences
            };
        }

        /// <summary>
        /// Adiciona um novo cliente.
        /// </summary>
        /// <param name="customer">Dados do cliente</param>
        /// <returns>Dados do cliente adicionado</returns>
        public async Task<CustomerDto> AddCustomerAsync(Customer customer)
        {
            var newCustomer = await _customerRepository.AddAsync(customer);
            return new CustomerDto
            {
                Id = newCustomer.Id,
                Name = newCustomer.Name,
                Preferences = newCustomer.Preferences.ToList()
            };
        }

        /// <summary>
        /// Atualiza um cliente existente.
        /// </summary>
        /// <param name="customer">Dados do cliente</param>
        public async Task UpdateCustomerAsync(Customer customer)
        {
            await _customerRepository.UpdateAsync(customer);
        }

        /// <summary>
        /// Remove um cliente.
        /// </summary>
        /// <param name="id">ID do cliente</param>
        public async Task DeleteCustomerAsync(int id)
        {
            await _customerRepository.DeleteAsync(id);
        }
    }
}