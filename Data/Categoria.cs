using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BrinquedosAPI.Data
{
    public class Categoria
    {
        [Key]
        public int Id_categoria { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome_categoria { get; set; }

        // Relação com Brinquedo (1:N)
        public ICollection<Brinquedo>? Brinquedos { get; set; }
    }
}