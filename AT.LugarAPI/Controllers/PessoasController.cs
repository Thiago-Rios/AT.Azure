using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Repository.Context;

namespace Assessment.API.Controllers
{
    [Route("api/pessoas")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly ProjetoContext _context;

        public AutoresController(ProjetoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pessoa>>> GetAutores()
        {
            return await _context.Pessoas.Include(x => x.Pais).Include(x => x.Estado).Include(x => x.Amigos).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa>> GetPessoa(int id)
        {
            var pessoa = await _context.Pessoas.Include(x => x.Amigos).Include(x => x.Pais).Include(x => x.Estado).FirstOrDefaultAsync(x => x.Id == id);

            if (pessoa == null)
            {
                return NotFound();
            }

            return pessoa;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPessoa([FromRoute] int id, [FromBody] Pessoa pessoaEditada)
        {
            var pessoa = _context.Pessoas.Find(id);

            pessoa.Nome = pessoaEditada.Nome;
            pessoa.Foto = pessoaEditada.Foto;
            pessoa.Email = pessoaEditada.Email;
            pessoa.Telefone = pessoaEditada.Telefone;
            pessoa.DataDeNascimento = pessoaEditada.DataDeNascimento;
            pessoa.Pais = pessoaEditada.Pais;
            pessoa.Estado = pessoaEditada.Estado;

            _context.Pessoas.Update(pessoa);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaExists(id))
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
        public async Task<ActionResult<Pessoa>> PostPessoa(PessoaResponse response)
        {
            var paisOrigem = await _context.Paises.FirstOrDefaultAsync(x => x.Id == response.Pais.Id);
            var estadoOrigem = await _context.Estados.FirstOrDefaultAsync(x => x.Id == response.Estado.Id);
            response.Pais = paisOrigem;
            response.Estado = estadoOrigem;

            Pessoa novaPessoa = new Pessoa { Nome = response.Nome, Foto = response.Foto, Email = response.Email, Telefone = response.Telefone, DataDeNascimento = response.DataDeNascimento, Pais = response.Pais,
                Estado = response.Estado};

            _context.Pessoas.Add(novaPessoa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPessoa", new { id = novaPessoa.Id }, novaPessoa);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Pessoa>> DeletePessoa(int id)
        {
            var pessoa = await _context.Pessoas.Include(x => x.Amigos).FirstOrDefaultAsync(x => x.Id == id);

            if (pessoa == null)
            {
                return NotFound();
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in pessoa.Amigos)
                    {
                        _context.Amigos.Remove(item);
                    }

                    _context.Pessoas.Remove(pessoa);
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

        private bool PessoaExists(int id)
        {
            return _context.Pessoas.Any(e => e.Id == id);
        }
    }
}