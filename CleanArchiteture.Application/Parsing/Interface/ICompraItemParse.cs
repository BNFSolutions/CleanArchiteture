using CleanArchiteture.Application.DTOs.CompraItem;
using CleanArchiteture.Domain.Models;

namespace CleanArchiteture.Application.Parsing.Interface
{
    public interface ICompraItemParse : IParse<CompraItemDto, CompraItem>
    {
        CompraItem Parse(CriarCompraItemDto origin);
        CompraItem Parse(AtualizarCompraItemDto origin, long id);
    }
}
