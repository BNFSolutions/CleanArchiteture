using CleanArchiteture.Domain.Models;

namespace CleanArchiteture.Application.Services.Interface
{
    public interface ICompraService
    {
        Task<IEnumerable<Compra>> Buscatodos(CancellationToken cancellationToken = default);
        Task<Compra> Perquisar(long id, CancellationToken cancellationToken = default);
        Task<Compra> Criar(Compra compra, CancellationToken cancellationToken = default);
        Task Alterar(Compra compra, CancellationToken cancellationToken = default);
        Task Deletar(long id, CancellationToken cancellationToken = default);
        Task<bool> Existe(long id, CancellationToken cancellationToken = default);
    }
}
