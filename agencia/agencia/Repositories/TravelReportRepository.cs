using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using agencia.Database;
using agencia.Interfaces;
using agencia.Models;
using Microsoft.EntityFrameworkCore;
using Travel = agencia.Classes.Travel;

namespace agencia.Repositories
{
    public class TravelReportRepository : ITravelReportRepository
    {
        private readonly DbContextMemory _context;

        public TravelReportRepository(DbContextMemory context)
        {
            _context = context;
        }

        public async Task<List<Models.Travel>> GetTravelsInDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Travels
                .Include(t => t.Customer)
                .Where(t => t.Date >= startDate && t.Date <= endDate)
                .ToListAsync();
        }
    }
}