using System.Collections.Generic;
using CleanArchiteture.Application.DTOs.CompraItem;
using CleanArchiteture.Application.Parsing.Interface;
using CleanArchiteture.Domain.Models;

namespace CleanArchiteture.Application.Parsing
{
    public class CompraItemParse : ICompraItemParse
    {
        public CompraItem Parse(CompraItemDto origin)
        {
            if (origin == null) return null;

            var destino = new CompraItem
            {
                Id = origin.IdDto,
                CompraId = origin.CompraIdDto,
                ProdutoId = origin.ProdutoIdDto,
                Quantidade = origin.QuantidadeDto
            };

            return destino;
        }

        public CompraItemDto Parse(CompraItem origin)
        {
            if (origin == null) return null;

            var destino = new CompraItemDto
            {
                IdDto = origin.Id,
                CompraIdDto = origin.CompraId,
                ProdutoIdDto = origin.ProdutoId,
                QuantidadeDto = origin.Quantidade
            };

            return destino;
        }

        public List<CompraItem> ParseList(IEnumerable<CompraItemDto> origin)
        {
            // Versao compacta: return origin == null ? null : origin.Select(Parse).ToList();

            if (origin == null) return null;

            var destino = new List<CompraItem>();

            foreach (var item in origin)
            {
                destino.Add(Parse(item));
            }

            return destino;
        }

        public List<CompraItemDto> ParseList(IEnumerable<CompraItem> origin)
        {
            // Versao compacta: return origin == null ? null : origin.Select(Parse).ToList();

            if (origin == null) return null;

            var destino = new List<CompraItemDto>();

            foreach (var item in origin)
            {
                destino.Add(Parse(item));
            }

            return destino;
        }

        public CompraItem Parse(CriarCompraItemDto origin)
        {
            if (origin == null) return null;

            var destino = new CompraItem
            {
                CompraId = origin.CompraIdDto,
                ProdutoId = origin.ProdutoIdDto,
                Quantidade = origin.QuantidadeDto
            };

            return destino;
        }

        public CompraItem Parse(AtualizarCompraItemDto origin, long id)
        {
            if (origin == null) return null;

            var destino = new CompraItem
            {
                Id = id,
                CompraId = origin.CompraIdDto,
                ProdutoId = origin.ProdutoIdDto,
                Quantidade = origin.QuantidadeDto
            };

            return destino;
        }
    }
}
