using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using agencia.Database;
using agencia.DTOs;
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
            _customerService = new CustomerService(context);
        }

        /// <summary>
        /// Retorna a lista de todos os clientes.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<CustomerDto>>> GetCustomers()
        {
            return Ok(await _customerService.GetCustomersAsync());
        }

        /// <summary>
        /// Retorna um cliente específico pelo ID.
        /// </summary>
        /// <param name="id">ID do cliente</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound("Cliente não encontrado.");
            
            return Ok(customer);
        }

        /// <summary>
        /// Adiciona um novo cliente.
        /// </summary>
        /// <param name="customerCreateDto">Dados do cliente a ser adicionado</param>
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> AddCustomer([FromBody] CustomerCreateDto customerCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                var customer = new Customer
                {
                    Name = customerCreateDto.Name,
                    Preferences = customerCreateDto.Preferences.Select(p => new Tag { Name = p }).ToList()
                };

                var newCustomer = await _customerService.AddCustomerAsync(customer);
                return CreatedAtAction(nameof(GetCustomerById), new { id = newCustomer.Id }, newCustomer);
            }
            catch (Exception ex)
            {
                // Logar exceção detalhada
                Console.WriteLine($"Erro ao adicionar cliente: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        /// <summary>
        /// Atualiza um cliente existente.
        /// </summary>
        /// <param name="id">ID do cliente a ser atualizado</param>
        /// <param name="customerUpdateDto">Dados atualizados do cliente</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerUpdateDto customerUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var customerDto = await _customerService.GetCustomerByIdAsync(id);
                Customer customer = new Customer
                {
                    Id = customerDto.Id,
                    Name = customerDto.Name,
                    Preferences = customerDto.Preferences.Select(p => new Tag { Name = p.Name }).ToList()
                };

                if (customer == null)
                {
                    return NotFound("Cliente não encontrado.");
                }

                customer.Name = customerUpdateDto.Name;
                customer.Preferences = customerUpdateDto.Preferences.Select(p => new Tag { Name = p }).ToList();

                await _customerService.UpdateCustomerAsync(customer);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Logar exceção detalhada
                Console.WriteLine($"Erro ao atualizar o cliente com ID {id}: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        /// <summary>
        /// Remove um cliente pelo ID.
        /// </summary>
        /// <param name="id">ID do cliente a ser removido</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                var existingCustomer = await _customerService.GetCustomerByIdAsync(id);
                if (existingCustomer == null)
                    return NotFound("Cliente não encontrado.");
                
                await _customerService.DeleteCustomerAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Logar exceção detalhada
                Console.WriteLine($"Erro ao remover o cliente com ID {id}: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }
    }
}