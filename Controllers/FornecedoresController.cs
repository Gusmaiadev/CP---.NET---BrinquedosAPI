using Microsoft.AspNetCore.Mvc;
using BrinquedosAPI.Data;
using BrinquedosAPI.DTOs;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BrinquedosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedoresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FornecedoresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/fornecedores
        [HttpGet]
        public IActionResult GetAll()
        {
            var fornecedores = _context.Fornecedores.ToList();
            var fornecedoresResponse = fornecedores.Select(f => new FornecedorResponseDTO
            {
                Id_fornecedor = f.Id_fornecedor,
                Nome_fornecedor = f.Nome_fornecedor,
                Nome_representante = f.Nome_representante,
                CNPJ = f.CNPJ,
                Telefone = f.Telefone
            }).ToList();

            return Ok(fornecedoresResponse);
        }

        // GET: api/fornecedores/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var fornecedor = _context.Fornecedores.Find(id);
            if (fornecedor == null)
                return NotFound("Fornecedor não encontrado.");

            var fornecedorResponse = new FornecedorResponseDTO
            {
                Id_fornecedor = fornecedor.Id_fornecedor,
                Nome_fornecedor = fornecedor.Nome_fornecedor,
                Nome_representante = fornecedor.Nome_representante,
                CNPJ = fornecedor.CNPJ,
                Telefone = fornecedor.Telefone
            };

            return Ok(fornecedorResponse);
        }

        // POST: api/fornecedores
        [HttpPost]
        public IActionResult Create([FromBody] FornecedorDTO fornecedorDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verificar se já existe um fornecedor com este CNPJ
            var fornecedorExistente = _context.Fornecedores
                .FirstOrDefault(f => f.CNPJ == fornecedorDTO.CNPJ);

            if (fornecedorExistente != null)
            {
                return Conflict(new
                {
                    message = "Já existe um fornecedor cadastrado com este CNPJ.",
                    fornecedorExistente = new FornecedorResponseDTO
                    {
                        Id_fornecedor = fornecedorExistente.Id_fornecedor,
                        Nome_fornecedor = fornecedorExistente.Nome_fornecedor,
                        Nome_representante = fornecedorExistente.Nome_representante,
                        CNPJ = fornecedorExistente.CNPJ,
                        Telefone = fornecedorExistente.Telefone
                    }
                });
            }

            var fornecedor = new Fornecedor
            {
                Nome_fornecedor = fornecedorDTO.Nome_fornecedor,
                Nome_representante = fornecedorDTO.Nome_representante,
                CNPJ = fornecedorDTO.CNPJ,
                Telefone = fornecedorDTO.Telefone
            };

            _context.Fornecedores.Add(fornecedor);
            _context.SaveChanges();

            var fornecedorResponse = new FornecedorResponseDTO
            {
                Id_fornecedor = fornecedor.Id_fornecedor,
                Nome_fornecedor = fornecedor.Nome_fornecedor,
                Nome_representante = fornecedor.Nome_representante,
                CNPJ = fornecedor.CNPJ,
                Telefone = fornecedor.Telefone
            };

            return CreatedAtAction(nameof(GetById), new { id = fornecedor.Id_fornecedor }, fornecedorResponse);
        }

        // PUT: api/fornecedores/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] FornecedorDTO fornecedorDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var fornecedor = _context.Fornecedores.Find(id);
            if (fornecedor == null)
                return NotFound("Fornecedor não encontrado.");

            // Verificar se o novo CNPJ já pertence a outro fornecedor
            if (fornecedor.CNPJ != fornecedorDTO.CNPJ)
            {
                var cnpjExistente = _context.Fornecedores
                    .Any(f => f.CNPJ == fornecedorDTO.CNPJ && f.Id_fornecedor != id);

                if (cnpjExistente)
                    return Conflict("Já existe outro fornecedor com este CNPJ.");
            }

            fornecedor.Nome_fornecedor = fornecedorDTO.Nome_fornecedor;
            fornecedor.Nome_representante = fornecedorDTO.Nome_representante;
            fornecedor.CNPJ = fornecedorDTO.CNPJ;
            fornecedor.Telefone = fornecedorDTO.Telefone;

            _context.Fornecedores.Update(fornecedor);
            _context.SaveChanges();

            var fornecedorResponse = new FornecedorResponseDTO
            {
                Id_fornecedor = fornecedor.Id_fornecedor,
                Nome_fornecedor = fornecedor.Nome_fornecedor,
                Nome_representante = fornecedor.Nome_representante,
                CNPJ = fornecedor.CNPJ,
                Telefone = fornecedor.Telefone
            };

            return Ok(fornecedorResponse);
        }

        // DELETE: api/fornecedores/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var fornecedor = _context.Fornecedores.Find(id);
            if (fornecedor == null)
                return NotFound("Fornecedor não encontrado.");

            _context.Fornecedores.Remove(fornecedor);
            _context.SaveChanges();

            return NoContent();
        }
    }
}