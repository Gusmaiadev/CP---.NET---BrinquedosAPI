// DTOs/FornecedorDTO.cs
using System.ComponentModel.DataAnnotations;

namespace BrinquedosAPI.DTOs
{
    public class FornecedorDTO
    {
        [Required]
        [StringLength(100)]
        public string Nome_fornecedor { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome_representante { get; set; }

        [Required]
        [StringLength(18)]
        public string CNPJ { get; set; }

        [Required]
        [StringLength(15)]
        public string Telefone { get; set; }
    }
}