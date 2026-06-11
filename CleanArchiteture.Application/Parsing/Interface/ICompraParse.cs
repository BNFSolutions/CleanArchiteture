using CleanArchiteture.Application.DTOs.Compra;
using CleanArchiteture.Domain.Models;

namespace CleanArchiteture.Application.Parsing.Interface
{
    public interface ICompraParse : IParse<CompraDto, Compra>
    {
        Compra Parse(CriarCompraDto origin);
        Compra Parse(AtualizarCompraDto origin, long id);
    }
}
