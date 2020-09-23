using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Repository.Context;

namespace AT.WebAPI.Controllers
{
    [Route("api/estados")]
    [ApiController]
    public class EstadosController : ControllerBase
    {
        private readonly ProjetoContext _context;

        public EstadosController(ProjetoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estado>>> GetEstados()
        {
            return await _context.Estados.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Estado>> GetEstado(int id)
        {
            var estado = await _context.Estados.FindAsync(id);

            if (estado == null)
            {
                return NotFound();
            }

            return estado;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstado(int id, Estado estadoEditado)
        {
            var estado = _context.Estados.Find(id);

            estado.Nome = estadoEditado.Nome;
            estado.Bandeira = estadoEditado.Bandeira;

            _context.Estados.Update(estado);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Estado>> PostEstado(EstadoResponse estadoResponse)
        {
            var pais = await _context.Paises.FirstOrDefaultAsync(x => x.Id == estadoResponse.Pais.Id);
            estadoResponse.Pais = pais;

            Estado estado = new Estado { Nome = estadoResponse.Nome, Bandeira = estadoResponse.Bandeira, Pais = estadoResponse.Pais};

            _context.Estados.Add(estado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstado", new { id = estado.Id }, estado);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Estado>> DeleteEstado(int id)
        {
            var estado = await _context.Estados.FindAsync(id);
            if (estado == null)
            {
                return NotFound();
            }

            _context.Estados.Remove(estado);
            await _context.SaveChangesAsync();

            return estado;
        }

        private bool EstadoExists(int id)
        {
            return _context.Estados.Any(e => e.Id == id);
        }
    }
}
