namespace CleanArchiteture.Application.DTOs.Produto
{
    public class ProdutoDto
    {
        public long IdDto { get; set; }
        public string CodProdutoDto { get; set; } = string.Empty;
        public string DescProdutoDto { get; set; } = string.Empty;
        public decimal VlrPrecoDto { get; set; }
        public DateTime DataCadastroDto { get; set; }
        public long CategoriaIdDto { get; set; }
    }
}
