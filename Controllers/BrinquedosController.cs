using Microsoft.AspNetCore.Mvc;
using BrinquedosAPI.Data;
using System.Collections.Generic;
using System.Linq;

namespace BrinquedosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrinquedosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BrinquedosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/brinquedos
        [HttpGet]
        public IActionResult Get()
        {
            var brinquedos = _context.Brinquedos.ToList();
            return Ok(brinquedos);
        }

        // GET: api/brinquedos/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var brinquedo = _context.Brinquedos.FirstOrDefault(b => b.Id_brinquedo == id);
            if (brinquedo == null)
                return NotFound("Brinquedo não encontrado.");

            return Ok(brinquedo);
        }

        // POST: api/brinquedos
        [HttpPost]
        public IActionResult Create([FromBody] Brinquedo brinquedo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Brinquedos.Add(brinquedo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = brinquedo.Id_brinquedo }, brinquedo);
        }

        // PUT: api/brinquedos/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Brinquedo brinquedoAtualizado)
        {
            var brinquedo = _context.Brinquedos.FirstOrDefault(b => b.Id_brinquedo == id);
            if (brinquedo == null)
                return NotFound("Brinquedo não encontrado.");

            brinquedo.Nome_brinquedo = brinquedoAtualizado.Nome_brinquedo;
            brinquedo.Tipo_brinquedo = brinquedoAtualizado.Tipo_brinquedo;
            brinquedo.Classificacao = brinquedoAtualizado.Classificacao;
            brinquedo.Tamanho = brinquedoAtualizado.Tamanho;
            brinquedo.Preco = brinquedoAtualizado.Preco;

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/brinquedos/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var brinquedo = _context.Brinquedos.FirstOrDefault(b => b.Id_brinquedo == id);
            if (brinquedo == null)
                return NotFound("Brinquedo não encontrado.");

            _context.Brinquedos.Remove(brinquedo);
            _context.SaveChanges();

            return NoContent();
        }
    }
}