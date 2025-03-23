using System.ComponentModel.DataAnnotations;

namespace BrinquedosAPI.Data
{
    public class Estoque
    {
        [Key]
        public int Id_estoque { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        [StringLength(50)]
        public string Faixa { get; set; } // Adicione esta linha

        // Relação com Brinquedo (opcional)
        public int? Id_brinquedo { get; set; }
        public Brinquedo? Brinquedo { get; set; }
    }
}