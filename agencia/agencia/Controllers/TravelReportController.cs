using System.Collections.Generic;
using System.Threading.Tasks;
using agencia.Database;
using agencia.Services;
using Microsoft.AspNetCore.Mvc;

namespace agencia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TravelReportController : ControllerBase
    {
        private readonly ReportService _reportService;

        /// <summary>
        /// Construtor que recebe o contexto do banco de dados.
        /// </summary>
        /// <param name="context">Contexto do banco de dados</param>
        public TravelReportController(DbContextMemory context)
        {
            _reportService = new ReportService(context);
        }

        /// <summary>
        /// Gera um relatório de clientes que vão viajar na semana.
        /// </summary>
        [HttpGet]
        public async Task<List<CustomerTravelReportDto>> GetWeeklyTravelReport()
        {
            return await _reportService.GetWeeklyTravelReportAsync();
        }
    }
}