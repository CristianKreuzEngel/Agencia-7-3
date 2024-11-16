using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using agencia.Classes;
using Travel = agencia.Models.Travel;

namespace agencia.Interfaces;

public interface ITravelReportRepository
{
    Task<List<Travel>> GetTravelsInDateRangeAsync(DateTime startDate, DateTime endDate);
}