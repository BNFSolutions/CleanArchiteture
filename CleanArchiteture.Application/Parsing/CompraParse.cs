using System.Collections.Generic;
using CleanArchiteture.Application.DTOs.Compra;
using CleanArchiteture.Application.Parsing.Interface;
using CleanArchiteture.Domain.Models;

namespace CleanArchiteture.Application.Parsing
{
    public class CompraParse : ICompraParse
    {
        public Compra Parse(CompraDto origin)
        {
            if (origin == null) return null;

            var destino = new Compra
            {
                Id = origin.IdDto,
                ClienteId = origin.ClienteIdDto,
                DataCompra = origin.DataCompraDto
            };

            return destino;
        }

        public CompraDto Parse(Compra origin)
        {
            if (origin == null) return null;

            var destino = new CompraDto
            {
                IdDto = origin.Id,
                ClienteIdDto = origin.ClienteId,
                DataCompraDto = origin.DataCompra
            };

            return destino;
        }

        public List<Compra> ParseList(IEnumerable<CompraDto> origin)
        {
            // Versao compacta: return origin == null ? null : origin.Select(Parse).ToList();

            if (origin == null) return null;

            var destino = new List<Compra>();

            foreach (var item in origin)
            {
                destino.Add(Parse(item));
            }

            return destino;
        }

        public List<CompraDto> ParseList(IEnumerable<Compra> origin)
        {
            // Versao compacta: return origin == null ? null : origin.Select(Parse).ToList();

            if (origin == null) return null;

            var destino = new List<CompraDto>();

            foreach (var item in origin)
            {
                destino.Add(Parse(item));
            }

            return destino;
        }

        public Compra Parse(CriarCompraDto origin)
        {
            if (origin == null) return null;

            var destino = new Compra
            {
                ClienteId = origin.ClienteIdDto,
                DataCompra = origin.DataCompraDto ?? default
            };

            return destino;
        }

        public Compra Parse(AtualizarCompraDto origin, long id)
        {
            if (origin == null) return null;

            var destino = new Compra
            {
                Id = id,
                ClienteId = origin.ClienteIdDto,
                DataCompra = origin.DataCompraDto ?? default
            };

            return destino;
        }
    }
}
