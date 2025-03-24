using Microsoft.AspNetCore.Mvc;
using BrinquedosAPI.Data;
using BrinquedosAPI.DTOs;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
            var brinquedos = _context.Brinquedos
                .Include(b => b.Categoria)
                .Include(b => b.Estoque)
                .Include(b => b.BrinquedoFornecedores)
                    .ThenInclude(bf => bf.Fornecedor)
                .AsNoTracking()
                .ToList();

            var brinquedosResponse = brinquedos.Select(b => new BrinquedoResponseDTO
            {
                Id_brinquedo = b.Id_brinquedo,
                Nome_brinquedo = b.Nome_brinquedo,
                Tipo_brinquedo = b.Tipo_brinquedo,
                Classificacao = b.Classificacao,
                Tamanho = b.Tamanho,
                Preco = b.Preco,
                Id_categoria = b.Id_categoria,
                Id_estoque = b.Id_estoque,
                Fornecedores = b.BrinquedoFornecedores?
                    .Select(bf => new FornecedorResponseDTO
                    {
                        Id_fornecedor = bf.Fornecedor.Id_fornecedor,
                        Nome_fornecedor = bf.Fornecedor.Nome_fornecedor,
                        Nome_representante = bf.Fornecedor.Nome_representante,
                        CNPJ = bf.Fornecedor.CNPJ,
                        Telefone = bf.Fornecedor.Telefone
                    }).ToList() ?? new List<FornecedorResponseDTO>()
            }).ToList();

            return Ok(brinquedosResponse);
        }

        // GET: api/brinquedos/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var brinquedo = _context.Brinquedos
                .Include(b => b.Categoria)
                .Include(b => b.Estoque)
                .Include(b => b.BrinquedoFornecedores)
                    .ThenInclude(bf => bf.Fornecedor)
                .AsNoTracking()
                .FirstOrDefault(b => b.Id_brinquedo == id);

            if (brinquedo == null)
                return NotFound("Brinquedo não encontrado.");

            var brinquedoResponse = new BrinquedoResponseDTO
            {
                Id_brinquedo = brinquedo.Id_brinquedo,
                Nome_brinquedo = brinquedo.Nome_brinquedo,
                Tipo_brinquedo = brinquedo.Tipo_brinquedo,
                Classificacao = brinquedo.Classificacao,
                Tamanho = brinquedo.Tamanho,
                Preco = brinquedo.Preco,
                Id_categoria = brinquedo.Id_categoria,
                Id_estoque = brinquedo.Id_estoque,
                Fornecedores = brinquedo.BrinquedoFornecedores?
                    .Select(bf => new FornecedorResponseDTO
                    {
                        Id_fornecedor = bf.Fornecedor.Id_fornecedor,
                        Nome_fornecedor = bf.Fornecedor.Nome_fornecedor,
                        Nome_representante = bf.Fornecedor.Nome_representante,
                        CNPJ = bf.Fornecedor.CNPJ,
                        Telefone = bf.Fornecedor.Telefone
                    }).ToList() ?? new List<FornecedorResponseDTO>()
            };

            return Ok(brinquedoResponse);
        }

        // POST: api/brinquedos
        [HttpPost]
        public IActionResult Create([FromBody] BrinquedoDTO brinquedoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Oracle-compatible existence checks
            if (brinquedoDTO.Id_categoria.HasValue)
            {
                var categoriaExists = _context.Categorias
                    .AsNoTracking()
                    .FirstOrDefault(c => c.Id_categoria == brinquedoDTO.Id_categoria) != null;

                if (!categoriaExists)
                    return BadRequest("Categoria não encontrada.");
            }

            if (brinquedoDTO.Id_estoque.HasValue)
            {
                var estoqueExists = _context.Estoques
                    .AsNoTracking()
                    .FirstOrDefault(e => e.Id_estoque == brinquedoDTO.Id_estoque) != null;

                if (!estoqueExists)
                    return BadRequest("Estoque não encontrado.");
            }

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

            var brinquedoResponse = new BrinquedoResponseDTO
            {
                Id_brinquedo = brinquedo.Id_brinquedo,
                Nome_brinquedo = brinquedo.Nome_brinquedo,
                Tipo_brinquedo = brinquedo.Tipo_brinquedo,
                Classificacao = brinquedo.Classificacao,
                Tamanho = brinquedo.Tamanho,
                Preco = brinquedo.Preco,
                Id_categoria = brinquedo.Id_categoria,
                Id_estoque = brinquedo.Id_estoque,
                Fornecedores = new List<FornecedorResponseDTO>()
            };

            return CreatedAtAction(nameof(GetById), new { id = brinquedo.Id_brinquedo }, brinquedoResponse);
        }

        // PUT: api/brinquedos/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] BrinquedoDTO brinquedoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var brinquedo = _context.Brinquedos.Find(id);
            if (brinquedo == null)
                return NotFound("Brinquedo não encontrado.");

            // Oracle-compatible existence checks
            if (brinquedoDTO.Id_categoria.HasValue)
            {
                var categoriaExists = _context.Categorias
                    .AsNoTracking()
                    .FirstOrDefault(c => c.Id_categoria == brinquedoDTO.Id_categoria) != null;

                if (!categoriaExists)
                    return BadRequest("Categoria não encontrada.");
            }

            if (brinquedoDTO.Id_estoque.HasValue)
            {
                var estoqueExists = _context.Estoques
                    .AsNoTracking()
                    .FirstOrDefault(e => e.Id_estoque == brinquedoDTO.Id_estoque) != null;

                if (!estoqueExists)
                    return BadRequest("Estoque não encontrado.");
            }

            brinquedo.Nome_brinquedo = brinquedoDTO.Nome_brinquedo;
            brinquedo.Tipo_brinquedo = brinquedoDTO.Tipo_brinquedo;
            brinquedo.Classificacao = brinquedoDTO.Classificacao;
            brinquedo.Tamanho = brinquedoDTO.Tamanho;
            brinquedo.Preco = brinquedoDTO.Preco;
            brinquedo.Id_categoria = brinquedoDTO.Id_categoria;
            brinquedo.Id_estoque = brinquedoDTO.Id_estoque;

            _context.Brinquedos.Update(brinquedo);
            _context.SaveChanges();

            return Ok(new BrinquedoResponseDTO
            {
                Id_brinquedo = brinquedo.Id_brinquedo,
                Nome_brinquedo = brinquedo.Nome_brinquedo,
                Tipo_brinquedo = brinquedo.Tipo_brinquedo,
                Classificacao = brinquedo.Classificacao,
                Tamanho = brinquedo.Tamanho,
                Preco = brinquedo.Preco,
                Id_categoria = brinquedo.Id_categoria,
                Id_estoque = brinquedo.Id_estoque
            });
        }

        // DELETE: api/brinquedos/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var brinquedo = _context.Brinquedos.Find(id);
            if (brinquedo == null)
                return NotFound("Brinquedo não encontrado.");

            _context.Brinquedos.Remove(brinquedo);
            _context.SaveChanges();

            return NoContent();
        }

        // POST: api/brinquedos/{brinquedoId}/fornecedores/{fornecedorId}
        [HttpPost("{brinquedoId}/fornecedores/{fornecedorId}")]
        public IActionResult AdicionarFornecedor(int brinquedoId, int fornecedorId)
        {
            var brinquedo = _context.Brinquedos.Find(brinquedoId);
            var fornecedor = _context.Fornecedores.Find(fornecedorId);

            if (brinquedo == null || fornecedor == null)
                return NotFound("Brinquedo ou fornecedor não encontrado.");

            // Oracle-compatible relationship check
            var relacaoExistente = _context.BrinquedoFornecedores
                .FirstOrDefault(bf => bf.BrinquedoId == brinquedoId && bf.FornecedorId == fornecedorId) != null;

            if (relacaoExistente)
                return BadRequest("Este fornecedor já está associado ao brinquedo.");

            _context.BrinquedoFornecedores.Add(new BrinquedoFornecedor
            {
                BrinquedoId = brinquedoId,
                FornecedorId = fornecedorId
            });

            _context.SaveChanges();

            return Ok(new { message = "Fornecedor associado com sucesso." });
        }

        // DELETE: api/brinquedos/{brinquedoId}/fornecedores/{fornecedorId}
        [HttpDelete("{brinquedoId}/fornecedores/{fornecedorId}")]
        public IActionResult RemoverFornecedor(int brinquedoId, int fornecedorId)
        {
            var relacao = _context.BrinquedoFornecedores
                .FirstOrDefault(bf => bf.BrinquedoId == brinquedoId && bf.FornecedorId == fornecedorId);

            if (relacao == null)
                return NotFound("Relação não encontrada.");

            _context.BrinquedoFornecedores.Remove(relacao);
            _context.SaveChanges();

            return NoContent();
        }

        // GET: api/brinquedos/{id}/fornecedores
        [HttpGet("{id}/fornecedores")]
        public IActionResult GetFornecedoresPorBrinquedo(int id)
        {
            var brinquedoExists = _context.Brinquedos.Find(id) != null;
            if (!brinquedoExists)
                return NotFound("Brinquedo não encontrado.");

            var fornecedores = _context.BrinquedoFornecedores
                .Where(bf => bf.BrinquedoId == id)
                .Include(bf => bf.Fornecedor)
                .AsNoTracking()
                .Select(bf => new FornecedorResponseDTO
                {
                    Id_fornecedor = bf.Fornecedor.Id_fornecedor,
                    Nome_fornecedor = bf.Fornecedor.Nome_fornecedor,
                    Nome_representante = bf.Fornecedor.Nome_representante,
                    CNPJ = bf.Fornecedor.CNPJ,
                    Telefone = bf.Fornecedor.Telefone
                }).ToList();

            return Ok(fornecedores);
        }
    }
}