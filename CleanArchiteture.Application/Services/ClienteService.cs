using CleanArchiteture.Application.Services.Interface;
using CleanArchiteture.Domain.Models;
using CleanArchiteture.Domain.Repositories.Interfaces;

namespace CleanArchiteture.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<Cliente> Criar(Cliente cliente, CancellationToken cancellationToken = default)
        {
            if (cliente == null)
            {
                throw new ArgumentNullException(nameof(cliente), "O cliente nao pode ser nulo.");
            }

            return await _repository.AddAsync(cliente, cancellationToken);
        }

        public async Task Deletar(long id, CancellationToken cancellationToken = default)
        {
            var cliente = await _repository.GetByIdAsync(id, cancellationToken);
            if (cliente == null)
            {
                throw new KeyNotFoundException($"Cliente com id {id} nao encontrado.");
            }

            await _repository.DeleteAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<Cliente>> Buscatodos(CancellationToken cancellationToken = default)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }

        public async Task<Cliente> Perquisar(long id, CancellationToken cancellationToken = default)
        {
            return await _repository.GetByIdAsync(id, cancellationToken);
        }

        public async Task Alterar(Cliente cliente, CancellationToken cancellationToken = default)
        {
            var existente = await _repository.GetByIdAsync(cliente.Id, cancellationToken);
            if (existente == null)
            {
                throw new KeyNotFoundException($"Cliente com id {cliente.Id} nao encontrado.");
            }

            existente.Cpf = cliente.Cpf;
            existente.Nome = cliente.Nome;

            await _repository.UpdateAsync(existente, cancellationToken);
        }

        public async Task<bool> Existe(long id, CancellationToken cancellationToken = default)
        {
            return await _repository.ExistsAsync(id, cancellationToken);
        }
    }
}
