using System.ComponentModel.DataAnnotations;

namespace BrinquedosAPI.DTOs
{
    public class EstoqueDTO
    {
        [Required]
        public int Quantidade { get; set; }

        [Required]
        [StringLength(50)]
        public string Faixa { get; set; } // Exemplo: "1 a 500", "500 a 1000"
    }
}