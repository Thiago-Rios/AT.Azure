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
    public class AmigosController : ControllerBase
    {
        private readonly ProjetoContext _context;

        public AmigosController(ProjetoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Amigo>>> GetAmigos()
        {
            return await _context.Amigos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Amigo>> GetAmigo(int id)
        {
            var amigo = await _context.Amigos.FindAsync(id);

            if (amigo == null)
            {
                return NotFound();
            }

            return amigo;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmigo(int id, Amigo amigoEditado)
        {
            var amigo = _context.Amigos.Find(id);

            amigo.Nome = amigoEditado.Nome;
            amigo.Email = amigoEditado.Email;
            amigo.Telefone = amigoEditado.Telefone;

            _context.Amigos.Update(amigo);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AmigoExists(id))
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
        public async Task<ActionResult<Amigo>> PostAmigo(AmigoResponse amigo)
        {
            var pessoa = await _context.Pessoas.FirstOrDefaultAsync(x => x.Id == amigo.AmigoPessoa.Id);
            amigo.AmigoPessoa = pessoa;

            Amigo novoAmigo = new Amigo { Nome = amigo.Nome, Email = amigo.Email, Telefone = amigo.Telefone, AmigoPessoa = amigo.AmigoPessoa };

            _context.Amigos.Add(novoAmigo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAmigo", new { id = novoAmigo.Id }, novoAmigo);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Amigo>> DeleteAmigo(int id)
        {
            var amigo = await _context.Amigos.FindAsync(id);
            if (amigo == null)
            {
                return NotFound();
            }

            _context.Amigos.Remove(amigo);
            await _context.SaveChangesAsync();

            return amigo;
        }

        private bool AmigoExists(int id)
        {
            return _context.Amigos.Any(e => e.Id == id);
        }
    }
}
