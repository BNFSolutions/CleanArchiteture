namespace CleanArchiteture.Application.DTOs.Produto
{
    public class AtualizarProdutoDto
    {
        public string CodProdutoDto { get; set; } = string.Empty;
        public string DescProdutoDto { get; set; } = string.Empty;
        public decimal VlrPrecoDto { get; set; }
        public long CategoriaIdDto { get; set; }
    }
}
