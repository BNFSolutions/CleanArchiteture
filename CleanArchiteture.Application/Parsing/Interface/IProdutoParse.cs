using CleanArchiteture.Application.DTOs.Produto;
using CleanArchiteture.Domain.Models;

namespace CleanArchiteture.Application.Parsing.Interface
{
    public interface IProdutoParse : IParse<ProdutoDto, Produtos>
    {
        Produtos Parse(CriarProdutoDto origin);
        Produtos Parse(AtualizarProdutoDto origin, long id);
    }
}
