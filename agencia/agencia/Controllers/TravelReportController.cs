using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using agencia.Database;
using agencia.DTOs;
using agencia.Services;
using Microsoft.AspNetCore.Mvc;

namespace agencia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TravelReportController : ControllerBase
    {
        private readonly ReportService _reportService;

        public TravelReportController(DbContextMemory context)
        {
            _reportService = new ReportService(context);
        }

        [HttpGet("weekly")]
        public async Task<List<CustomerTravelReportDto>> GetWeeklyTravelReport()
        {
            return await _reportService.GetWeeklyTravelReportAsync();
        }

        [HttpGet("monthly")]
        public async Task<List<CustomerTravelReportDto>> GetMonthlyTravelReport()
        {
            return await _reportService.GetMonthlyTravelReportAsync();
        }

        [HttpGet("yearly")]
        public async Task<List<CustomerTravelReportDto>> GetYearlyTravelReport()
        {
            return await _reportService.GetYearlyTravelReportAsync();
        }

        [HttpGet("custom")]
        public async Task<List<CustomerTravelReportDto>> GetCustomTravelReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return await _reportService.GetCustomTravelReportAsync(startDate, endDate);
        }
    }
}