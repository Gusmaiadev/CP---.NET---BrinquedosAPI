using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrinquedosAPI.Data
{
    public class Brinquedo
    {
        [Key]
        public int Id_brinquedo { get; set; }

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
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Preco { get; set; }

        // Relação com Categoria (opcional)
        public int? Id_categoria { get; set; }
        public Categoria? Categoria { get; set; }

        // Relação com Estoque (opcional)
        public int? Id_estoque { get; set; }
        public Estoque? Estoque { get; set; }

        // Relação muitos-para-muitos com Fornecedores
        public ICollection<BrinquedoFornecedor> BrinquedoFornecedores { get; set; }

        // Construtor para inicializar a coleção
        public Brinquedo()
        {
            BrinquedoFornecedores = new HashSet<BrinquedoFornecedor>();
        }
    }
}