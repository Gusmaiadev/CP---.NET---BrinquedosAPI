using Microsoft.AspNetCore.Mvc;
using BrinquedosAPI.Data;
using System.Linq;

namespace BrinquedosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstoquesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EstoquesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/estoques
        [HttpGet]
        public IActionResult Get()
        {
            var estoques = _context.Estoques.ToList();
            return Ok(estoques);
        }

        // GET: api/estoques/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var estoque = _context.Estoques.FirstOrDefault(e => e.Id_estoque == id);
            if (estoque == null)
                return NotFound("Estoque não encontrado.");

            return Ok(estoque);
        }

        // POST: api/estoques
        [HttpPost]
        public IActionResult Create([FromBody] Estoque estoque)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Estoques.Add(estoque);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = estoque.Id_estoque }, estoque);
        }

        // PUT: api/estoques/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Estoque estoqueAtualizado)
        {
            var estoque = _context.Estoques.FirstOrDefault(e => e.Id_estoque == id);
            if (estoque == null)
                return NotFound("Estoque não encontrado.");

            estoque.Quantidade = estoqueAtualizado.Quantidade;

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/estoques/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var estoque = _context.Estoques.FirstOrDefault(e => e.Id_estoque == id);
            if (estoque == null)
                return NotFound("Estoque não encontrado.");

            _context.Estoques.Remove(estoque);
            _context.SaveChanges();

            return NoContent();
        }
    }
}