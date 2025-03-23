using Microsoft.AspNetCore.Mvc;
using BrinquedosAPI.Data;
using BrinquedosAPI.DTOs;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult GetAll()
        {
            // Buscar todos os brinquedos
            var brinquedos = _context.Brinquedos
                .Include(b => b.Categoria) // Incluir a categoria (opcional)
                .Include(b => b.Estoque)   // Incluir o estoque (opcional)
                .ToList();

            // Mapear para o DTO de resposta
            var brinquedosResponse = brinquedos.Select(b => new BrinquedoResponseDTO
            {
                Id_brinquedo = b.Id_brinquedo,
                Nome_brinquedo = b.Nome_brinquedo,
                Tipo_brinquedo = b.Tipo_brinquedo,
                Classificacao = b.Classificacao,
                Tamanho = b.Tamanho,
                Preco = b.Preco,
                Id_categoria = b.Id_categoria,
                Id_estoque = b.Id_estoque
            }).ToList();

            return Ok(brinquedosResponse);
        }

        // GET: api/brinquedos/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Buscar o brinquedo pelo ID
            var brinquedo = _context.Brinquedos
                .Include(b => b.Categoria) // Incluir a categoria (opcional)
                .Include(b => b.Estoque)   // Incluir o estoque (opcional)
                .FirstOrDefault(b => b.Id_brinquedo == id);

            if (brinquedo == null)
                return NotFound("Brinquedo não encontrado.");

            // Mapear para o DTO de resposta
            var brinquedoResponse = new BrinquedoResponseDTO
            {
                Id_brinquedo = brinquedo.Id_brinquedo,
                Nome_brinquedo = brinquedo.Nome_brinquedo,
                Tipo_brinquedo = brinquedo.Tipo_brinquedo,
                Classificacao = brinquedo.Classificacao,
                Tamanho = brinquedo.Tamanho,
                Preco = brinquedo.Preco,
                Id_categoria = brinquedo.Id_categoria,
                Id_estoque = brinquedo.Id_estoque
            };

            return Ok(brinquedoResponse);
        }

        // POST: api/brinquedos
        [HttpPost]
        public IActionResult Create([FromBody] BrinquedoDTO brinquedoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verificar se a categoria existe (se fornecida)
            if (brinquedoDTO.Id_categoria.HasValue)
            {
                var categoria = _context.Categorias.FirstOrDefault(c => c.Id_categoria == brinquedoDTO.Id_categoria);
                if (categoria == null)
                    return BadRequest("Categoria não encontrada.");
            }

            // Verificar se o estoque existe (se fornecido)
            if (brinquedoDTO.Id_estoque.HasValue)
            {
                var estoque = _context.Estoques.FirstOrDefault(e => e.Id_estoque == brinquedoDTO.Id_estoque);
                if (estoque == null)
                    return BadRequest("Estoque não encontrado.");
            }

            // Criar o brinquedo
            var brinquedo = new Brinquedo
            {
                Nome_brinquedo = brinquedoDTO.Nome_brinquedo,
                Tipo_brinquedo = brinquedoDTO.Tipo_brinquedo,
                Classificacao = brinquedoDTO.Classificacao,
                Tamanho = brinquedoDTO.Tamanho,
                Preco = brinquedoDTO.Preco,
                Id_categoria = brinquedoDTO.Id_categoria,
                Id_estoque = brinquedoDTO.Id_estoque
            };

            _context.Brinquedos.Add(brinquedo);
            _context.SaveChanges();

            // Mapear para o DTO de resposta
            var brinquedoResponse = new BrinquedoResponseDTO
            {
                Id_brinquedo = brinquedo.Id_brinquedo,
                Nome_brinquedo = brinquedo.Nome_brinquedo,
                Tipo_brinquedo = brinquedo.Tipo_brinquedo,
                Classificacao = brinquedo.Classificacao,
                Tamanho = brinquedo.Tamanho,
                Preco = brinquedo.Preco,
                Id_categoria = brinquedo.Id_categoria,
                Id_estoque = brinquedo.Id_estoque
            };

            // Retornar o endpoint GetById com o ID do brinquedo criado
            return CreatedAtAction(nameof(GetById), new { id = brinquedo.Id_brinquedo }, brinquedoResponse);
        }

        // PUT: api/brinquedos/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] BrinquedoDTO brinquedoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Buscar o brinquedo pelo ID
            var brinquedo = _context.Brinquedos.FirstOrDefault(b => b.Id_brinquedo == id);
            if (brinquedo == null)
                return NotFound("Brinquedo não encontrado.");

            // Verificar se a categoria existe (se fornecida)
            if (brinquedoDTO.Id_categoria.HasValue)
            {
                var categoria = _context.Categorias.FirstOrDefault(c => c.Id_categoria == brinquedoDTO.Id_categoria);
                if (categoria == null)
                    return BadRequest("Categoria não encontrada.");
            }

            // Verificar se o estoque existe (se fornecido)
            if (brinquedoDTO.Id_estoque.HasValue)
            {
                var estoque = _context.Estoques.FirstOrDefault(e => e.Id_estoque == brinquedoDTO.Id_estoque);
                if (estoque == null)
                    return BadRequest("Estoque não encontrado.");
            }

            // Atualizar os dados do brinquedo
            brinquedo.Nome_brinquedo = brinquedoDTO.Nome_brinquedo;
            brinquedo.Tipo_brinquedo = brinquedoDTO.Tipo_brinquedo;
            brinquedo.Classificacao = brinquedoDTO.Classificacao;
            brinquedo.Tamanho = brinquedoDTO.Tamanho;
            brinquedo.Preco = brinquedoDTO.Preco;
            brinquedo.Id_categoria = brinquedoDTO.Id_categoria;
            brinquedo.Id_estoque = brinquedoDTO.Id_estoque;

            _context.Brinquedos.Update(brinquedo);
            _context.SaveChanges();

            // Mapear para o DTO de resposta
            var brinquedoResponse = new BrinquedoResponseDTO
            {
                Id_brinquedo = brinquedo.Id_brinquedo,
                Nome_brinquedo = brinquedo.Nome_brinquedo,
                Tipo_brinquedo = brinquedo.Tipo_brinquedo,
                Classificacao = brinquedo.Classificacao,
                Tamanho = brinquedo.Tamanho,
                Preco = brinquedo.Preco,
                Id_categoria = brinquedo.Id_categoria,
                Id_estoque = brinquedo.Id_estoque
            };

            return Ok(brinquedoResponse);
        }

        // DELETE: api/brinquedos/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Buscar o brinquedo pelo ID
            var brinquedo = _context.Brinquedos.FirstOrDefault(b => b.Id_brinquedo == id);
            if (brinquedo == null)
                return NotFound("Brinquedo não encontrado.");

            // Remover o brinquedo
            _context.Brinquedos.Remove(brinquedo);
            _context.SaveChanges();

            return NoContent(); // Retorna 204 No Content
        }
    }
}