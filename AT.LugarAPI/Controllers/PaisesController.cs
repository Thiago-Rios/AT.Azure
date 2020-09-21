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
    [Route("api/[controller]")]
    [ApiController]
    public class PaisesController : ControllerBase
    {
        private readonly ProjetoContext _context;

        public PaisesController(ProjetoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pais>>> GetPaises()
        {
            return await _context.Paises.Include(x => x.Estados).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pais>> GetPais(int id)
        {
            var pais = await _context.Paises.Include(x => x.Estados).FirstOrDefaultAsync(x => x.Id == id);

            if (pais == null)
            {
                return NotFound();
            }

            return pais;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPais(int id, Pais paisEditado)
        {
            var pais = _context.Paises.Find(id);

            pais.Nome = paisEditado.Nome;
            pais.Bandeira = paisEditado.Bandeira;

            _context.Paises.Update(pais);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaisExists(id))
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
        public async Task<ActionResult<Pais>> PostPais(Pais pais)
        {
            _context.Paises.Add(pais);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPais", new { id = pais.Id }, pais);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Pais>> DeletePais(int id)
        {
            var pais = await _context.Paises.FirstOrDefaultAsync(x => x.Id == id);

            if (pais == null)
            {
                return NotFound();
            }


            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Paises.Remove(pais);
                    await _context.SaveChangesAsync();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }

            return NoContent();
        }

        private bool PaisExists(int id)
        {
            return _context.Paises.Any(e => e.Id == id);
        }
    }
}
