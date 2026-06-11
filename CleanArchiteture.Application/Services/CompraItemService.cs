using CleanArchiteture.Application.Services.Interface;
using CleanArchiteture.Domain.Models;
using CleanArchiteture.Domain.Repositories.Interfaces;

namespace CleanArchiteture.Application.Services
{
    public class CompraItemService : ICompraItemService
    {
        private readonly ICompraItemRepository _repository;

        public CompraItemService(ICompraItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<CompraItem> Criar(CompraItem item, CancellationToken cancellationToken = default)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "O item de compra nao pode ser nulo.");
            }

            if (item.Quantidade <= 0)
            {
                throw new ArgumentException("A quantidade deve ser maior que zero.", nameof(item));
            }

            return await _repository.AddAsync(item, cancellationToken);
        }

        public async Task Deletar(long id, CancellationToken cancellationToken = default)
        {
            var item = await _repository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                throw new KeyNotFoundException($"Item de compra com id {id} nao encontrado.");
            }

            await _repository.DeleteAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<CompraItem>> Buscatodos(CancellationToken cancellationToken = default)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }

        public async Task<CompraItem> Perquisar(long id, CancellationToken cancellationToken = default)
        {
            return await _repository.GetByIdAsync(id, cancellationToken);
        }

        public async Task Alterar(CompraItem item, CancellationToken cancellationToken = default)
        {
            var existente = await _repository.GetByIdAsync(item.Id, cancellationToken);
            if (existente == null)
            {
                throw new KeyNotFoundException($"Item de compra com id {item.Id} nao encontrado.");
            }

            existente.CompraId = item.CompraId;
            existente.ProdutoId = item.ProdutoId;
            existente.Quantidade = item.Quantidade;

            await _repository.UpdateAsync(existente, cancellationToken);
        }

        public async Task<bool> Existe(long id, CancellationToken cancellationToken = default)
        {
            return await _repository.ExistsAsync(id, cancellationToken);
        }
    }
}
