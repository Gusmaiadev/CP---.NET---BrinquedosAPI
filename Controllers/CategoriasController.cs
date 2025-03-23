using Microsoft.AspNetCore.Mvc;
using BrinquedosAPI.Data;
using BrinquedosAPI.DTOs;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult GetAll()
        {
            // Buscar todas as categorias
            var categorias = _context.Categorias.ToList();

            // Mapear para o DTO de resposta
            var categoriasResponse = categorias.Select(c => new CategoriaResponseDTO
            {
                Id_categoria = c.Id_categoria,
                Nome_categoria = c.Nome_categoria
            }).ToList();

            return Ok(categoriasResponse);
        }

        // GET: api/categorias/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Buscar a categoria pelo ID
            var categoria = _context.Categorias.FirstOrDefault(c => c.Id_categoria == id);
            if (categoria == null)
                return NotFound("Categoria não encontrada.");

            // Mapear para o DTO de resposta
            var categoriaResponse = new CategoriaResponseDTO
            {
                Id_categoria = categoria.Id_categoria,
                Nome_categoria = categoria.Nome_categoria
            };

            return Ok(categoriaResponse);
        }

        // POST: api/categorias
        [HttpPost]
        public IActionResult Create([FromBody] CategoriaDTO categoriaDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Criar a categoria
            var categoria = new Categoria
            {
                Nome_categoria = categoriaDTO.Nome_categoria
            };

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            // Mapear para o DTO de resposta
            var categoriaResponse = new CategoriaResponseDTO
            {
                Id_categoria = categoria.Id_categoria,
                Nome_categoria = categoria.Nome_categoria
            };

            // Retornar o endpoint GetById com o ID da categoria criada
            return CreatedAtAction(nameof(GetById), new { id = categoria.Id_categoria }, categoriaResponse);
        }

        // PUT: api/categorias/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CategoriaDTO categoriaDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Buscar a categoria pelo ID
            var categoria = _context.Categorias.FirstOrDefault(c => c.Id_categoria == id);
            if (categoria == null)
                return NotFound("Categoria não encontrada.");

            // Atualizar os dados da categoria
            categoria.Nome_categoria = categoriaDTO.Nome_categoria;

            _context.Categorias.Update(categoria);
            _context.SaveChanges();

            // Mapear para o DTO de resposta
            var categoriaResponse = new CategoriaResponseDTO
            {
                Id_categoria = categoria.Id_categoria,
                Nome_categoria = categoria.Nome_categoria
            };

            return Ok(categoriaResponse);
        }

        // DELETE: api/categorias/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Buscar a categoria pelo ID
            var categoria = _context.Categorias.FirstOrDefault(c => c.Id_categoria == id);
            if (categoria == null)
                return NotFound("Categoria não encontrada.");

            // Remover a categoria
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return NoContent(); // Retorna 204 No Content
        }
    }
}