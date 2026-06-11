namespace CleanArchiteture.Domain.Models
{
    public class Produtos
    {
        public long Id { get; set; }
        public string CodProduto { get; set; } = string.Empty;
        public string DescProduto { get; set; } = string.Empty;
        public decimal VlrPreco { get; set; }
        public DateTime DataCadastro { get; set; }

        public long CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
