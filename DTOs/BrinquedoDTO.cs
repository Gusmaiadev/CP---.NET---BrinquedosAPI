using System.ComponentModel.DataAnnotations;

namespace BrinquedosAPI.DTOs
{
    public class BrinquedoDTO
    {
        [Required]
        [StringLength(100)]
        public string Nome_brinquedo { get; set; }

        [Required]
        [StringLength(50)]
        public string Tipo_brinquedo { get; set; }

        [Required]
        [StringLength(50)]
        public string Classificacao { get; set; }

        [Required]
        [StringLength(20)]
        public string Tamanho { get; set; }

        [Required]
        public decimal Preco { get; set; }

        // ID da Categoria (opcional)
        public int? Id_categoria { get; set; }

        // ID do Estoque (opcional)
        public int? Id_estoque { get; set; }
    }
}