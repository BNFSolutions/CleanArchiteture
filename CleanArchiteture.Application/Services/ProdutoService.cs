using CleanArchiteture.Application.Services.Interface;
using CleanArchiteture.Domain.Models;
using CleanArchiteture.Domain.Repositories.Interfaces;

namespace CleanArchiteture.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repository;

        public ProdutoService(IProdutoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Produtos> Criar(Produtos produto, CancellationToken cancellationToken = default)
        {
            if (produto == null)
            {
                throw new ArgumentNullException(nameof(produto), "O produto nao pode ser nulo.");
            }

            return await _repository.AddAsync(produto, cancellationToken);
        }

        public async Task Deletar(long id, CancellationToken cancellationToken = default)
        {
            var produto = await _repository.GetByIdAsync(id, cancellationToken);
            if (produto == null)
            {
                throw new KeyNotFoundException($"Produto com id {id} nao encontrado.");
            }

            await _repository.DeleteAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<Produtos>> Buscatodos(CancellationToken cancellationToken = default)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }

        public async Task<Produtos> Perquisar(long id, CancellationToken cancellationToken = default)
        {
            return await _repository.GetByIdAsync(id, cancellationToken);
        }

        public async Task Alterar(Produtos produto, CancellationToken cancellationToken = default)
        {
            var existente = await _repository.GetByIdAsync(produto.Id, cancellationToken);
            if (existente == null)
            {
                throw new KeyNotFoundException($"Produto com id {produto.Id} nao encontrado.");
            }

            existente.CodProduto = produto.CodProduto;
            existente.DescProduto = produto.DescProduto;
            existente.VlrPreco = produto.VlrPreco;
            existente.CategoriaId = produto.CategoriaId;

            await _repository.UpdateAsync(existente, cancellationToken);
        }

        public async Task<bool> Existe(long id, CancellationToken cancellationToken = default)
        {
            return await _repository.ExistsAsync(id, cancellationToken);
        }
    }
}
