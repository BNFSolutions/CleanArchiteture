namespace CleanArchiteture.Domain.Models
{
    public class CompraItem
    {
        public long Id { get; set; }
        public long CompraId { get; set; }
        public long ProdutoId { get; set; }
        public int Quantidade { get; set; }

        public Compra? Compra { get; set; }
        public Produtos? Produto { get; set; }
    }
}
