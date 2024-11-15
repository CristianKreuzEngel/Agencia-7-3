using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using agencia.Database;
using agencia.DTOs;
using agencia.Models;
using agencia.Repositories;

namespace agencia.Services
{
    /// <summary>
    /// Serviço responsável pelas operações relacionadas a viagens.
    /// </summary>
    public class TravelService
    {
        private readonly TravelRepository _travelRepository;

        /// <summary>
        /// Construtor que recebe o contexto do banco de dados.
        /// </summary>
        /// <param name="context">Contexto do banco de dados</param>
        public TravelService(DbContextMemory context)
        {
            _travelRepository = new TravelRepository(context);
        }

        /// <summary>
        /// Obtém a lista de todas as viagens.
        /// </summary>
        /// <returns>Lista de viagens</returns>
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

        /// <summary>
        /// Obtém uma viagem por seu ID.
        /// </summary>
        /// <param name="id">ID da viagem</param>
        /// <returns>Dados da viagem</returns>
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

        /// <summary>
        /// Adiciona uma nova viagem.
        /// </summary>
        /// <param name="travel">Dados da viagem</param>
        /// <returns>Dados da viagem adicionada</returns>
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

        /// <summary>
        /// Atualiza uma viagem existente.
        /// </summary>
        /// <param name="travel">Dados da viagem</param>
        public async Task UpdateTravelAsync(Travel travel)
        {
            await _travelRepository.UpdateAsync(travel);
        }

        /// <summary>
        /// Remove uma viagem.
        /// </summary>
        /// <param name="id">ID da viagem</param>
        public async Task DeleteTravelAsync(int id)
        {
            await _travelRepository.DeleteAsync(id);
        }
    }
}