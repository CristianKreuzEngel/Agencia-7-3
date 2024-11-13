using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using agencia.DTOs;
using agencia.Interfaces;
using agencia.Models;

namespace agencia.Services
{
    public class TravelService
    {
        private readonly ITravelRepository _travelRepository;

        public TravelService(ITravelRepository travelRepository)
        {
            _travelRepository = travelRepository;
        }

        public async Task<List<TravelDTO>> GetTravelsAsync()
        {
            var travels = await _travelRepository.GetAllAsync();
            return travels.Select(t => new TravelDTO
            {
                Id = t.Id,
                Date = t.Date,
                Destination = t.Destination,
                CustomerId = t.Customer.Id
            }).ToList();
        }

        public async Task<TravelDTO> GetTravelByIdAsync(int id)
        {
            var travel = await _travelRepository.GetByIdAsync(id);
            if (travel == null) return null;

            return new TravelDTO
            {
                Id = travel.Id,
                Date = travel.Date,
                Destination = travel.Destination,
                CustomerId = travel.Customer.Id
            };
        }

        public async Task<TravelDTO> AddTravelAsync(Travel travel)
        {
            var newTravel = await _travelRepository.AddAsync(travel);
            return new TravelDTO
            {
                Id = newTravel.Id,
                Date = newTravel.Date,
                Destination = newTravel.Destination,
                CustomerId = newTravel.Customer.Id
            };
        }

        public async Task UpdateTravelAsync(Travel travel)
        {
            await _travelRepository.UpdateAsync(travel);
        }

        public async Task DeleteTravelAsync(int id)
        {
            await _travelRepository.DeleteAsync(id);
        }
    }
}