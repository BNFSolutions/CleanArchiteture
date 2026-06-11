using System.Collections.Generic;
using CleanArchiteture.Application.DTOs.Cliente;
using CleanArchiteture.Application.Parsing.Interface;
using CleanArchiteture.Domain.Models;

namespace CleanArchiteture.Application.Parsing
{
    public class ClienteParse : IClienteParse
    {
        public Cliente Parse(ClienteDto origin)
        {
            if (origin == null) return null;

            var destino = new Cliente
            {
                Id = origin.IdDto,
                Cpf = origin.CpfDto,
                Nome = origin.NomeDto
            };

            return destino;
        }

        public ClienteDto Parse(Cliente origin)
        {
            if (origin == null) return null;

            var destino = new ClienteDto
            {
                IdDto = origin.Id,
                CpfDto = origin.Cpf,
                NomeDto = origin.Nome
            };

            return destino;
        }

        public List<Cliente> ParseList(IEnumerable<ClienteDto> origin)
        {
            // Versao compacta: return origin == null ? null : origin.Select(Parse).ToList();

            if (origin == null) return null;

            var destino = new List<Cliente>();

            foreach (var item in origin)
            {
                destino.Add(Parse(item));
            }

            return destino;
        }

        public List<ClienteDto> ParseList(IEnumerable<Cliente> origin)
        {
            // Versao compacta: return origin == null ? null : origin.Select(Parse).ToList();

            if (origin == null) return null;

            var destino = new List<ClienteDto>();

            foreach (var item in origin)
            {
                destino.Add(Parse(item));
            }

            return destino;
        }

        public Cliente Parse(CriarClienteDto origin)
        {
            if (origin == null) return null;

            var destino = new Cliente
            {
                Cpf = origin.CpfDto,
                Nome = origin.NomeDto
            };

            return destino;
        }

        public Cliente Parse(AtualizarClienteDto origin, long id)
        {
            if (origin == null) return null;

            var destino = new Cliente
            {
                Id = id,
                Cpf = origin.CpfDto,
                Nome = origin.NomeDto
            };

            return destino;
        }
    }
}
