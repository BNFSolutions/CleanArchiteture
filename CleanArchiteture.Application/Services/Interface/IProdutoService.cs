using CleanArchiteture.Domain.Models;

namespace CleanArchiteture.Application.Services.Interface
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produtos>> Buscatodos(CancellationToken cancellationToken = default);
        Task<Produtos> Perquisar(long id, CancellationToken cancellationToken = default);
        Task<Produtos> Criar(Produtos produto, CancellationToken cancellationToken = default);
        Task Alterar(Produtos produto, CancellationToken cancellationToken = default);
        Task Deletar(long id, CancellationToken cancellationToken = default);
        Task<bool> Existe(long id, CancellationToken cancellationToken = default);
    }
}
