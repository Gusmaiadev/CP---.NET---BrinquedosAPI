using Microsoft.AspNetCore.Mvc;
using BrinquedosAPI.Data;
using BrinquedosAPI.DTOs;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult GetAll()
        {
            // Buscar todos os estoques
            var estoques = _context.Estoques.ToList();

            // Mapear para o DTO de resposta
            var estoquesResponse = estoques.Select(e => new EstoqueResponseDTO
            {
                Id_estoque = e.Id_estoque,
                Quantidade = e.Quantidade,
                Faixa = e.Faixa
            }).ToList();

            return Ok(estoquesResponse);
        }

        // GET: api/estoques/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Buscar o estoque pelo ID
            var estoque = _context.Estoques.FirstOrDefault(e => e.Id_estoque == id);
            if (estoque == null)
                return NotFound("Estoque não encontrado.");

            // Mapear para o DTO de resposta
            var estoqueResponse = new EstoqueResponseDTO
            {
                Id_estoque = estoque.Id_estoque,
                Quantidade = estoque.Quantidade,
                Faixa = estoque.Faixa
            };

            return Ok(estoqueResponse);
        }

        // POST: api/estoques
        [HttpPost]
        public IActionResult Create([FromBody] EstoqueDTO estoqueDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Criar o estoque
            var estoque = new Estoque
            {
                Quantidade = estoqueDTO.Quantidade,
                Faixa = estoqueDTO.Faixa
            };

            _context.Estoques.Add(estoque);
            _context.SaveChanges();

            // Mapear para o DTO de resposta
            var estoqueResponse = new EstoqueResponseDTO
            {
                Id_estoque = estoque.Id_estoque,
                Quantidade = estoque.Quantidade,
                Faixa = estoque.Faixa
            };

            // Retornar o endpoint GetById com o ID do estoque criado
            return CreatedAtAction(nameof(GetById), new { id = estoque.Id_estoque }, estoqueResponse);
        }

        // PUT: api/estoques/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] EstoqueDTO estoqueDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Buscar o estoque pelo ID
            var estoque = _context.Estoques.FirstOrDefault(e => e.Id_estoque == id);
            if (estoque == null)
                return NotFound("Estoque não encontrado.");

            // Atualizar os dados do estoque
            estoque.Quantidade = estoqueDTO.Quantidade;
            estoque.Faixa = estoqueDTO.Faixa;

            _context.Estoques.Update(estoque);
            _context.SaveChanges();

            // Mapear para o DTO de resposta
            var estoqueResponse = new EstoqueResponseDTO
            {
                Id_estoque = estoque.Id_estoque,
                Quantidade = estoque.Quantidade,
                Faixa = estoque.Faixa
            };

            return Ok(estoqueResponse);
        }

        // DELETE: api/estoques/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Buscar o estoque pelo ID
            var estoque = _context.Estoques.FirstOrDefault(e => e.Id_estoque == id);
            if (estoque == null)
                return NotFound("Estoque não encontrado.");

            // Remover o estoque
            _context.Estoques.Remove(estoque);
            _context.SaveChanges();

            return NoContent(); // Retorna 204 No Content
        }
    }
}