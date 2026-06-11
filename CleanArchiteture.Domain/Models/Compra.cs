namespace CleanArchiteture.Domain.Models
{
    public class Compra
    {
        public long Id { get; set; }
        public long ClienteId { get; set; }
        public DateTime DataCompra { get; set; }

        public Cliente? Cliente { get; set; }
        public ICollection<CompraItem> Itens { get; set; } = new List<CompraItem>();
    }
}
