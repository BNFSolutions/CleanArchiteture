using CleanArchiteture.Domain.Models;

namespace CleanArchiteture.Application.Services.Interface
{
    public interface ICategoriaService
    {
        Task<IEnumerable<Categoria>> Buscatodos(CancellationToken cancellationToken = default);
        Task<Categoria> Perquisar(long id, CancellationToken cancellationToken = default);
        Task<Categoria> Criar(Categoria categoria, CancellationToken cancellationToken = default);
        Task Alterar(Categoria categoria, CancellationToken cancellationToken = default);
        Task Deletar(long id, CancellationToken cancellationToken = default);
        Task<bool> Existe(long id, CancellationToken cancellationToken = default);
    }
}
