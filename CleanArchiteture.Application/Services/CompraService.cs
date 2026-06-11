using CleanArchiteture.Application.Services.Interface;
using CleanArchiteture.Domain.Models;
using CleanArchiteture.Domain.Repositories.Interfaces;

namespace CleanArchiteture.Application.Services
{
    public class CompraService : ICompraService
    {
        private readonly ICompraRepository _repository;

        public CompraService(ICompraRepository repository)
        {
            _repository = repository;
        }

        public async Task<Compra> Criar(Compra compra, CancellationToken cancellationToken = default)
        {
            if (compra == null)
            {
                throw new ArgumentNullException(nameof(compra), "A compra nao pode ser nula.");
            }

            compra.DataCompra = compra.DataCompra == default
                ? DateTime.Now
                : compra.DataCompra;

            return await _repository.AddAsync(compra, cancellationToken);
        }

        public async Task Deletar(long id, CancellationToken cancellationToken = default)
        {
            var compra = await _repository.GetByIdAsync(id, cancellationToken);
            if (compra == null)
            {
                throw new KeyNotFoundException($"Compra com id {id} nao encontrada.");
            }

            await _repository.DeleteAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<Compra>> Buscatodos(CancellationToken cancellationToken = default)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }

        public async Task<Compra> Perquisar(long id, CancellationToken cancellationToken = default)
        {
            return await _repository.GetByIdAsync(id, cancellationToken);
        }

        public async Task Alterar(Compra compra, CancellationToken cancellationToken = default)
        {
            var existente = await _repository.GetByIdAsync(compra.Id, cancellationToken);
            if (existente == null)
            {
                throw new KeyNotFoundException($"Compra com id {compra.Id} nao encontrada.");
            }

            existente.ClienteId = compra.ClienteId;
            existente.DataCompra = compra.DataCompra == default
                ? existente.DataCompra
                : compra.DataCompra;

            await _repository.UpdateAsync(existente, cancellationToken);
        }

        public async Task<bool> Existe(long id, CancellationToken cancellationToken = default)
        {
            return await _repository.ExistsAsync(id, cancellationToken);
        }
    }
}
