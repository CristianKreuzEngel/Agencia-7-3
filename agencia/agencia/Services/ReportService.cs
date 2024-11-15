using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using agencia.Database;
using agencia.DTOs;
using Microsoft.EntityFrameworkCore;

namespace agencia.Services
{
    /// <summary>
    /// Serviço responsável por gerar relatórios.
    /// </summary>
    public class ReportService
    {
        private readonly DbContextMemory _context;

        /// <summary>
        /// Construtor que recebe o contexto do banco de dados.
        /// </summary>
        /// <param name="context">Contexto do banco de dados</param>
        public ReportService(DbContextMemory context)
        {
            _context = context;
        }

        /// <summary>
        /// Gera um relatório de clientes que vão viajar na semana.
        /// </summary>
        /// <returns>Lista de clientes e suas respectivas viagens para a semana</returns>
        public async Task<List<CustomerTravelReportDto>> GetWeeklyTravelReportAsync()
        {
            var startDate = DateTime.UtcNow;
            var endDate = startDate.AddDays(7);

            var travels = await _context.Travels
                .Include(t => t.Customer)
                .Where(t => t.Date >= startDate && t.Date <= endDate)
                .ToListAsync();

            var report = travels.Select(t => new CustomerTravelReportDto
            {
                CustomerName = t.Customer.Name,
                TravelDate = t.Date,
                Destination = t.Destination
            }).ToList();

            return report;
        }
    }

    /// <summary>
    /// DTO para o relatório de clientes que vão viajar na semana.
    /// </summary>
    public class CustomerTravelReportDto
    {
        public string CustomerName { get; set; }
        public DateTime TravelDate { get; set; }
        public string Destination { get; set; }
    }
}