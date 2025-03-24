namespace BrinquedosAPI.DTOs
{
    public class BrinquedoResponseDTO
    {
        public int Id_brinquedo { get; set; }
        public string Nome_brinquedo { get; set; }
        public string Tipo_brinquedo { get; set; }
        public string Classificacao { get; set; }
        public string Tamanho { get; set; }
        public decimal Preco { get; set; }
        public int? Id_categoria { get; set; }
        public int? Id_estoque { get; set; }
        public List<FornecedorResponseDTO> Fornecedores { get; set; } = new List<FornecedorResponseDTO>();
    }
}