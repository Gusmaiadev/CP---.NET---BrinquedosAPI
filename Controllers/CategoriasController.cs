using Microsoft.AspNetCore.Mvc;
using BrinquedosAPI.Data;
using System.Collections.Generic;
using System.Linq;

namespace BrinquedosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/categorias
        [HttpGet]
        public IActionResult Get()
        {
            var categorias = _context.Categorias.ToList();
            return Ok(categorias);
        }

        // GET: api/categorias/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.Id_categoria == id);
            if (categoria == null)
                return NotFound("Categoria não encontrada.");

            return Ok(categoria);
        }

        // POST: api/categorias
        [HttpPost]
        public IActionResult Create([FromBody] Categoria categoria)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = categoria.Id_categoria }, categoria);
        }

        // PUT: api/categorias/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Categoria categoriaAtualizada)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.Id_categoria == id);
            if (categoria == null)
                return NotFound("Categoria não encontrada.");

            categoria.Nome_categoria = categoriaAtualizada.Nome_categoria;

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/categorias/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.Id_categoria == id);
            if (categoria == null)
                return NotFound("Categoria não encontrada.");

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return NoContent();
        }
    }
}