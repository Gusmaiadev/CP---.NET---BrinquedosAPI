// Data/Fornecedor.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BrinquedosAPI.Data
{
    public class Fornecedor
    {
        [Key]
        public int Id_fornecedor { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome_fornecedor { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome_representante { get; set; }

        [Required]
        [StringLength(18)] // 14 dígitos + formatação
        public string CNPJ { get; set; }

        [Required]
        [StringLength(15)] // Considerando formato internacional
        public string Telefone { get; set; }

        // Relação muitos-para-muitos com Brinquedos
        public ICollection<BrinquedoFornecedor> BrinquedoFornecedores { get; set; }
    }
}