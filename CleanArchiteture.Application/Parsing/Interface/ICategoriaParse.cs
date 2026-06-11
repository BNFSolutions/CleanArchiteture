using CleanArchiteture.Application.DTOs.Categoria;
using CleanArchiteture.Domain.Models;

namespace CleanArchiteture.Application.Parsing.Interface
{
    public interface ICategoriaParse : IParse<CategoriaDto, Categoria>
    {
        Categoria Parse(CriarCategoriaDto origin);
        Categoria Parse(AtualizarCategoriaDto origin, long id);
    }
}
