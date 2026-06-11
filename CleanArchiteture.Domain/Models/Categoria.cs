namespace CleanArchiteture.Domain.Models
{
    public class Categoria
    {
        public long Id { get; set; }
        public string CodCategoria { get; set; } = string.Empty;
        public string DescCategoria { get; set; } = string.Empty;

        public ICollection<Produtos> Produtos { get; set; } = new List<Produtos>();
    }
}
