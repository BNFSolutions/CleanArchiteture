using CleanArchiteture.Application.Services.Interface;
using CleanArchiteture.Domain.Models;
using CleanArchiteture.Domain.Repositories.Interfaces;

namespace CleanArchiteture.Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _repository;

        public CategoriaService(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Categoria> Criar(Categoria categoria, CancellationToken cancellationToken = default)
        {
            return await _repository.AddAsync(categoria, cancellationToken);
        }

        public async Task Deletar(long id, CancellationToken cancellationToken = default)
        {
            var categoria = await _repository.GetByIdAsync(id, cancellationToken);
            if (categoria == null)
            {
                throw new KeyNotFoundException($"Categoria com id {id} nao encontrada.");
            }

            await _repository.DeleteAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<Categoria>> Buscatodos(CancellationToken cancellationToken = default)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }

        public async Task<Categoria> Perquisar(long id, CancellationToken cancellationToken = default)
        {
            return await _repository.GetByIdAsync(id, cancellationToken);
        }

        public async Task Alterar(Categoria categoria, CancellationToken cancellationToken = default)
        {
            var categoriaExistente = await _repository.GetByIdAsync(categoria.Id, cancellationToken);
            if (categoriaExistente == null)
            {
                throw new KeyNotFoundException($"Categoria com id {categoria.Id} nao encontrada.");
            }

            categoriaExistente.CodCategoria = categoria.CodCategoria;
            categoriaExistente.DescCategoria = categoria.DescCategoria;

            await _repository.UpdateAsync(categoriaExistente, cancellationToken);
        }

        public async Task<bool> Existe(long id, CancellationToken cancellationToken = default)
        {
            return await _repository.ExistsAsync(id, cancellationToken);
        }
    }
}
