using System.Collections.Generic;
using System.Threading.Tasks;
using agencia.Database;
using agencia.DTOs;
using agencia.Interfaces;
using agencia.Models;
using Microsoft.EntityFrameworkCore;

namespace agencia.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DbContextMemory _context;

        public CustomerRepository(DbContextMemory context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _context.Customers.Include(c => c.Preferences).ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _context.Customers.Include(c => c.Preferences).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
    }
}