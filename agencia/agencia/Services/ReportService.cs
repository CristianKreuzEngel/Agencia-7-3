using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using agencia.Database;
using agencia.DTOs;
using agencia.Interfaces;
using agencia.Repositories;

namespace agencia.Services
{
    public class ReportService
    {
        private readonly ITravelReportRepository _travelRepository;

        public ReportService(DbContextMemory travelRepository)
        {
            _travelRepository = new TravelReportRepository(travelRepository);
        }

        public async Task<List<CustomerTravelReportDto>> GetWeeklyTravelReportAsync()
        {
            var startDate = DateTime.UtcNow;
            var endDate = startDate.AddDays(7);

            return await GenerateTravelReportAsync(startDate, endDate);
        }

        public async Task<List<CustomerTravelReportDto>> GetMonthlyTravelReportAsync()
        {
            var startDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            return await GenerateTravelReportAsync(startDate, endDate);
        }

        public async Task<List<CustomerTravelReportDto>> GetYearlyTravelReportAsync()
        {
            var startDate = new DateTime(DateTime.UtcNow.Year, 1, 1);
            var endDate = new DateTime(DateTime.UtcNow.Year, 12, 31);

            return await GenerateTravelReportAsync(startDate, endDate);
        }

        public async Task<List<CustomerTravelReportDto>> GetCustomTravelReportAsync(DateTime startDate, DateTime endDate)
        {
            return await GenerateTravelReportAsync(startDate, endDate);
        }

        private async Task<List<CustomerTravelReportDto>> GenerateTravelReportAsync(DateTime startDate, DateTime endDate)
        {
            var travels = await _travelRepository.GetTravelsInDateRangeAsync(startDate, endDate);

            var report = travels.Select(t => new CustomerTravelReportDto
            {
                CustomerName = t.Customer.Name,
                TravelDate = t.Date,
                Destination = t.Destination
            }).ToList();

            return report;
        }
    }
}