using System.Collections.Generic;
using System.Threading.Tasks;
using agencia.DTOs;
using agencia.Models;

namespace agencia.Interfaces
{
    public interface ITravelRepository
    {
        Task<List<Travel>> GetAllAsync();
        Task<Travel> GetByIdAsync(int id);
        Task<Travel> AddAsync(Travel travel);
        Task UpdateAsync(Travel travel);
        Task DeleteAsync(int id);
    }
}