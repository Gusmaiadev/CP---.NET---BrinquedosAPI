using System.ComponentModel.DataAnnotations;

namespace BrinquedosAPI.DTOs
{
    public class CategoriaDTO
    {
        [Required]
        [StringLength(100)]
        public string Nome_categoria { get; set; }
    }
}