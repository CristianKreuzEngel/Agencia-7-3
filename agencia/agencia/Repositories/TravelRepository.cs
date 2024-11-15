using System.Collections.Generic;
using System.Threading.Tasks;
using agencia.Database;
using agencia.DTOs;
using agencia.Interfaces;
using agencia.Models;
using Microsoft.EntityFrameworkCore;

namespace agencia.Repositories
{
    public class TravelRepository : ITravelRepository
    {
        private readonly DbContextMemory _context;

        public TravelRepository(DbContextMemory context)
        {
            _context = context;
        }

        public async Task<List<Travel>> GetAllAsync()
        {
            return await _context.Travels.Include(t => t.Customer).ToListAsync();
        }

        public async Task<Travel> GetByIdAsync(int id)
        {
            return await _context.Travels.Include(t => t.Customer).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Travel> AddAsync(Travel travel)
        {
            _context.Travels.Add(travel);
            await _context.SaveChangesAsync();
            return travel;
        }

        public async Task UpdateAsync(Travel travel)
        {
            _context.Travels.Update(travel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var travel = await _context.Travels.FindAsync(id);
            if (travel != null)
            {
                _context.Travels.Remove(travel);
                await _context.SaveChangesAsync();
            }
        }
    }
}