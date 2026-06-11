namespace CleanArchiteture.Domain.Models
{
    public class Cliente
    {
        public long Id { get; set; }
        public string Cpf { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;

        public ICollection<Compra> Compras { get; set; } = new List<Compra>();
    }
}
