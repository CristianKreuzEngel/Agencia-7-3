using System.Collections.Generic;
using System.Threading.Tasks;
using agencia.Database;
using agencia.DTOs;
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
        public async Task<List<TravelDTO>> GetTravels()
        {
            return await _travelService.GetTravelsAsync();
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
            {
                return NotFound();
            }
            return travel;
        }

        /// <summary>
        /// Adiciona uma nova viagem.
        /// </summary>
        /// <param name="travel">Dados da viagem a ser adicionada</param>
        [HttpPost]
        public async Task<ActionResult<Travel>> AddTravel(Travel travel)
        {
            var newTravel = await _travelService.AddTravelAsync(travel);
            return CreatedAtAction(nameof(GetTravelById), new { id = newTravel.Id }, newTravel);
        }

        /// <summary>
        /// Atualiza uma viagem existente.
        /// </summary>
        /// <param name="id">ID da viagem a ser atualizada</param>
        /// <param name="travel">Dados atualizados da viagem</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTravel(int id, Travel travel)
        {
            if (id != travel.Id)
            {
                return BadRequest();
            }

            await _travelService.UpdateTravelAsync(travel);
            return NoContent();
        }

        /// <summary>
        /// Remove uma viagem pelo ID.
        /// </summary>
        /// <param name="id">ID da viagem a ser removida</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTravel(int id)
        {
            await _travelService.DeleteTravelAsync(id);
            return NoContent();
        }
    }
}
