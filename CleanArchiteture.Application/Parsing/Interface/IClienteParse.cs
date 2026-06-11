using CleanArchiteture.Application.DTOs.Cliente;
using CleanArchiteture.Domain.Models;

namespace CleanArchiteture.Application.Parsing.Interface
{
    public interface IClienteParse : IParse<ClienteDto, Cliente>
    {
        Cliente Parse(CriarClienteDto origin);
        Cliente Parse(AtualizarClienteDto origin, long id);
    }
}
