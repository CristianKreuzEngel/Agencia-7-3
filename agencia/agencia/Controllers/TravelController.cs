using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using agencia.Database;
using agencia.DTOs;
using agencia.Models;
using Microsoft.AspNetCore.Mvc;
using agencia.Models;
using agencia.Services;

namespace agencia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TravelController : ControllerBase
    {
        private readonly TravelService _travelService;

        public TravelController(DbContextMemory context)
        {
            _travelService = new TravelService(context);
        }

        /// <summary>
        /// Retorna a lista de todas as viagens.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<TravelDTO>>> GetTravels()
        {
            return Ok(await _travelService.GetTravelsAsync());
        }

        /// <summary>
        /// Retorna uma viagem específica pelo ID.
        /// </summary>
        /// <param name="id">ID da viagem</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<TravelDTO>> GetTravelById(int id)
        {
            var travel = await _travelService.GetTravelByIdAsync(id);
            if (travel == null)
                return NotFound("Viagem não encontrada.");
            
            return Ok(travel);
        }

        /// <summary>
        /// Adiciona uma nova viagem.
        /// </summary>
        /// <param name="travelCreateDto">Dados da viagem a ser adicionada</param>
        [HttpPost]
        public async Task<ActionResult<TravelDTO>> AddTravel([FromBody] TravelCreateDto travelCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                var travel = new Travel
                {
                    Date = travelCreateDto.Date,
                    Destination = travelCreateDto.Destination,
                    Customer = new Customer { Id = travelCreateDto.CustomerId }
                };

                var newTravel = await _travelService.AddTravelAsync(travel);
                return CreatedAtAction(nameof(GetTravelById), new { id = newTravel.Id }, newTravel);
            }
            catch (Exception ex)
            {
                // Logar exceção detalhada
                Console.WriteLine($"Erro ao adicionar viagem: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        /// <summary>
        /// Atualiza uma viagem existente.
        /// </summary>
        /// <param name="id">ID da viagem a ser atualizada</param>
        /// <param name="travelUpdateDto">Dados atualizados da viagem</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTravel(int id, [FromBody] TravelCreateDto travelUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                TravelDTO travelDTO = await _travelService.GetTravelByIdAsync(id);
                Travel travel = new Travel
                {
                    Id = travelDTO.Id,
                    Date = travelDTO.Date,
                    Destination = travelDTO.Destination,
                    Customer = new Customer { Id = travelDTO.CustomerId }
                };

                if (travel == null)
                {
                    return NotFound("Viagem não encontrada.");
                }

                travel.Date = travelUpdateDto.Date;
                travel.Destination = travelUpdateDto.Destination;
                travel.Customer = new Customer { Id = travelUpdateDto.CustomerId };

                await _travelService.UpdateTravelAsync(travel);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Logar exceção detalhada
                Console.WriteLine($"Erro ao atualizar a viagem com ID {id}: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        /// <summary>
        /// Remove uma viagem pelo ID.
        /// </summary>
        /// <param name="id">ID da viagem a ser removida</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTravel(int id)
        {
            try
            {
                var existingTravel = await _travelService.GetTravelByIdAsync(id);
                if (existingTravel == null)
                    return NotFound("Viagem não encontrada.");
                
                await _travelService.DeleteTravelAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Logar exceção detalhada
                Console.WriteLine($"Erro ao remover a viagem com ID {id}: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }
    }
}