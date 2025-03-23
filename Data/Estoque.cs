using System.ComponentModel.DataAnnotations;

namespace BrinquedosAPI.Data
{
    public class Estoque
    {
        [Key]
        public int Id_estoque { get; set; }

        [Required]
        public int Id_brinquedo { get; set; }

        [Required]
        public int Quantidade { get; set; }

        // Relação com Brinquedo (1:1)
        public Brinquedo? Brinquedo { get; set; }
    }
}