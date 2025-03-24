// Data/BrinquedoFornecedor.cs
using System.ComponentModel.DataAnnotations;

namespace BrinquedosAPI.Data
{
    public class BrinquedoFornecedor
    {
        [Key]
        public int Id { get; set; }

        public int BrinquedoId { get; set; }
        public Brinquedo Brinquedo { get; set; }

        public int FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }
    }
}