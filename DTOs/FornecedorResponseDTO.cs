// DTOs/FornecedorResponseDTO.cs
namespace BrinquedosAPI.DTOs
{
    public class FornecedorResponseDTO
    {
        public int Id_fornecedor { get; set; }
        public string Nome_fornecedor { get; set; }
        public string Nome_representante { get; set; }
        public string CNPJ { get; set; }
        public string Telefone { get; set; }
    }
}