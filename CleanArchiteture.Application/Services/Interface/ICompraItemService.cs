using CleanArchiteture.Domain.Models;

namespace CleanArchiteture.Application.Services.Interface
{
    public interface ICompraItemService
    {
        Task<IEnumerable<CompraItem>> Buscatodos(CancellationToken cancellationToken = default);
        Task<CompraItem> Perquisar(long id, CancellationToken cancellationToken = default);
        Task<CompraItem> Criar(CompraItem item, CancellationToken cancellationToken = default);
        Task Alterar(CompraItem item, CancellationToken cancellationToken = default);
        Task Deletar(long id, CancellationToken cancellationToken = default);
        Task<bool> Existe(long id, CancellationToken cancellationToken = default);
    }
}
