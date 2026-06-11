using CleanArchiteture.Domain.Models;

namespace CleanArchiteture.Application.Services.Interface
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> Buscatodos(CancellationToken cancellationToken = default);
        Task<Cliente> Perquisar(long id, CancellationToken cancellationToken = default);
        Task<Cliente> Criar(Cliente cliente, CancellationToken cancellationToken = default);
        Task Alterar(Cliente cliente, CancellationToken cancellationToken = default);
        Task Deletar(long id, CancellationToken cancellationToken = default);
        Task<bool> Existe(long id, CancellationToken cancellationToken = default);
    }
}
