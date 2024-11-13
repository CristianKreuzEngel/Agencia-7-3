using System.Collections.Generic;
using System.Threading.Tasks;
using agencia.Database;
using agencia.DTOs;
using Microsoft.AspNetCore.Mvc;
using agencia.Models;
using agencia.Services;

namespace agencia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(DbContextMemory context)
        {
            _customerService = new CustomerService(context) ;
        }

        /// <summary>
        /// Retorna a lista de todos os clientes.
        /// </summary>
        [HttpGet]
        public async Task<List<CustomerDto>> GetCustomers()
        {
            return await _customerService.GetCustomersAsync();
        }

        /// <summary>
        /// Retorna um cliente específico pelo ID.
        /// </summary>
        /// <param name="id">ID do cliente</param>
        [HttpGet("{id}")]
        public async Task<CustomerDto> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            return customer;
        }

        /// <summary>
        /// Adiciona um novo cliente.
        /// </summary>
        /// <param name="customer">Dados do cliente a ser adicionado</param>
        [HttpPost]
        public async Task<ActionResult<Customer>> AddCustomer(Customer customer)
        {
            var newCustomer = await _customerService.AddCustomerAsync(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { id = newCustomer.Id }, newCustomer);
        }

        /// <summary>
        /// Atualiza um cliente existente.
        /// </summary>
        /// <param name="id">ID do cliente a ser atualizado</param>
        /// <param name="customer">Dados atualizados do cliente</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            await _customerService.UpdateCustomerAsync(customer);
            return NoContent();
        }

        /// <summary>
        /// Remove um cliente pelo ID.
        /// </summary>
        /// <param name="id">ID do cliente a ser removido</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }
    }
}
